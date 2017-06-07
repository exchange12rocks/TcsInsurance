namespace TinkoffClient.Models
{
    public class RedemptionAmount
    {
        /// <summary>
        /// Год действия
        /// </summary>
        public int year { get; set; }
        /// <summary>
        /// Периодичность уплаты страховых
        /// </summary>
        public Periodicity contributionsPeriodicity { get; set; }
        /// <summary>
        /// Периодичность выплаты ДИД
        /// </summary>
        public Periodicity paymentsPeriodicities { get; set; }
        /// <summary>
        /// Валюта договора
        /// </summary>
        public string currency { get; set; }
        /// <summary>
        /// Сроков действия договора страхования в годах
        /// </summary>
        public int policyTerm { get; set; }
        /// <summary>
        /// Выкупная сумма в %
        /// </summary>
        public decimal sum { get; set; }
    }
}