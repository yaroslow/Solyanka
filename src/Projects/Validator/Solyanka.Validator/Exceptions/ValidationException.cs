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
        public override string Source { get; set; }


        /// <inheritdoc />
        public ValidationException() {}

        /// <inheritdoc />
        public ValidationException(string message, string source) : base(message)
        {
            Source = source;
        }

        /// <inheritdoc />
        public ValidationException(string message, string source, Exception inner) : base(message, inner)
        {
            Source = source;
        }

        private ValidationException(SerializationInfo info, StreamingContext context) : base(info, context) {}
    }
}