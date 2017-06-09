using NPOI.SS.UserModel;
using System;
using System.Globalization;
namespace TinkoffService.Helpers.Excel
{
    public class CellHelper
    {
        private static CellHelper _default;
        public static CellHelper Default
        {
            get
            {
                return _default ?? (_default = new CellHelper());
            }
        }
        private T cast<T>(ICell cell, Func<ICell, T>[] casts)
        {
            foreach (var cast in casts)
            {
                try
                {
                    return cast(cell);
                }
                catch
                {
                }
            }
            throw new ExcelParseInvalidOperationException();
        }
        private DateTime GetDate(ICell cell)
        {
            return this.cast(cell, new Func<ICell, DateTime>[]
            {
                A => cell.DateCellValue,
                A => DateTime.ParseExact(cell.StringCellValue.Trim(), "dd.MM.yyyy", null, DateTimeStyles.None),
            });
        }
        private double GetNumber(ICell cell)
        {
            return this.cast(cell, new Func<ICell, double>[]
            {
                A => cell.NumericCellValue,
                A => double.Parse(A.StringCellValue.Replace(" ", "").Trim(), NumberStyles.Number & ~NumberStyles.AllowThousands, CultureInfo.InvariantCulture.NumberFormat),
                A => double.Parse(A.StringCellValue.Replace(" ", "").Trim(), NumberStyles.Number & ~NumberStyles.AllowThousands, CultureInfo.GetCultureInfo("ru-ru").NumberFormat),
            });
        }
        private string GetString(ICell cell)
        {
            return this.cast(cell, new Func<ICell, string>[]
            {
                A => cell.StringCellValue.Trim(),
                A => cell.NumericCellValue.ToString(),
                A => cell.DateCellValue.ToString("dd.MM.yyyy"),
            });
        }
        public DateTime GetDate(IRow row, int cellNum)
        {
            try
            {
                return GetDate(row.GetCell(cellNum, MissingCellPolicy.RETURN_BLANK_AS_NULL));
            }
            catch (ExcelParseInvalidOperationException exception)
            {
                throw new ExcelParseInvalidOperationException($"Произошла ошибка при попытке получить дату из ячейки со значением '{GetNotEmptyString(row, cellNum)}' строки {row.RowNum} столбца {cellNum}", exception);
            }
        }
        public double GetNumber(IRow row, int cellNum)
        {
            try
            {
                return GetNumber(row.GetCell(cellNum, MissingCellPolicy.RETURN_BLANK_AS_NULL));
            }
            catch (ExcelParseInvalidOperationException exception)
            {
                throw new ExcelParseInvalidOperationException($"Произошла ошибка при попытке получить число из ячейки со значением '{GetNotEmptyString(row, cellNum)}' строки {row.RowNum} столбца {cellNum}", exception);
            }
        }
        public string GetString(IRow row, int cellNum)
        {
            try
            {
                return GetString(row.GetCell(cellNum, MissingCellPolicy.CREATE_NULL_AS_BLANK));
            }
            catch (ExcelParseInvalidOperationException exception)
            {
                throw new ExcelParseInvalidOperationException($"Произошла ошибка при попытке получить текст из ячейки строки {row.RowNum} столбца {cellNum}", exception);
            }
        }
        public string GetNotEmptyString(IRow row, int cellNum)
        {
            try
            {
                string result = GetString(row.GetCell(cellNum, MissingCellPolicy.CREATE_NULL_AS_BLANK));
                if (string.IsNullOrEmpty(result)) throw new ExcelParseInvalidOperationException();
                return result;
            }
            catch (ExcelParseInvalidOperationException exception)
            {
                throw new ExcelParseInvalidOperationException($"Произошла ошибка при попытке получить текст из ячейки строки {row.RowNum} столбца {cellNum}", exception);
            }
        }
        public T TryGet<T>(IRow row, Func<IRow, int, T> func, int cellNum, Func<T> defaultReturnValue = null)
        {
            try
            {
                return func(row, cellNum);
            }
            catch
            {
            }
            if (defaultReturnValue != null)
            {
                return defaultReturnValue();
            }
            else
            {
                throw new ExcelParseInvalidOperationException($"Произошла ошибка при попытке получить данные из ячейки строки {row.RowNum} столбца {cellNum}");
            }
        }
    }
}