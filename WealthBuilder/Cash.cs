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

    public static decimal GetAccountBalance(long accountId)
        {
            using (var db = new WBEntities())
            {
                var rs = db.Transactions.Where(x => x.AccountId == accountId).ToList();
                decimal currentBalance = rs.Sum(p => p.Deposit - p.Withdrawal);
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
                var transactions = db.Transactions.Where(x=>x.AccountId == account.Id && x.Date < cutOffDateTime).ToList();
                decimal currentBalance= transactions.Sum(p => p.Deposit - p.Withdrawal);
                return currentBalance;
            }
        }
    }
}