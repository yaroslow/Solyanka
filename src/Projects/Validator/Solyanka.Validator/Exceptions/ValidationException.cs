using System;
using System.Runtime.Serialization;

namespace Solyanka.Validator.Exceptions
{
    /// <summary>
    /// Exception of validation
    /// </summary>
    [Serializable]
    public sealed class ValidationException : Exception
    {
        /// <inheritdoc />
        public ValidationException() {}

        /// <inheritdoc />
        public ValidationException(string message) : base(message) {}

        /// <inheritdoc />
        public ValidationException(string message, Exception inner) : base(message, inner) {}

        private ValidationException(SerializationInfo info, StreamingContext context) : base(info, context) {}
    }
}