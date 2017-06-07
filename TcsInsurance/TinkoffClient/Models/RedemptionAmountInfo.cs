using System;
namespace TinkoffClient.Models
{
    public class RedemptionAmountInfo
    {
        /// <summary>
        /// Дата, до которой будет актуальна данная выкупная сумма
        /// </summary>
        public DateTime date { get; set; }
        /// <summary>
        /// Выкупная сумма
        /// </summary>
        public decimal sum { get; set; }
    }
}