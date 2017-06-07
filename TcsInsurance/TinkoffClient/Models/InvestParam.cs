namespace TinkoffClient.Models
{
    public class InvestParam
    {
        /// <summary>
        /// Идентификатор стратегии
        /// </summary>
        public string strategyId { get; set; }
        /// <summary>
        /// Название стратегии для клиента
        /// </summary>
        public string strategy { get; set; }
        /// <summary>
        /// Периодичность уплаты страховых
        /// </summary>
        public Periodicity contributionsPeriodicity { get; set; }
        /// <summary>
        /// Периодичность 
        /// </summary>
        public Periodicity paymentsPeriodicities { get; set; }
        /// <summary>
        /// Сроков действия договора страхования в годах
        /// </summary>
        public int policyTerm { get; set; }
        /// <summary>
        /// Гарантированный возврат взноса (по умолчанию 100%)
        /// </summary>
        public decimal coverCapital { get; set; }
        /// <summary>
        /// Валюта инвест. части договора страхования 
        /// </summary>
        public string currencyInvest { get; set; }
        /// <summary>
        /// Коэффициент участия
        /// </summary>
        public decimal coefficient { get; set; }
    }
}