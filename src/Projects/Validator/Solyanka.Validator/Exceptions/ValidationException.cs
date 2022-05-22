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
        private ValidationException() {}
        
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
        public ValidationException(string message, string source, string condition, Exception innerException) : base(message, innerException)
        {
            Source = source;
            Data["Condition"] = condition;
            Data["Source"] = source;
        }
        
        /// <inheritdoc />
        public ValidationException(string message, string source, string condition, string code, string subCode, Exception innerException) : 
            this(message, source, condition, innerException)
        {
            Code = code;
            SubCode = subCode;
        }
        
        /// <inheritdoc />
        private ValidationException(SerializationInfo info, StreamingContext context) : base(info, context) {}
    }
}