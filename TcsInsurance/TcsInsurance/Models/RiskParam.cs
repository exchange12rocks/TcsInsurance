namespace TcsInsurance.Models
{
    public class RiskParam
    {
        /// <summary>
        /// Идентификатор риска
        /// </summary>
        public string RiskId { get; set; }
        /// <summary>
        /// Название риска
        /// </summary>
        public string RiskText { get; set; }
        /// <summary>
        /// Выплата по риску
        /// </summary>
        public string Sum { get; set; }
    }
}