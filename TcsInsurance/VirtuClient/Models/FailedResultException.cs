using System;
namespace VirtuClient.Models
{
    [Serializable]
    public class FailedResultException : Exception
    {
        public FailedResultException() { }
        public FailedResultException(string message) : base(message) { }
        public FailedResultException(string message, Exception inner) : base(message, inner) { }
        protected FailedResultException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
