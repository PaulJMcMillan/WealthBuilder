//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace WealthBuilder
{
    public class CashFlowForecast
    {
        private List<CashFlowForecastEntry> cashFlowForecastList = new List<CashFlowForecastEntry>();
        private DateTime endDate;
        private int interval;
        private SortedDictionary<DateTime, double> summarizedEntries;
        private SortedDictionary<DateTime, double> dailyAmounts;

        public double BalanceBelowThreshold { get; private set; }
        public DateTime BalanceBelowThresholdDate { get; private set; }
        public double MinimumBalance { get; set; }
        public SortedDictionary<DateTime, double> DailyBalances { get; set; }
        public DateTime MinimumBalanceDate { get; set; }
        public double MinimumBalanceThreshold { get; set; }

        public CashFlowForecast(DateTime endDate, int interval)
        {
            this.endDate = endDate;
            this.interval = interval;
        }

        public void Compute(double accountBalance, long entityId)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            CreateForecastEntry(DateTime.Today, accountBalance);
            CreateBudgetEntries(entityId);
            CreateInflowEntries(entityId);
            CreateReminderEntries(entityId);
            AddFutureTransactions(entityId);
            AggregateAmountsByDate();
            CreateDailyAmounts();
            PopulateReportTable();
        }

        private void AddFutureTransactions(long entityId)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            using (var db = new WBEntities())
            {
                var transactions = from transaction in db.Transactions.Where(x => x.Date > DateTime.Today.Date)
                         join entity in db.Entities.Where(x => x.Active == true && (x.Id == entityId || entityId == -1)) on transaction.EntityId equals entity.Id
                         join account in db.Accounts.Where(x=>x.Active == true) on transaction.AccountId equals account.Id
                         select transaction;

                foreach (var transaction in transactions)
                {
                    DateTime date;
                    if (transaction.Date == null) continue;
                    date = (DateTime)transaction.Date;
                    if (date <= DateTime.Today) continue;
                    double deposit = (double)transaction.Deposit;
                    double withdrawal = (double)transaction.Withdrawal;
                    double amount = deposit != 0 ? deposit : -withdrawal;
                    CreateForecastEntry(date, amount);
                }
            }
        }

        private void CreateReminderEntries(long entityId)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            using (var db = new WBEntities())
            {
                var reminders = from reminder in db.Reminders join entity in db.Entities.Where(x => x.Active == true && x.Id == entityId) on reminder.EntityId equals entity.Id select reminder;

                foreach (var reminder in reminders)
                {
                    if (reminder.Date == null) continue;
                    DateTime date = (DateTime)reminder.Date;
                    date = date.Date;
                    double payAmount = (double)(reminder.PayAmount ?? 0);
                    double inflowAmount = (double)(reminder.InflowAmount ?? 0);
                    double amount = inflowAmount - payAmount;
                    CreateForecastEntry(date, amount);
                }
            }
        }

        private void CreateForecastEntry(DateTime date, double amount)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            date = date.Date;
            if (date > endDate) return;
            cashFlowForecastList.Add(new CashFlowForecastEntry(date, amount));
        }

        private void CreateBudgetEntries(long entityId)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            using (var db = new WBEntities())
            {
                var rs = from budget in db.Budgets join entity in db.Entities.Where(x=>x.Active == true && (x.Id == entityId || entityId == -1)) on budget.EntityId equals entity.Id select budget;
                foreach (var r in rs) CreateEntriesForBudgetItem(r);
            }
        }

        private void CreateEntriesForBudgetItem(Budget r)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            if (r == null || r.FrequencyId == null || r.Amount == null || r.PayDate == null) return;
            double amount;
            if (!double.TryParse(r.Amount.ToString(), out amount)) return;
            DateTime date;
            //if (!DateTime.TryParse(r.PayDate, out date)) return;
            if (r.PayDate == null) return;
            date = ((DateTime)r.PayDate).Date;

            //if (r.Frequency == Frequencies.Once)
            //{
            //    //Start and End Dates are not checked for a one-time budget entry.
            //    CreateForecastEntry(date, -amount);
            //    return;
            //}

            string frequencyCode = FrequencyCode.GetById((int)r.FrequencyId);

            if (frequencyCode == FrequencyCode.Once)
            {
                CreateForecastEntry(date, -amount);
                return;
            }

            while (date <= endDate)
            {
                DateTime startDate = r.StartDate == null ? DateTime.MinValue : (DateTime)r.StartDate;
                DateTime endDate = r.EndDate == null ? DateTime.MaxValue : (DateTime)r.EndDate;

                if (startDate < endDate)
                {
                    if (date >= startDate && date <= endDate) CreateForecastEntry(date, -amount);
                }

                date = Date.IncrementDate(date, (int)r.FrequencyId).Date;
            }
        }

        private void CreateInflowEntries(long entityId)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            using (var db = new WBEntities())
            {
                var rs = from inflow in db.Inflows join entity in db.Entities.Where(x => x.Active == true && (x.Id == entityId || entityId == -1)) on inflow.EntityId equals entity.Id select inflow;
                foreach (var r in rs) CreateEntriesForInflow(r);
            }
        }

        private void CreateEntriesForInflow(Inflow r)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            if (r == null || r.FrequencyId == null || r.Amount == null || r.InflowDate == null) return;
            double amount = (double)r.Amount;
            DateTime date = (DateTime)r.InflowDate;
            date = date.Date;

            string frequencyCode = FrequencyCode.GetById((int)r.FrequencyId);

            if (frequencyCode == FrequencyCode.Once)
            {
                CreateForecastEntry(date, amount);
                return;
            }

            while (date <= endDate)
            {
                DateTime startDate = r.StartDate == null ? DateTime.MinValue : (DateTime)r.StartDate;
                DateTime endDate = r.EndDate == null ? DateTime.MaxValue : (DateTime)r.EndDate;

                if (startDate < endDate)
                {
                    if (date >= startDate && date <= endDate) CreateForecastEntry(date, amount);
                }

                date = Date.IncrementDate(date, (int)r.FrequencyId).Date;
            }
        }

        private void PopulateReportTable()
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            using (var db = new WBEntities())
            {
                db.Database.ExecuteSqlCommand("Delete From CashFlowForecastData");
                db.SaveChanges();

                var rs = db.CashFlowForecastDatas;

                for (DateTime dt = DateTime.Today.Date; dt <= endDate; dt = dt.AddDays(interval).Date)
                {
                    double balance;
                    if (!dailyAmounts.TryGetValue(dt, out balance)) throw new Exception("Error occurred in cash flow calculator.  Please call technical support.");
                    var cashFlowForecastData = new CashFlowForecastData();
                    int? oaDate = (int?)dt.ToOADate();
                    cashFlowForecastData.Date = oaDate;
                    cashFlowForecastData.DateString = dt.ToString("M/d/y");
                    cashFlowForecastData.Balance = (long?)balance;
                    rs.Add(cashFlowForecastData);
                    Console.WriteLine("Balance: " + cashFlowForecastData.Balance.ToString());
                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred in the Cash Flow Forecaster.  Please try again or contact Customer Care.");
                    Error.Log(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
                }
            }
        }

        private void CreateDailyAmounts()
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            double balance = 0;
            dailyAmounts = new SortedDictionary<DateTime, double>();
            var rs = summarizedEntries.OrderBy(x => x.Key).First();
            DateTime startDate = (DateTime)rs.Key;
              
            for (DateTime dt = startDate.Date; dt <= endDate; dt = dt.AddDays(1).Date)
            {
                double amount = 0;
                if (summarizedEntries.TryGetValue(dt, out amount)) balance += amount;
                dailyAmounts.Add(dt, balance);
            }

            DailyBalances = dailyAmounts;
        }

        private void AggregateAmountsByDate()
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            var rs = cashFlowForecastList
                .GroupBy(l => l.Date.Date)
                .Select(cl => new AggregateEntry
                {
                    Date = cl.First().Date.Date,
                    Amount = cl.Sum(c => c.Amount)
                });

            summarizedEntries = new SortedDictionary<DateTime, double>();
            foreach (var r in rs) summarizedEntries.Add(r.Date, r.Amount);
        }

        public void CalculateAllEntities()
        {
            using (var db = new WBEntities())
            {
                var entities = db.Entities.Where(x => x.Active == true);
                List<string> lowestBalances = new List<string>();

                foreach(var entity in entities)
                {
                    ResetData();
                    cashFlowForecastList.Clear();
                    CalculateForOneEntity(entity.Id);
                    string s, msg;

                    if (BalanceBelowThresholdDate != DateTime.MinValue)
                    {
                        s = "Your balance of {0} will drop below {1} on {2}.";
                        string cb = BalanceBelowThreshold.ToString("C");
                        string t = MinimumBalanceThreshold.ToString("C");
                        string d = BalanceBelowThresholdDate.ToShortDateString();
                        msg = string.Format(s, cb, t, d);
                        MessageBox.Show(msg, "Cash Balance Alert for " + entity.Name + "!");
                    }

                    string lowestBalanceString = MinimumBalance.ToString("C");
                    string lowestBalanceDateString = MinimumBalanceDate.ToShortDateString();
                    s = "The lowest balance for " + entity.Name + " is {0} on {1}.";
                    msg = string.Format(s, lowestBalanceString, lowestBalanceDateString);
                    lowestBalances.Add(msg);
                }

                new LowestBalancesNotificationsForm(lowestBalances).Show();
            }
        }

        private void ResetData()
        {
            cashFlowForecastList.Clear();
            if(dailyAmounts != null) dailyAmounts.Clear();
            if (DailyBalances != null) DailyBalances.Clear();
            BalanceBelowThreshold = double.MinValue;
            BalanceBelowThresholdDate = DateTime.MinValue;
            MinimumBalance = double.MinValue;
            MinimumBalanceDate = DateTime.MinValue;
            MinimumBalanceThreshold = double.MinValue;
        }

        public void CalculateForOneEntity(long entityId)
        {
            using (var db = new WBEntities())
            {
                var entity = db.Entities.Where(x => x.Active == true && x.Id==entityId).FirstOrDefault();
                if (entity == null) return;
                double cashBalance = Cash.GetStartingBalanceForCashFlow(entityId);
                Compute(cashBalance, entityId);
                MinimumBalanceThreshold = entity.LowestBalance == null ? 0 : (double)entity.LowestBalance;
                var rs = dailyAmounts.Where(x => x.Value < MinimumBalanceThreshold).FirstOrDefault();
                BalanceBelowThreshold = rs.Value;
                BalanceBelowThresholdDate = rs.Key;
                MinimumBalance = dailyAmounts.Min(x => x.Value);
                var r = dailyAmounts.Where(x => x.Value == MinimumBalance).FirstOrDefault();
                MinimumBalanceDate = r.Key;
            }
        }
    }
}