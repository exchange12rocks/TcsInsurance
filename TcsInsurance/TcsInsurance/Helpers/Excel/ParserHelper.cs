using NPOI.SS.UserModel;
using System;
namespace TcsInsurance.Helpers.Excel
{
    public class ParserHelper
    {
        private static ParserHelper _default;
        public static ParserHelper Default
        {
            get
            {
                return _default ?? (_default = new ParserHelper());
            }
        }
        public string GetStringOrNull(IRow row, int cellNum)
        {
            return CellHelper.Default.TryGet(row, CellHelper.Default.GetString, cellNum, () => null);
        }
        public string GetStringOrEmptyString(IRow row, int cellNum)
        {
            return CellHelper.Default.TryGet(row, CellHelper.Default.GetString, cellNum, () => "");
        }
        public bool IsNullOrEmptyString(IRow row, int cellNum)
        {
            return string.IsNullOrEmpty(GetStringOrNull(row, cellNum));
        }
        public DateTime? GetDateTimeOrNull(IRow row, int cellNum)
        {
            return CellHelper.Default.TryGet(row, (A, B) => (DateTime?)CellHelper.Default.GetDate(A, B), cellNum, () => null);
        }
        public DateTime GetDateTime(IRow row, int cellNum)
        {
            return CellHelper.Default.GetDate(row, cellNum);
        }
        public double GetDouble(IRow row, int cellNum)
        {
            return CellHelper.Default.GetNumber(row, cellNum);
        }
        public double? GetDoubleOrNull(IRow row, int cellNum)
        {
            return CellHelper.Default.TryGet(row, (A, B) => (double?)CellHelper.Default.GetNumber(A, B), cellNum, () => null);
        }
    }
}