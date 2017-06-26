using Newtonsoft.Json;
using System;
using System.Linq;
using tinkoff.ru.partners.insurance.investing.types;
using TinkoffService.Entities;
namespace TinkoffService.Helpers
{
    internal class quotesMapping
    {
        public string quote { get; set; }
        public string strategy { get; set; }
    }
    public class QuotesHelper
    {
        private VirtuClient.VirtuClient virtuClient;
        public QuotesHelper(VirtuClient.VirtuClient virtuClient)
        {
            this.virtuClient = virtuClient;
        }
        public GetQuotesResponse GetQuotes(GetQuotesRequest parameter)
        {
            var strategies = this.virtuClient.StrategiesSearch(new VirtuClient.Models.StrategiesSearchInput()
            {
                IsActive = true,
                ReadRedefined = true,
            });
            var strategy = strategies.First(A => string.Equals(A.ID, parameter.strategyId, StringComparison.OrdinalIgnoreCase));
            using (var db = new Model())
            {




                TickerHistoryHelper helper = new TickerHistoryHelper(db);
                var a = helper.LoadFromService(strategy.ID);





                    var mapping = db.Settings.Where(A => A.Key == "quotesMapping")
                    .Select(A => A.Value)
                    .ToArray()
                    .Select(A => JsonConvert.DeserializeObject<quotesMapping>(A))
                    .ToDictionary(A => A.strategy, A => A.quote, StringComparer.OrdinalIgnoreCase);
                string index = mapping[strategy.InvestmentStrategyRaw];
                var queryable = db.TickerHistoryValues.Where(A => A.Ticker == index);
                return new GetQuotesResponse()
                {
                    date = queryable.Max(A => A.Date),
                    dateSpecified = true,
                    quotes = queryable.Where(A => A.Date >= parameter.dateFrom && A.Date < parameter.dateTo).Select(A => new quote()
                    {
                        price = A.Value,
                        date = A.Date,
                    }).ToArray(),
                };
            }
        }
    }
}