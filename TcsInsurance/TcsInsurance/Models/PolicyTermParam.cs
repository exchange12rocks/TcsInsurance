namespace TcsInsurance.Models
{
    public class PolicyTermParam
    {
        /// <summary>
        /// Периодичность уплаты страховых Возможные значения: 0 – единовременно, 1 - раз в год, 2 - раз в полгода, 4 – ежеквартально, 12 - ежемесячно
        /// </summary>
        public int PeriodicityOfcontributions { get; set; }
        /// <summary>
        /// Сроков действия договора страхования в годах
        /// </summary>
        public string PolicyTerm { get; set; }
    }
}