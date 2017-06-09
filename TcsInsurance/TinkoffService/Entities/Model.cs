namespace TinkoffService.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model : DbContext
    {
        public Model()
            : base("name=Model")
        {
        }

        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<TickerHistoryValue> TickerHistoryValues { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TickerHistoryValue>()
                .Property(e => e.Value)
                .HasPrecision(18, 9);
        }
    }
}
