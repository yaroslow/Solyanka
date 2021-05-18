using System;
using System.Runtime.Serialization;

namespace Solyanka.Ddd.Exceptions
{
    /// <summary>
    /// Exception in domain logic
    /// </summary>
    [Serializable]
    public class DomainException : Exception
    {
        /// <inheritdoc />
        public DomainException() {}

        /// <inheritdoc />
        public DomainException(string message) : base(message) {}

        /// <inheritdoc />
        public DomainException(string message, Exception inner) : base(message, inner) {}

        /// <inheritdoc />
        protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context) {}
    }
}