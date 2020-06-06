//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;

namespace WealthBuilder
{
    public class CashFlowForecastEntry
    {
        private DateTime nextPayDate;
        private decimal amount;

        public CashFlowForecastEntry(DateTime nextPayDate, decimal amount)
        {
            this.nextPayDate = nextPayDate;
            this.amount = amount;
        }

        public DateTime Date { get { return nextPayDate; } set { nextPayDate = value; } }
        public decimal Amount { get { return amount; } set { amount = value; } }

    }
}
