using System.Collections.Generic;
namespace TcsInsurance.Models
{
    public class GetProductsOutputParam
    {
        /// <summary>
        /// ID продукта в СК
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// Название продукта
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Краткое описание продукта
        /// </summary>
        public string ShortDescription { get; set; }
        /// <summary>
        /// Полное описание полиса
        /// </summary>
        public string FullDescription { get; set; }
        /// <summary>
        /// Длительность договора (один срок-один продукт)
        /// </summary>
        public string PolicyTerm { get; set; }
        /// <summary>
        /// Минимальный страховой взнос
        /// </summary>
        public string MinPremiums { get; set; }
        /// <summary>
        /// Максимальный страховой взнос
        /// </summary>
        public string MaxPremiums { get; set; }
        /// <summary>
        /// Сроки действия договора страхования в годах
        /// </summary>
        public List<PolicyTermParam> PolicyTerms { get; set; }
        /// <summary>
        /// Периодичность уплаты страховых взносов
        /// </summary>
        public List<int> PeriodicitiesOfcontributions { get; set; }
        /// <summary>
        /// Страховые риски
        /// </summary>
        public List<RiskParam> InsuranceRisks { get; set; }
        /// <summary>
        /// Выкупные суммы
        /// </summary>
        public List<RedemptionAmountParam> RedemptionAmounts { get; set; }
        /// <summary>
        /// Инвестиционные параметры
        /// </summary>
        public List<InvestParam> InvestParams { get; set; }
    }
}
