namespace TcsInsurance.Models
{
    public class GetProductsInputParam
    {
        /// <summary>
        /// Имя системы
        /// </summary>
        public string SystemName { get; set; }
        /// <summary>
        /// Логин системы Tinkoff
        /// </summary>
        public string LoginUser { get; set; }
        /// <summary>
        /// Пароль системы Tinkoff
        /// </summary>
        public string UserPass { get; set; }
    }
}