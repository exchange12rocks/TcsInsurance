using System;
using System.Collections.Generic;
namespace TinkoffClient.Models
{
    public class GetPolicyResponse
    {
        /// <summary>
        /// ID продукта
        /// </summary>
        public string productId { get; set; }
        /// <summary>
        /// Наименование продукта
        /// </summary>
        public string productName { get; set; }
        /// <summary>
        /// ID полиса
        /// </summary>
        public string policyId { get; set; }
        /// <summary>
        /// Номер полиса
        /// </summary>
        public string policyNumber { get; set; }
        /// <summary>
        /// Полное описание полиса
        /// </summary>
        public string fullDescription { get; set; }
        /// <summary>
        /// Дата действия полиса с
        /// </summary>
        public DateTime effectiveDate { get; set; }
        /// <summary>
        /// Дата действия полиса по
        /// </summary>
        public DateTime expirationDate { get; set; }
        /// <summary>
        /// Коэффициент участия
        /// </summary>
        public string coefficient { get; set; }
        /// <summary>
        /// Доход инвестиционного дохода (ДИД)
        /// </summary>
        public decimal profitability { get; set; }
        /// <summary>
        /// Статус полиса
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// Валюта полиса 
        /// </summary>
        public string currency { get; set; }
        /// <summary>
        /// Сумма вложений
        /// </summary>
        public decimal amount { get; set; }
        /// <summary>
        /// Гарантированный возврат взноса (в %)
        /// </summary>
        public string coverCapital { get; set; }
        /// <summary>
        /// Стратегия
        /// </summary>
        public string strategy { get; set; }
        /// <summary>
        /// Даты выплаты ДИД 
        /// </summary>
        public List<PaymentsPlanItem> paymentsPlan { get; set; }
        /// <summary>
        /// Страховые риски
        /// </summary>
        public List<Risk> insuranceRisks { get; set; }
        /// <summary>
        /// Выкупные суммы
        /// </summary>
        public List<RedemptionAmountInfo> redemptionAmounts { get; set; }
    }
}