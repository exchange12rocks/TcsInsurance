using System;
namespace TcsInsurance.Helpers.Excel
{
    [Serializable]
    public class ExcelParseInvalidOperationException : InvalidOperationException
    {
        public ExcelParseInvalidOperationException() { }
        public ExcelParseInvalidOperationException(string message) : base(message) { }
        public ExcelParseInvalidOperationException(string message, Exception inner) : base(message, inner) { }
        protected ExcelParseInvalidOperationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}