//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Linq;

namespace WealthBuilder
{
    internal class AccountRepository
    {
        public AccountRepository()
        {
        }

        internal Account GetCashFlowForecastAccount()
        {
            using (var db = new WBEntities())
            {
                var account = db.Accounts.Where(x => x.EntityId == CurrentEntity.Id && x.Active == true && x.CashFlowForeCast == true).FirstOrDefault();
                //todo: check for multiple cash flow accounts.  if so, tell the user only one can be setup.
                return account;
            }
        }
    }
}