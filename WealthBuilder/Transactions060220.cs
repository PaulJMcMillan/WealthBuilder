//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WealthBuilder
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transactions060220
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Deposit { get; set; }
        public Nullable<decimal> Withdrawal { get; set; }
        public Nullable<bool> Cleared { get; set; }
        public Nullable<int> CheckNumber { get; set; }
        public Nullable<bool> Reconciled { get; set; }
        public string Notes { get; set; }
        public Nullable<int> AccountId { get; set; }
        public Nullable<int> EntityId { get; set; }
        public Nullable<int> TaxFormId { get; set; }
        public Nullable<int> TaxCategoryId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> AssetId { get; set; }
        public Nullable<int> ContractorId { get; set; }
    }
}