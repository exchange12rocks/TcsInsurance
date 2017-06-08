namespace TcsInsurance.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class TickerHistoryValue
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(40)]
        public string Ticker { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "date")]
        public DateTime Date { get; set; }

        public decimal Value { get; set; }
    }
}
