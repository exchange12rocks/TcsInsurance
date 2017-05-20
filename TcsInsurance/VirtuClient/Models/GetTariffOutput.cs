namespace VirtuClient.Models
{
    public class GetTariffOutput
    {
        public GetTariffValueOutput InsPeriod { get; set; }
        public GetTariffValueOutput Year { get; set; }
        public GetTariffValueOutput InsSum { get; set; }
    }
}