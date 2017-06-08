namespace TcsInsurance.Entities
{
    using System.Data.Entity;

    public partial class Model : DbContext
    {
        public Model()
            : base("name=TinkoffInsuranceModel")
        {
        }

        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<TickerHistoryValue> TickerHistoryValues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TickerHistoryValue>()
                .Property(e => e.Value)
                .HasPrecision(18, 9);
        }
    }
}
