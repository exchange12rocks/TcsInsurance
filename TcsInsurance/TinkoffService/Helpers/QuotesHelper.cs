using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using tinkoff.ru.partners.insurance.investing.types;
using TinkoffClient;
using TinkoffService.Entities;
namespace TinkoffService.Helpers
{
    public class QuotesHelper
    {
        static readonly string getQuotesLastUpdateDateTimeSettingName = "quotesLastUpdateDateTime";
        static QuotesHelper()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
            (se, cert, chain, sslerror) =>
            {
                return true;
            };
        }
        private VirtuClient.VirtuClient virtuClient;
        public Action<Core.Log> Log;
        public QuotesHelper(VirtuClient.VirtuClient virtuClient)
        {
            this.virtuClient = virtuClient;
        }
        private DateTime? getQuotesLastUpdateDateTime(Model db, string ticker)
        {
            Setting quotesLastUpdateDateTimeSetting = db.Settings.SingleOrDefault(A => A.Key == getQuotesLastUpdateDateTimeSettingName);
            if(quotesLastUpdateDateTimeSetting != null && quotesLastUpdateDateTimeSetting.Value != null)
            {
                Dictionary<string, DateTime?> quotesLastUpdateDateTime = JsonConvert.DeserializeObject<Dictionary<string, DateTime?>>(quotesLastUpdateDateTimeSetting.Value);
                if(quotesLastUpdateDateTime != null && quotesLastUpdateDateTime.Keys.Contains(ticker, StringComparer.OrdinalIgnoreCase))
                {
                    return quotesLastUpdateDateTime[ticker];
                }
            }
            return null;
        }
        private void setQuotesLastUpdateDateTime(Model db, string ticker, DateTime? lastUpdateDateTime)
        {
            Setting quotesLastUpdateDateTimeSetting = db.Settings.SingleOrDefault(A => A.Key == getQuotesLastUpdateDateTimeSettingName);
            if(quotesLastUpdateDateTimeSetting == null)
            {
                db.Settings.Add(new Setting()
                {
                    Key = getQuotesLastUpdateDateTimeSettingName,
                    Value = JsonConvert.SerializeObject(new Dictionary<string, DateTime?>()
                    {
                        { ticker, lastUpdateDateTime },
                    }),
                });
            }
            else
            {
                Dictionary<string, DateTime?> quotesLastUpdateDateTime = JsonConvert.DeserializeObject<Dictionary<string, DateTime?>>(quotesLastUpdateDateTimeSetting.Value);
                if(quotesLastUpdateDateTime == null)
                {
                    quotesLastUpdateDateTime = new Dictionary<string, DateTime?>();
                }
                quotesLastUpdateDateTime[ticker] = lastUpdateDateTime;
                quotesLastUpdateDateTimeSetting.Value = JsonConvert.SerializeObject(quotesLastUpdateDateTime);
            }
            db.SaveChanges();
        }
        public GetQuotesResponse GetQuotes(GetQuotesRequest parameter)
        {
            var strategies = this.virtuClient.StrategiesSearch(new VirtuClient.Models.StrategiesSearchInput()
            {
                IsActive = true,
                ReadRedefined = true,
            });
            var strategy = strategies.Single(A => A.ID, parameter.strategyId);
            using (Model db = new Model())
            {
                string ticker = strategy.InvestmentStrategyRaw;
                DateTime? quotesLastUpdateDateTimeSetting = this.getQuotesLastUpdateDateTime(db, ticker);
                if (!quotesLastUpdateDateTimeSetting.HasValue || (DateTime.Now - quotesLastUpdateDateTimeSetting.Value).TotalHours >= 1)
                {
                    TickerHistoryHelper tickerHistoryHelper = new TickerHistoryHelper(db)
                    {
                        Log = this.Log,
                    };
                    var tickers = tickerHistoryHelper.LoadFromService(ticker);
                    tickerHistoryHelper.AddOrUpdate(tickers);
                    this.setQuotesLastUpdateDateTime(db, ticker, DateTime.Now);
                    db.SaveChanges();
                }
                var queryable = db.TickerHistoryValues.Where(A => A.Ticker == ticker);
                return new GetQuotesResponse()
                {
                    date = this.getQuotesLastUpdateDateTime(db, ticker).Value,
                    dateSpecified = true,
                    quotes = queryable.Where(A => A.Date >= parameter.dateFrom && A.Date <= parameter.dateTo).Select(A => new quote()
                    {
                        price = A.Value,
                        date = A.Date,
                    }).ToArray(),
                };
            }
        }
    }
}