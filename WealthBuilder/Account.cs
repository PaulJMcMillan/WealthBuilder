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
    
    public partial class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> EntityId { get; set; }
        public Nullable<bool> CashFlowForeCast { get; set; }
        public Nullable<bool> DefaultAccount { get; set; }
    }
}
