using System;
using System.Runtime.Serialization;

namespace WebMvc.Controllers
{
    [Serializable]
    internal class brokencircuitexception : Exception
    {
        public brokencircuitexception()
        {
        }

        public brokencircuitexception(string message) : base(message)
        {
        }

        public brokencircuitexception(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected brokencircuitexception(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}