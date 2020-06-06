//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WealthBuilder
{

    public class Cash
    {
        public class CashTransaction
        {
            public DateTime Date { get; set; }
            public decimal Deposit { get; set; }
            public decimal Withdrawal { get; set; }
            public long AccountId { get; set; }
            public long EntityId { get; set; }
        }

    public static decimal GetBalanceForOneAccount(long accountId, long entityId)
        {
            using (var db = new WBEntities())
            {
                var accountsLoaded = db.Accounts.Where(x => x.EntityId == entityId && x.Id == accountId && x.Active == true).ToList();
                List<CashTransaction> transactionsLoaded;
                var rs = db.Transactions.Where(x => x.EntityId == entityId).Join(db.Accounts.Where(x => x.Active == true), t => t.AccountId, a => a.Id, (t, a) => new { t.Date, t.Deposit, t.Withdrawal, t.AccountId, t.EntityId });
                List<CashTransaction> tQuery = new List<CashTransaction>();

                foreach (var r in rs)
                {
                    var cashTransaction = new CashTransaction();
                    if (r.Date != null) cashTransaction.Date = (DateTime)r.Date;
                    cashTransaction.Deposit = r.Deposit;
                    cashTransaction.Withdrawal = r.Withdrawal == null ? 0 : (decimal)r.Withdrawal;
                    cashTransaction.AccountId = r.AccountId;
                    cashTransaction.EntityId = r.EntityId;
                    tQuery.Add(cashTransaction);
                }

                transactionsLoaded = tQuery.Where(x => x.AccountId == accountId).ToList();
                var transactionsQuery = from transaction in transactionsLoaded
                                        join account in accountsLoaded on transaction.AccountId equals account.Id into transactionsFiltered
                                        select new { transaction.Date, transaction.Deposit, transaction.Withdrawal, transaction.AccountId };
                var transactionsFilteredLoaded = transactionsQuery.ToList();
                decimal currentBalance;
                var transactions = transactionsFilteredLoaded.Select(o => new { o.Date.Date, o.Deposit, o.Withdrawal });
                currentBalance = transactions.Sum(p => (p.Deposit) - (p.Withdrawal));
                return currentBalance;
            }
        }

        internal static decimal GetStartingBalanceForCashFlow()
        {
            using (var db = new WBEntities())
            {
                //List<Account> accountsLoaded;
                Account account;
                List<CashTransaction> cashTransactions = new List<CashTransaction>();
                var cashFlowForecastAccounts = db.Accounts.Where(x => x.EntityId == CurrentEntity.Id && x.Active == true && x.CashFlowForeCast == true);
                int count = cashFlowForecastAccounts.Count();

                if(count == 0)
                {
                    MessageBox.Show("Cash flow forecast cannot be calculated because no account is setup to track cash flow.");
                    return -1;
                }

                if (count > 1)
                {
                    MessageBox.Show("You have more than 1 account that's mark to track cash flow.  Only 1 account is allowed.");
                    return -1;
                }

                account = cashFlowForecastAccounts.FirstOrDefault();
                //var startingRs = db.Transactions.Where(x => x.EntityId == entityId).Join(db.Accounts.Where(x => x.Active == true && x.CashFlowForeCast == true),
                //                    t => t.AccountId, a => a.Id, (t, a) => new { t.Date, t.Deposit, t.Withdrawal, t.AccountId, t.EntityId }).ToList();
                var transactions = db.Transactions.Where(x => x.EntityId == CurrentEntity.Id && x.AccountId == account.Id);

                //var rs = startingRs.Join(db.Entities.Where(x => x.Active == true), t => t.EntityId, a => a.Id, (t, a) => new { t.Date, t.Deposit, t.Withdrawal, t.AccountId, t.EntityId }).ToList();

                foreach (var transaction in transactions)
                {
                    if (transaction.Date.Date > DateTime.Today) continue;

                    var cashTransaction = new CashTransaction()
                    {
                        Date = transaction.Date.Date,
                        Deposit = transaction.Deposit,
                        Withdrawal = transaction.Withdrawal,
                        AccountId = transaction.AccountId,
                        EntityId = transaction.EntityId

                    };

                    cashTransactions.Add(cashTransaction);
                }

                //transactionsLoaded = cashTransactions.Where(x => x.Date <= DateTime.Today).ToList();
                //var transactionsQuery = from transaction in transactionsLoaded
                //                        join account in accountsLoaded on transaction.AccountId equals account.Id into transactionsFiltered
                //                        select new { transaction.Date, transaction.Deposit, transaction.Withdrawal, transaction.AccountId };
                //var transactionsFilteredLoaded = transactionsQuery.ToList();
                decimal currentBalance=0;
                //var transactions = transactionsFilteredLoaded.Select(o => new { o.Date.Date, o.Deposit, o.Withdrawal });
                currentBalance = transactions.Sum(p => p.Deposit - p.Withdrawal);
                return currentBalance;
            }
        }
    }
}