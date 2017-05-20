using System;
namespace VirtuClient.Models.Core
{
    [Serializable]
    public class VirtuResponceException : VirtuException
    {
        public VirtuResponceException() { }
        public VirtuResponceException(string message) : base(message) { }
        public VirtuResponceException(string message, Exception inner) : base(message, inner) { }
        protected VirtuResponceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}