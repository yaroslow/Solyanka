using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Solyanka.Exceptions.Exceptions;

namespace Solyanka.Validator.Exceptions
{
    /// <summary>
    /// Aggregated exception of validation
    /// </summary>
    [Serializable]
    public class ValidationAggregateException : ControllableException
    {
        /// <summary>
        /// List of validation exception
        /// </summary>
        public List<ValidationException> ValidationExceptions { get; }


        /// <inheritdoc />
        protected ValidationAggregateException()
        {
            ValidationExceptions = new List<ValidationException>();
        }

        /// <inheritdoc />
        public ValidationAggregateException(string message, IEnumerable<ValidationException> validationExceptions) : base(message)
        {
            ValidationExceptions = validationExceptions.ToList();
        }


        /// <inheritdoc />
        public override string ToString()
        {
            var stringBuilder = new StringBuilder(base.ToString());
            stringBuilder.Append(Environment.NewLine);
            foreach (var validationException in ValidationExceptions)
            {
                stringBuilder.Append(validationException);
            }

            return stringBuilder.ToString();
        }
    }
}