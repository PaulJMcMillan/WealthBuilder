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
                Account account;
                var accounts = db.Accounts.Where(x => x.EntityId == CurrentEntity.Id && x.Active == true && x.CashFlowForeCast == true);
                int count = accounts.Count();

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

                account = accounts.FirstOrDefault();
                DateTime cutOffDateTime = DateTime.Today.AddDays(1);
                var transactions = db.Transactions.Where(x => x.EntityId == CurrentEntity.Id && x.AccountId == account.Id && x.Date < cutOffDateTime).ToList();
                if (transactions.Count == 0) return 0;
                decimal currentBalance= transactions.Sum(p => p.Deposit - p.Withdrawal);
                return currentBalance;
            }
        }
    }
}