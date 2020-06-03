
namespace WealthBuilder
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class WBEntities : DbContext
    {
        public WBEntities()
            : base("name=WBEntities")
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
