namespace TinkoffClient.Models
{
    public class Address
    {
        /// <summary>
        /// Тип адреса
        /// </summary>
        public string addressType { get; set; }
        /// <summary>
        /// Страна
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// Индекс
        /// </summary>
        public string index { get; set; }
        /// <summary>
        /// Регион
        /// </summary>
        public string region { get; set; }
        /// <summary>
        /// Район
        /// </summary>
        public string district { get; set; }
        /// <summary>
        /// Город
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// Населенный пункт
        /// </summary>
        public string locality { get; set; }
        /// <summary>
        /// Улица
        /// </summary>
        public string street { get; set; }
        /// <summary>
        /// Дом
        /// </summary>
        public string house { get; set; }
        /// <summary>
        /// Строение
        /// </summary>
        public string building { get; set; }
        /// <summary>
        /// Номер квартиры или комнаты
        /// </summary>
        public string flat { get; set; }
    }
}