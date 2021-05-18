using System;
using System.Collections.Generic;

namespace Solyanka.Validator.Exceptions
{
    /// <summary>
    /// Aggregated exception of validation
    /// </summary>
    public class ValidationAggregateException : AggregateException
    {
        /// <inheritdoc />
        public ValidationAggregateException() {}

        /// <inheritdoc />
        public ValidationAggregateException(string message) : base(message) {}

        /// <inheritdoc />
        public ValidationAggregateException(string message, ValidationException innerValidationException) : 
            base(message, innerValidationException) {}

        /// <inheritdoc />
        public ValidationAggregateException(string message, IEnumerable<ValidationException> innerValidationExceptions) : 
            base(message, innerValidationExceptions) {}
    }
}