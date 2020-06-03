//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Collections.Generic;
using System.Linq;

namespace WealthBuilder
{

    public class Cash
    {
        public class CashTransaction
        {
            public DateTime Date { get; set; }
            public double? Deposit { get; set; }
            public double? Withdrawal { get; set; }
            public long? AccountId { get; set; }
            public long? EntityId { get; set; }
        }

        public class RawTransaction
        {
            public DateTime Date { get; set; }
            public double? Deposit { get; set; }
            public double? Withdrawal { get; set; }
            public long? AccountId { get; set; }
            public long? EntityId { get; set; }
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
                    cashTransaction.Deposit = r.Deposit == null ? 0 : (double)r.Deposit;
                    cashTransaction.Withdrawal = r.Withdrawal == null ? 0 : (double)r.Withdrawal;
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
                currentBalance = (decimal)transactions.Sum(p => (p.Deposit ?? 0) - (p.Withdrawal ?? 0));
                return currentBalance;
            }
        }

        internal static double GetStartingBalanceForCashFlow(long entityId)
        {
            //This method calcs the beginning cash balance for 1 entity or all (entityId = -1).

            using (var db = new WBEntities())
            {
                List<Account> accountsLoaded;
                List<CashTransaction> transactionsLoaded;
                List<CashTransaction> tQuery = new List<CashTransaction>();


                if (entityId == -1)
                {
                    accountsLoaded = db.Accounts.Where(x => x.Active == true && x.CashFlowForeCast == true).ToList();
                    var startingRs = db.Transactions.Join(db.Accounts.Where(x => x.Active == true && x.CashFlowForeCast == true),
                                              t => t.AccountId, a => a.Id, (t, a) => new { t.Date, t.Deposit, t.Withdrawal, t.AccountId, t.EntityId }).ToList();
                    var rs = startingRs.Join(db.Entities.Where(x => x.Active == true), t => t.EntityId, a => a.Id, (t, a) => new { t.Date, t.Deposit, t.Withdrawal, t.AccountId, t.EntityId }).ToList();

                    foreach (var r in rs)
                    {
                        var cashTransaction = new CashTransaction();
                        if (r.Date != null) cashTransaction.Date = (DateTime)r.Date;
                        cashTransaction.Deposit = r.Deposit == null ? 0 : (double)r.Deposit;
                        cashTransaction.Withdrawal = r.Withdrawal == null ? 0 : (double)r.Withdrawal;
                        cashTransaction.AccountId = r.AccountId;
                        cashTransaction.EntityId = r.EntityId;
                        tQuery.Add(cashTransaction);
                    }
                }
                else
                {
                    accountsLoaded = db.Accounts.Where(x => x.EntityId == entityId && x.Active == true && x.CashFlowForeCast == true).ToList();
                    var startingRs = db.Transactions.Where(x => x.EntityId == entityId).Join(db.Accounts.Where(x => x.Active == true && x.CashFlowForeCast == true),
                                     t => t.AccountId, a => a.Id, (t, a) => new { t.Date, t.Deposit, t.Withdrawal, t.AccountId, t.EntityId }).ToList();
                    var rs = startingRs.Join(db.Entities.Where(x => x.Active == true), t => t.EntityId, a => a.Id, (t, a) => new { t.Date, t.Deposit, t.Withdrawal, t.AccountId, t.EntityId }).ToList();

                    foreach (var r in rs)
                    {
                        var cashTransaction = new CashTransaction();
                        if (r.Date != null) cashTransaction.Date = (DateTime)r.Date;
                        cashTransaction.Deposit = r.Deposit == null ? 0 : (double)r.Deposit;
                        cashTransaction.Withdrawal = r.Withdrawal == null ? 0 : (double)r.Withdrawal;
                        cashTransaction.AccountId = r.AccountId;
                        cashTransaction.EntityId = r.EntityId;
                        tQuery.Add(cashTransaction);
                    }
                }

                transactionsLoaded = tQuery.Where(x => x.Date <= DateTime.Today).ToList();
                var transactionsQuery = from transaction in transactionsLoaded
                                        join account in accountsLoaded on transaction.AccountId equals account.Id into transactionsFiltered
                                        select new { transaction.Date, transaction.Deposit, transaction.Withdrawal, transaction.AccountId };
                var transactionsFilteredLoaded = transactionsQuery.ToList();
                double? currentBalance;
                var transactions = transactionsFilteredLoaded.Select(o => new { o.Date.Date, o.Deposit, o.Withdrawal });
                currentBalance = transactions.Sum(p => (p.Deposit ?? 0) - (p.Withdrawal ?? 0));
                return currentBalance ?? 0;
            }
        }
    }
}