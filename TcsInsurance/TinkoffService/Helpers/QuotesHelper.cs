using Newtonsoft.Json;
using System;
using System.Linq;
using tinkoff.ru.partners.insurance.investing.types;
using TinkoffService.Entities;
using TinkoffService.TikerHistoryServiceReference;
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
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
            (se, cert, chain, sslerror) =>
            {
                return true;
            };
            WSClient client = new WSClient();
            client.ClientCredentials.UserName.UserName = "tinkoff_svc";
            client.ClientCredentials.UserName.Password = "tinkoff_svc_12345";
            string productId = "50de0164-cfb5-4843-a166-43a8c0b41084";

            var a = this.virtuClient.GetStrategies(productId);
            var b = this.virtuClient.StrategiesSearch(new VirtuClient.Models.StrategiesSearchInput()
            {
                IsActive = true,
                ReadRedefined = true,
            });
            foreach(var aa in a)
            {
                var result1 = client.getStrategyTickers(aa.ID);
                if(result1.tickers?.Any() == true)
                {
                    throw new Exception();
                }
                var result2 = client.getStrategyTickers(aa.Name);
                if (result2.tickers?.Any() == true)
                {
                    throw new Exception();
                }
                var result3 = client.getStrategyTickers(aa.DisplayName);
                if (result3.tickers?.Any() == true)
                {
                    throw new Exception();
                }
            }
            foreach (var bb in b)
            {
                var result1 = client.getStrategyTickers(bb.ID);
                if (result1.tickers?.Any() == true)
                {
                    throw new Exception();
                }
                var result2 = client.getStrategyTickers(bb.InvestmentStrategy);
                if (result2.tickers?.Any() == true)
                {
                    throw new Exception();
                }
                var result3 = client.getStrategyTickers(bb.InvestmentStrategyRaw);
                if (result3.tickers?.Any() == true)
                {
                    throw new Exception();
                }
                var result4 = client.getStrategyTickers(bb.OptionID);
                if (result4.tickers?.Any() == true)
                {
                    throw new Exception();
                }
                var result5 = client.getStrategyTickers(bb.BaseIndex);
                if (result5.tickers?.Any() == true)
                {
                    throw new Exception();
                }
            }

            return new GetQuotesResponse()
            {
                date = DateTime.Today,
                dateSpecified = true,
                /*quotes = result.tickers.Select(A => new quote()
                {
                    date = A.date,
                    price = (decimal)A.rate,
                }).ToArray(),*/
            };
        }
    }
}