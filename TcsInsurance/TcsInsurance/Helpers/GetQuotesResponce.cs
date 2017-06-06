using System;
using System.Collections.Generic;
namespace TcsInsurance.Helpers
{
    public class GetQuotesResponce
    {
        public List<Quote> quotes { get; set; }
        public DateTime date { get; set; }
    }
    public class Quote
    {
        public decimal price { get; set; }
        public DateTime date { get; set; }
    }
}
