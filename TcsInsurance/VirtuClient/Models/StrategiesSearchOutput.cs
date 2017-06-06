using System;
namespace VirtuClient.Models
{
    public class StrategiesSearchOutput
    {
        public string type { get; set; }
        public int tid { get; set; }
        public string action { get; set; }
        public string method { get; set; }
        public StrategiesSearchResultOutput result { get; set; }
    }
    public class StrategiesSearchResultOutput
    {
        public bool success { get; set; }
        public string message { get; set; }
        public StrategiesSearchDataOutput[] data { get; set; }
        public int totalCount { get; set; }
    }
    public class StrategiesSearchDataOutput
    {
        public string VersionCode { get; set; }
        public string OptionID { get; set; }
        public string InvestmentStrategy { get; set; }
        public string BaseIndex { get; set; }
        public string OptionPeriod { get; set; }
        public string Coefficient { get; set; }
        public string OptionType { get; set; }
        public DateTime InvestmentStartDate { get; set; }
        public string OptionKind { get; set; }
        public string OptionCurrency { get; set; }
        public DateTime? SellingStartDate { get; set; }
        public DateTime? SellingEndDate { get; set; }
        public bool IsActive { get; set; }
        public decimal? BaseIndexOnStartDate { get; set; }
        public decimal? CurrencyRate { get; set; }
        public decimal InsuranceCurrencyRate { get; set; }
        public decimal? OptionPrice { get; set; }
        public decimal OptionPriceRUR { get; set; }
        public decimal? OptionValue { get; set; }
        public decimal OptionValueRUR { get; set; }
        public string GFPart { get; set; }
        public string FIPart { get; set; }
        public decimal Profitability { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Date1 { get; set; }
        public string Date2 { get; set; }
        public string Date3 { get; set; }
        public string Date4 { get; set; }
        public string InvestmentStrategyRaw { get; set; }
        public string OptionTypeRaw { get; set; }
        public string OptionCurrencyRaw { get; set; }
        public decimal RateOfReturn { get; set; }
        public bool IsArchive { get; set; }
        public bool isNotBought { get; set; }
        public string ID { get; set; }
    }
}