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
        private SortedDictionary<DateTime, decimal> summarizedEntries;
        private SortedDictionary<DateTime, decimal> dailyAmounts;

        public decimal BalanceBelowThreshold { get; private set; }
        public DateTime BalanceBelowThresholdDate { get; private set; }
        public decimal MinimumBalance { get; set; }
        public SortedDictionary<DateTime, decimal> DailyBalances { get; set; }
        public DateTime MinimumBalanceDate { get; set; }
        public decimal MinimumBalanceThreshold { get; set; }

        public CashFlowForecast(DateTime endDate, int interval)
        {
            this.endDate = endDate;
            this.interval = interval;
        }

        public void Compute(decimal accountBalance)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            CreateForecastEntry(DateTime.Today, accountBalance);
            CreateBudgetEntries();
            CreateInflowEntries();
            CreateReminderEntries();
            AddFutureTransactions();
            AggregateAmountsByDate();
            CreateDailyAmounts();
            PopulateReportTable();
        }

        private void AddFutureTransactions()
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            Account cashFlowForecastAccount = new AccountRepository().GetCashFlowForecastAccount();
            if (cashFlowForecastAccount == null) return;

            using (var db = new WBEntities())
            {
                //var transactions = from transaction in db.Transactions.Where(x => x.Date > DateTime.Today.Date)
                //         join entity in db.Entities.Where(x => x.Active == true && (x.Id == entityId || entityId == -1)) on transaction.EntityId equals entity.Id
                //         join account in db.Accounts.Where(x=>x.Active == true) on transaction.AccountId equals account.Id
                //         select transaction;

                var transactions = db.Transactions.Where(x => x.EntityId == CurrentEntity.Id && x.AccountId == cashFlowForecastAccount.Id);

                foreach (var transaction in transactions)
                {
                    if (transaction.Date.Date <= DateTime.Today) continue;
                    decimal deposit = transaction.Deposit;
                    decimal withdrawal = transaction.Withdrawal;
                    decimal amount = deposit != 0 ? deposit : -withdrawal;
                    CreateForecastEntry(transaction.Date.Date, amount);
                }
            }
        }

        private void CreateReminderEntries()
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            using (var db = new WBEntities())
            {
                //var reminders = from reminder in db.Reminders join entity in db.Entities.Where(x => x.Active == true && x.Id == entityId) on reminder.EntityId equals entity.Id select reminder;
                var reminders = db.Reminders.Where(x => x.EntityId == CurrentEntity.Id);

                foreach (var reminder in reminders)
                {
                    DateTime date = reminder.Date;
                    date = date.Date;
                    decimal payAmount = reminder.PayAmount;
                    decimal inflowAmount = reminder.InflowAmount;
                    decimal amount = inflowAmount - payAmount;
                    CreateForecastEntry(date, amount);
                }
            }
        }

        private void CreateForecastEntry(DateTime date, decimal amount)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            date = date.Date;
            if (date > endDate) return;
            cashFlowForecastList.Add(new CashFlowForecastEntry(date, amount));
        }

        private void CreateBudgetEntries()
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            using (var db = new WBEntities())
            {
                //var rs = from budget in db.Budgets join entity in db.Entities.Where(x=>x.Active == true && (x.Id == entityId || entityId == -1)) on budget.EntityId equals entity.Id select budget;
                var budgets = db.Budgets.Where(x => x.EntityId == CurrentEntity.Id);
                
                foreach (var budget in budgets)
                {
                    CreateEntriesForBudgetItem(budget);
                }
            }
        }

        private void CreateEntriesForBudgetItem(Budget r)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            if (r == null) return;
            if (!decimal.TryParse(r.Amount.ToString(), out decimal amount)) return;
            DateTime date;
            if (r.PayDate == null) return;
            date = r.PayDate.Date;
            string frequencyCode = FrequencyCode.GetById(r.FrequencyId);

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

        private void CreateInflowEntries()
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            using (var db = new WBEntities())
            {
                //var rs = from inflow in db.Inflows join entity in db.Entities.Where(x => x.Active == true && (x.Id == entityId || entityId == -1)) on inflow.EntityId equals entity.Id select inflow;
                var inflows = db.Inflows.Where(x => x.EntityId == CurrentEntity.Id);

                foreach (var inflow in inflows)
                {
                    CreateEntriesForInflow(inflow);
                }
            }
        }

        private void CreateEntriesForInflow(Inflow r)
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            if (r == null || r.FrequencyId == null || r.Amount == null || r.InflowDate == null) return;
            decimal amount = (decimal)r.Amount;
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
                    decimal balance;
                    if (!dailyAmounts.TryGetValue(dt, out balance)) throw new Exception("Error occurred in cash flow calculator.  Please call technical support.");
                    var cashFlowForecastData = new CashFlowForecastData();
                    int oaDate = (int)dt.ToOADate();
                    cashFlowForecastData.Date = oaDate;
                    cashFlowForecastData.DateString = dt.ToString("M/d/y");
                    cashFlowForecastData.Balance = (decimal)balance;
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
            decimal balance = 0;
            dailyAmounts = new SortedDictionary<DateTime, decimal>();
            var rs = summarizedEntries.OrderBy(x => x.Key).First();
            DateTime startDate = (DateTime)rs.Key;
              
            for (DateTime dt = startDate.Date; dt <= endDate; dt = dt.AddDays(1).Date)
            {
                decimal amount = 0;
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

            summarizedEntries = new SortedDictionary<DateTime, decimal>();
            foreach (var r in rs) summarizedEntries.Add(r.Date, r.Amount);
        }

        //public void CalculateAllEntities()
        //{
        //    using (var db = new WBEntities())
        //    {
        //        var entities = db.Entities.Where(x => x.Active == true);
        //        List<string> lowestBalances = new List<string>();

        //        foreach(var entity in entities)
        //        {
        //            ResetData();
        //            cashFlowForecastList.Clear();
        //            CalculateForOneEntity(entity.Id);
        //            string s, msg;

        //            if (BalanceBelowThresholdDate != DateTime.MinValue)
        //            {
        //                s = "Your balance of {0} will drop below {1} on {2}.";
        //                string cb = BalanceBelowThreshold.ToString("C");
        //                string t = MinimumBalanceThreshold.ToString("C");
        //                string d = BalanceBelowThresholdDate.ToShortDateString();
        //                msg = string.Format(s, cb, t, d);
        //                MessageBox.Show(msg, "Cash Balance Alert for " + entity.Name + "!");
        //            }

        //            string lowestBalanceString = MinimumBalance.ToString("C");
        //            string lowestBalanceDateString = MinimumBalanceDate.ToShortDateString();
        //            s = "The lowest balance for " + entity.Name + " is {0} on {1}.";
        //            msg = string.Format(s, lowestBalanceString, lowestBalanceDateString);
        //            lowestBalances.Add(msg);
        //        }

        //        new LowestBalancesNotificationsForm(lowestBalances).Show();
        //    }
        //}

        public void CalculateForDefaultEntity()
        {
            using (var db = new WBEntities())
            {
                var entities = db.Entities.Where(x => x.Active == true);
                var defaultEntity = db.Entities.Where(x => x.DefaultEntity == true && x.Active == true).FirstOrDefault();
                List<string> lowestBalances = new List<string>();
                ResetData();
                Calculate();
                string s, msg;

                if (BalanceBelowThresholdDate != DateTime.MinValue)
                {
                    s = "Your balance of {0} will drop below {1} on {2}.";
                    string cb = BalanceBelowThreshold.ToString("C");
                    string t = MinimumBalanceThreshold.ToString("C");
                    string d = BalanceBelowThresholdDate.ToShortDateString();
                    msg = string.Format(s, cb, t, d);
                    MessageBox.Show(msg, "Cash Balance Alert!");
                }

                string lowestBalanceString = MinimumBalance.ToString("C");
                string lowestBalanceDateString = MinimumBalanceDate.ToShortDateString();
                s = "The lowest balance is {0} on {1}.";
                msg = string.Format(s, lowestBalanceString, lowestBalanceDateString);
                lowestBalances.Add(msg);
                new LowestBalancesNotificationsForm(lowestBalances).Show();
            }
        }

        private void ResetData()
        {
            cashFlowForecastList.Clear();
            if(dailyAmounts != null) dailyAmounts.Clear();
            if (DailyBalances != null) DailyBalances.Clear();
            BalanceBelowThreshold = decimal.MinValue;
            BalanceBelowThresholdDate = DateTime.MinValue;
            MinimumBalance = decimal.MinValue;
            MinimumBalanceDate = DateTime.MinValue;
            MinimumBalanceThreshold = decimal.MinValue;
        }

        public void Calculate()
        {
            using (var db = new WBEntities())
            {
                decimal cashBalance = Cash.GetStartingBalanceForCashFlow();
                Compute(cashBalance);
                MinimumBalanceThreshold = CurrentEntity.LowestBalanceThreshold;
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