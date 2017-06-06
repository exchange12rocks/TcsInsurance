namespace VirtuClient.Models
{
    public class CalculateInput
    {
        /// <summary>
        /// Сумма страховой премии
        /// </summary>
        public string Premium { get; set; }
        /// <summary>
        /// Идентификатор продукта
        /// </summary>
        public string ProductID { get; set; }
    }
}