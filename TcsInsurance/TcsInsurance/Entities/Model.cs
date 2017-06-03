namespace TcsInsurance.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model : DbContext
    {
        public Model()
            : base("name=TinkoffInsuranceModel")
        {
        }

        public virtual DbSet<TickerHistoryValue> TickerHistoryValues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TickerHistoryValue>()
                .Property(e => e.Value)
                .HasPrecision(18, 9);
        }
    }
}
