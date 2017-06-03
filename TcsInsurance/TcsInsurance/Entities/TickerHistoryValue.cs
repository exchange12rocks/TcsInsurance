namespace TcsInsurance.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TickerHistoryValue
    {
        [Key]
        [Column(Order = 0, TypeName = "date")]
        public DateTime Date { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(40)]
        public string Ticker { get; set; }

        public decimal Value { get; set; }
    }
}
