//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;

namespace WealthBuilder.Code
{
    public class Transaction
    {
        public long Id { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public decimal? Deposit { get; set; }
        public decimal? Withdrawal { get; set; }
        public bool? Cleared { get; set; }
        public int? CheckNumber { get; set; }
        public bool? Reconciled { get; set; }
        public bool? AccountId { get; set; }
    }
}