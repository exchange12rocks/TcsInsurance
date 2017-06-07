using System;
namespace TinkoffClient.Models
{
    public class PaymentsPlanItem
    {
        /// <summary>
        /// Дата, когда можно получить инвестдоход
        /// </summary>
        public DateTime date { get; set; }
        /// <summary>
        /// Сумма накопленного ДИД на дату
        /// </summary>
        public decimal sum { get; set; }
    }
}