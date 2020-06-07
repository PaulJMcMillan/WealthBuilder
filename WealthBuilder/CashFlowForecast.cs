//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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
            CreateForecastEntry(DateTime.Today, accountBalance);
            CreateBudgetEntries();
            CreateInflowEntries();
            CreateReminderEntries();
            AddFutureTransactions();
            DateTime startDateTime = new DateTime(2020, 6, 6);
            DateTime endDateTime = new DateTime(2021, 6, 6);
            OutputCashFlowEntries(startDateTime,endDateTime);
            AggregateAmountsByDate();
            CreateDailyAmounts();
            PopulateReportTable();
        }

        private void OutputCashFlowEntries(DateTime startDateTime,DateTime endDateTime)
        {
            //This method is used for troubleshooting.
            var outputs = cashFlowForecastList.Where(x=>x.Date.Date>=startDateTime.Date && x.Date.Date <=endDateTime).OrderBy(x=>x.Date.Date).ToList();
            string fileName = Constants.DataFolder + "CashFlowForecastEntries.log";
            if (File.Exists(fileName)) File.Delete(fileName);
            List<string> contents = new List<string>();

            foreach (var output in outputs)
            {
                string msg = string.Format("{0}, {1}",output.Date.Date, output.Amount.ToString("C"));
                contents.Add(msg);
                //TextFile.AppendInfo(fileName, string.Format(msg,output.Date.Date,output.Amount));
            }

            File.AppendAllLines(fileName, contents);
        }

        private void AddFutureTransactions()
        {
            Account cashFlowForecastAccount = new AccountRepository().GetCashFlowForecastAccount();
            if (cashFlowForecastAccount == null) return;

            using (var db = new WBEntities())
            {
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
            using (var db = new WBEntities())
            {
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
            date = date.Date;
            if (date > endDate) return;
            cashFlowForecastList.Add(new CashFlowForecastEntry(date, amount));
        }

        private void CreateBudgetEntries()
        {
            using (var db = new WBEntities())
            {
                var budgets = db.Budgets.Where(x => x.EntityId == CurrentEntity.Id);
                
                foreach (var budget in budgets)
                {
                    CreateEntriesForBudgetItem(budget);
                }
            }
        }

        private void CreateEntriesForBudgetItem(Budget budget)
        {
            DateTime date = budget.PayDate.Date;
            string frequencyCode = FrequencyCode.GetById(budget.FrequencyId);

            if (frequencyCode == FrequencyCode.Once)
            {
                CreateForecastEntry(date, -budget.Amount);
                return;
            }

            while (date <= endDate)
            {
                CreateForecastEntry(date, -budget.Amount);
                date = Date.IncrementDate(date, budget.FrequencyId).Date;
            }
        }

        private void CreateInflowEntries()
        {
         
            using (var db = new WBEntities())
            {
                var inflows = db.Inflows.Where(x => x.EntityId == CurrentEntity.Id);

                foreach (var inflow in inflows)
                {
                    CreateEntriesForInflow(inflow);
                }
            }
        }

        private void CreateEntriesForInflow(Inflow inflow)
        {
            decimal amount = inflow.Amount;
            DateTime date = inflow.InflowDate.Date;

            string frequencyCode = FrequencyCode.GetById((int)inflow.FrequencyId);

            if (frequencyCode == FrequencyCode.Once)
            {
                CreateForecastEntry(date, amount);
                return;
            }

            while (date <= endDate)
            {
                CreateForecastEntry(date, amount);
                date = Date.IncrementDate(date, inflow.FrequencyId).Date;
            }
        }

        private void PopulateReportTable()
        {
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
                    cashFlowForecastData.Balance = balance;
                    rs.Add(cashFlowForecastData);
                }

                db.SaveChanges();

            }
        }

        private void CreateDailyAmounts()
        {
            decimal balance = 0;
            dailyAmounts = new SortedDictionary<DateTime, decimal>();
            var rs = summarizedEntries.OrderBy(x => x.Key).First();
            DateTime startDate = rs.Key;
              
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
            var rs = cashFlowForecastList
                .GroupBy(l => l.Date.Date)
                .Select(cl => new AggregateEntry
                {
                    Date = cl.First().Date.Date,
                    Amount = cl.Sum(c => c.Amount)
                });

            summarizedEntries = new SortedDictionary<DateTime, decimal>();
            
            foreach (var r in rs)
            {
                summarizedEntries.Add(r.Date, r.Amount);
            }
        }

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