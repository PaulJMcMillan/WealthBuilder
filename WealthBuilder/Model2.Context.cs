﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WealthBuilderEntities : DbContext
    {
        public WealthBuilderEntities()
            : base("name=WealthBuilderEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C1099Contractors> C1099Contractors { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<Budget> Budgets { get; set; }
        public virtual DbSet<CashFlowForecastData> CashFlowForecastDatas { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Entity> Entities { get; set; }
        public virtual DbSet<Frequency> Frequencies { get; set; }
        public virtual DbSet<Inflow> Inflows { get; set; }
        public virtual DbSet<ProjectedCashBalance> ProjectedCashBalances { get; set; }
        public virtual DbSet<Reminder> Reminders { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<TaxCategory> TaxCategories { get; set; }
        public virtual DbSet<TaxForm> TaxForms { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
    }
}
