using System;
namespace TcsInsurance.Helpers
{
    public class GetQuotesRequest
    {
        public string productId { get; set; }
        public string strategyId { get; set; }
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }
    }
}
