using System.Collections.Generic;
namespace TinkoffClient.Models
{
    public class Product
    {
        /// <summary>
        /// ID продукта в СК
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// Название продукта
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Краткое описание продукта
        /// </summary>
        public string shortDescription { get; set; }
        /// <summary>
        /// Полное описание полиса
        /// </summary>
        public string fullDescription { get; set; }
        /// <summary>
        /// Валюта договора
        /// </summary>
        public Currency currency { get; set; }
        /// <summary>
        /// Минимальный страховой взнос
        /// </summary>
        public string minPremiums { get; set; }
        /// <summary>
        /// Максимальный страховой взнос
        /// </summary>
        public string maxPremiums { get; set; }
        /// <summary>
        /// Сроки действия договора страхования в годах
        /// </summary>
        public List<PolicyTerm> policyTermOptions { get; set; }
        /// <summary>
        /// Периодичность уплаты страховых взносов
        /// </summary>
        public List<Periodicity> contributionsPeriodicityOptions { get; set; }
        /// <summary>
        /// Периодичность выплаты ДИД
        /// </summary>
        public List<Periodicity> paymentsPeriodicityOptions { get; set; }
        /// <summary>
        /// Страховые риски
        /// </summary>
        public List<Risk> insuranceRisks { get; set; }
        /// <summary>
        /// Выкупные суммы
        /// </summary>
        public List<RedemptionAmount> redemptionAmounts { get; set; }
        /// <summary>
        /// Инвестиционные параметры
        /// </summary>
        public List<InvestParam> investParams { get; set; }
    }
}