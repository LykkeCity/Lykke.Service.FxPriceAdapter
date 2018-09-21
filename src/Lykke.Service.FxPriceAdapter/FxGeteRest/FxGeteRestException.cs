using System;
using System.Runtime.Serialization;

namespace Lykke.Service.FxPriceAdapter.FxGeteRest
{
    public class FxGeteRestException : Exception
    {
        public FxGeteRestException()
        {
        }

        protected FxGeteRestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public FxGeteRestException(string message, string json) : base(message)
        {
        }

        public FxGeteRestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public string Json { get; set; }
    }
}