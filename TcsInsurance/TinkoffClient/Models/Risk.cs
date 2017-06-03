namespace TinkoffClient.Models
{
    public class Risk
    {
        /// <summary>
        /// Идентификатор риска
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// Название риска
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// Выплата по риску
        /// </summary>
        public string sum { get; set; }
    }
}