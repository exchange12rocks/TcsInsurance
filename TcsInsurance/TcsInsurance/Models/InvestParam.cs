namespace TcsInsurance.Models
{
    public class InvestParam
    {
        public string StrategyId { get; set; }
        public string Strategy { get; set; }
        public int PeriodicityOfcontributions { get; set; }
        public string PolicyTerm { get; set; }
        public string Currency { get; set; }
        public decimal Refund { get; set; }
        public string CurrencyInvest { get; set; }
        public decimal Coefficient { get; set; }
    }
}