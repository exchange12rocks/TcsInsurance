using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TinkoffService.Entities;
using TinkoffService.Helpers.Excel;
namespace TinkoffService.Helpers
{
    public class TickerHistoryHelper
    {
        private Model db;
        public TickerHistoryHelper(Model model)
        {
            this.db = model;
        }
        public IEnumerable<TickerHistoryValue> GetFromExcel(Stream stream)
        {
            List<TickerHistoryValue> result = new List<TickerHistoryValue>();
            IWorkbook workbook = WorkbookFactory.Create(stream);
            for (int sheetIndex = 0; sheetIndex < workbook.NumberOfSheets; ++sheetIndex)
            {
                ISheet sheet = workbook.GetSheetAt(sheetIndex);
                string tickerName = sheet.SheetName;
                result.AddRange(Range.CreateByEndInclusive(sheet.FirstRowNum, sheet.LastRowNum)
                    .Select(index => sheet.GetRow(index))
                    .Select(row => new { Date = ParserHelper.Default.GetDateTimeOrNull(row, 0), Value = ParserHelper.Default.GetDoubleOrNull(row, 1) })
                    .Where(record => record.Date != null && record.Value != null)
                    .Select(record => new TickerHistoryValue()
                    {
                        Ticker = tickerName,
                        Date = record.Date.Value,
                        Value = (decimal)record.Value.Value,
                    }));
            }
            return result;
        }
        public void AddOrUpdate(IEnumerable<TickerHistoryValue> values)
        {
            this.db.Database.UsingTransaction(() =>
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
            });
        }
    }
}