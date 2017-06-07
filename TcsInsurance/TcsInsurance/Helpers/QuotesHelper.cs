using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TcsInsurance.Entities;
using TinkoffClient.Models;
namespace TcsInsurance.Helpers
{
    public class QuotesHelper
    {
        private VirtuClient.VirtuClient virtuClient;
        public QuotesHelper(VirtuClient.VirtuClient virtuClient)
        {
            this.virtuClient = virtuClient;
        }
        public GetQuotesResponse GetQuotes(GetQuotesRequest parameter)
        {
            Dictionary<string, string> mapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            mapping.Add("МЕДИЦИНА БУДУЩЕГО", "SXDP");
            mapping.Add("МИРОВЫЕ БРЕНДЫ", "XLP");
            mapping.Add("ОБЛИГАЦИИ МИРОВЫХ РЫНКОВ", "CBKIGINF");
            mapping.Add("ОБЛИГАЦИИ MAX", "CBKIGINF");
            var strategies = this.virtuClient.StrategiesSearch(new VirtuClient.Models.StrategiesSearchInput()
            {
                IsActive = true,
                ReadRedefined = true,
            });
            var strategy = strategies.Single(A => A.ID == parameter.strategyId);
            string index = mapping[strategy.InvestmentStrategyRaw];
            using (var db = new Model())
            {
                var queryable = db.TickerHistoryValues.Where(A => A.Ticker == index);
                return new GetQuotesResponse()
                {
                    date = queryable.Max(A => A.Date),
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