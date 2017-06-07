namespace TinkoffClient.Models
{
    public class CreatePolicyRequest
    {
        /// <summary>
        /// Id договора в системе тинькофф
        /// </summary>
        public string applicationId { get; set; }
        /// <summary>
        /// Id продукта, который хочет приобрести клиент
        /// </summary>
        public string productId { get; set; }
        /// <summary>
        /// Валюта договора
        /// </summary>
        public string currency { get; set; }
        /// <summary>
        /// Срок действия договора
        /// </summary>
        public int policyTerm { get; set; }
        /// <summary>
        /// Периодичность уплаты страховых взносов по договору
        /// </summary>
        public Periodicity periodicityOfContributions { get; set; }
        /// <summary>
        /// Периодичность выплаты ДИД
        /// </summary>
        public Periodicity periodicitiesOfPayments { get; set; }
        /// <summary>
        /// Id Стратегии
        /// </summary>
        public string strategyId { get; set; }
        /// <summary>
        /// Защита капитала в%
        /// </summary>
        public decimal coverCapital { get; set; }
        /// <summary>
        /// Сумма вложений
        /// </summary>
        public decimal amount { get; set; }
        /// <summary>
        /// Страхователь
        /// </summary>
        public Person insurant { get; set; }
        /// <summary>
        /// Застрахованный
        /// </summary>
        public Person insured { get; set; }
        /// <summary>
        /// Признак «Страхователь совпадает с застрахованным», True - Страхователь совпадает с застрахованным, False - Страхователь не совпадает с застрахованным(в этом случае должны быть данные по застрахованному)
        /// </summary>
        public bool insurantIsInsured { get; set; }
    }
}