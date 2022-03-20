using System;
using System.Runtime.Serialization;
using Solyanka.Exceptions.Exceptions;

namespace Solyanka.Validator.Exceptions
{
    /// <summary>
    /// Exception of validation
    /// </summary>
    [Serializable]
    public class ValidationException : ControllableException
    {
        /// <inheritdoc />
        protected ValidationException() {}
        
        /// <inheritdoc />
        public ValidationException(string message) : base(message) {}

        /// <inheritdoc />
        public ValidationException(string message, string source = null) : this(message)
        {
            Source = source;
        }

        /// <inheritdoc />
        public ValidationException(string message, string source, string code, string subCode) : this(message, source)
        {
            Code = code;
            SubCode = subCode;
        }
        
        /// <inheritdoc />
        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context) {}
    }
}