namespace TinkoffClient.Models
{
    public class Periodicity
    {
        /// <summary>
        /// Периодичность 
        /// Возможные значения:
        /// 0 – единовременно
        /// 1 - раз в год
        /// 2 - раз в полгода
        /// 4 – ежеквартально
        /// 12 - ежемесячно
        /// </summary>
        public int periodicitiy { get; set; }
    }
}