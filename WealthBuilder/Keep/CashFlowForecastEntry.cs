//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;

namespace WealthBuilder
{
    public class CashFlowForecastEntry
    {
        private DateTime nextPayDate;
        private double amount;

        public CashFlowForecastEntry(DateTime nextPayDate, double amount)
        {
            this.nextPayDate = nextPayDate;
            this.amount = amount;
        }

        public DateTime Date { get { return nextPayDate; } set { nextPayDate = value; } }
        public double Amount { get { return amount; } set { amount = value; } }

    }
}
