using System;
using System.Linq.Expressions;

namespace Solyanka.Validator.Results
{
    /// <summary>
    /// Validation error
    /// </summary>
    /// <typeparam name="TModel">Type of model that was validated</typeparam>
    public class ValidationError<TModel>
    {
        /// <summary>
        /// Validation constraint that caused validation error
        /// </summary>
        public Expression<Func<TModel, bool>> ValidationRule { get; }
        
        /// <summary>
        /// Validation conditional rule
        /// </summary>
        public Expression<Func<TModel, bool>> ConditionalRule { get; }

        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; }
        
        /// <summary>
        /// Error source
        /// </summary>
        public string Source { get; }
        
        /// <summary>
        /// Validation rule condition
        /// </summary>
        public string Condition { get; }
        
        /// <summary>
        /// Exception thrown during validation
        /// </summary>
        public Exception Exception { get; }


        /// <summary/>
        public ValidationError(Expression<Func<TModel, bool>> validationRule,
            Expression<Func<TModel, bool>> conditionalRule, string message, 
            Exception exception = null)
        {
            ValidationRule = validationRule;
            ConditionalRule = conditionalRule;
            Message = message;
            Source = validationRule?.Body.ToString();
            Condition = conditionalRule?.Body.ToString();
            
            Exception = exception;
        }
    }
}