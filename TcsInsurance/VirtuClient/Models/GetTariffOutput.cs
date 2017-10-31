namespace VirtuClient.Models
{
    public class GetTariffOutput
    {
        public GetTariffValueOutput InsPeriod { get; set; }
        public GetTariffValueOutput Year { get; set; }
        public GetTariffValueOutput InsSum { get; set; }
    }
    public class GetTariffValueOutput
    {
        public string value { get; set; }
    }
    public class GetInsSumsOutput
    {
        public GetTariffValueOutput InsPeriod { get; set; }
        public GetTariffValueOutput InsSum { get; set; }
    }
    public class GetMinInsSumsOutput
    {
        public GetTariffValueOutput InsPeriod { get; set; }
        public GetTariffValueOutput MaxInsSum { get; set; }
        public GetTariffValueOutput MinInsSum { get; set; }
    }
}