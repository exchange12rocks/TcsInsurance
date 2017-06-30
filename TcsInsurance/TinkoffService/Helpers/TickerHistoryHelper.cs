using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<TickerHistoryValue> LoadFromService(string strategyCode)
        {
            WSClient client = new WSClient();
            client.ClientCredentials.UserName.UserName = ConfigurationManagerHelper.Default.WSUserName;
            client.ClientCredentials.UserName.Password = ConfigurationManagerHelper.Default.WSPassword;
            var result = client.getStrategyTickers(strategyCode);
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
            foreach (TickerHistoryValue value in values)
            {
                TickerHistoryValue oldTickerHistoryValue = oldTickerHistoryValues.SingleOrDefault(A => A.Ticker == value.Ticker && A.Date == value.Date);
                if (oldTickerHistoryValue != null)
                {
                    oldTickerHistoryValue.Value = oldTickerHistoryValue.Value;
                }
                else
                {
                    this.db.TickerHistoryValues.Add(value);
                }
            }
            this.db.SaveChanges();
        }
    }
}