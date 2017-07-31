﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinkoffService.Entities;
using TinkoffService.TikerHistoryServiceReference;
namespace TinkoffService.Helpers
{
    public class TickerHistoryHelper
    {
        private Model db;
        public TickerHistoryHelper(Model model)
        {
            this.db = model;
        }
        public Action<Core.Log> Log;
        public IEnumerable<TickerHistoryValue> LoadFromService(string strategyCode)
        {
            DateTime start = DateTime.Now;
            WSClient client = new WSClient();
            client.ClientCredentials.UserName.UserName = ConfigurationManagerHelper.Default.WSUserName;
            client.ClientCredentials.UserName.Password = ConfigurationManagerHelper.Default.WSPassword;
            var result = client.getStrategyTickers(strategyCode);
            this.Log?.Invoke(new Core.Log()
            {
                Name = "TickerHistoryHelper.LoadFromService",
                Start = start,
                Input = strategyCode,
                Output = JsonConvert.SerializeObject(result),
            });
            return result.tickers.Select(A => new TickerHistoryValue()
            {
                Ticker = strategyCode,
                Date = A.date,
                Value = (decimal)A.rate,
            });
        }
        public void AddOrUpdate(IEnumerable<TickerHistoryValue> values)
        {
            TickerHistoryValue[] oldTickerHistoryValues = this.db.TickerHistoryValues.ToArray();
            StringBuilder logString = new StringBuilder();
            DateTime start = DateTime.Now;
            foreach (TickerHistoryValue value in values)
            {
                TickerHistoryValue oldTickerHistoryValue = oldTickerHistoryValues.SingleOrDefault(A => A.Ticker == value.Ticker && A.Date == value.Date);
                if (oldTickerHistoryValue != null)
                {
                    oldTickerHistoryValue.Value = oldTickerHistoryValue.Value;
                    logString.AppendLine($"update {value.Ticker} {value.Date.ToString("yyyy-MM-dd")}");
                }
                else
                {
                    this.db.TickerHistoryValues.Add(value);
                    logString.AppendLine($"insert {value.Ticker} {value.Date.ToString("yyyy-MM-dd")}");
                }
            }
            this.db.SaveChanges();
            this.Log?.Invoke(new Core.Log()
            {
                Name = "TickerHistoryHelper.AddOrUpdate",
                Start = start,
                Output = logString.ToString(),
            });
        }
    }
}