using System;
using System.Runtime.Serialization;

namespace WebMvc.Controllers
{
    [Serializable]
    internal class BrokenCircuitException : Exception
    {
        public BrokenCircuitException()
        {
        }

        public BrokenCircuitException(string message) : base(message)
        {
        }

        public BrokenCircuitException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BrokenCircuitException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}