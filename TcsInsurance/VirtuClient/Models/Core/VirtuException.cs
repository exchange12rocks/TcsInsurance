using System;
namespace VirtuClient.Models.Core
{
    [Serializable]
    public class VirtuException : Exception
    {
        public VirtuException() { }
        public VirtuException(string message) : base(message) { }
        public VirtuException(string message, Exception inner) : base(message, inner) { }
        protected VirtuException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}