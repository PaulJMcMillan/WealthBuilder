//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System.Collections.Generic;
using System.Linq;

namespace WealthBuilder.Code
{
    internal class Transactions
    {
        internal static List<WealthBuilder.Transaction> GetUnclearedTransactionsByAccountId(long accountId)
        {
            List<WealthBuilder.Transaction> transactionsList = new List<WealthBuilder.Transaction>();

            using (var db = new WBEntities())
            {
                var account = db.Accounts.Where(x => x.Id == accountId && x.Active == true).FirstOrDefault();
                if (account == null) return transactionsList;
                var entity = db.Entities.Where(x => x.Id == account.EntityId && x.Active == true);
                if (entity == null) return transactionsList;
                return db.Transactions.Where(x => (x.Cleared == null || x.Cleared == false) && x.AccountId == accountId).ToList();
            }
        }
    }
}