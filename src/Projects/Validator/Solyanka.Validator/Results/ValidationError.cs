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
        public Expression<Func<TModel, bool>> ValidationConstraint { get; }
        
        /// <summary>
        /// Source (model or member) of validation error
        /// </summary>
        public string Source { get; }
        
        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get; }
        
        
        /// <summary/>
        public ValidationError(Expression<Func<TModel, bool>> validationConstraint, string source, string errorMessage)
        {
            ValidationConstraint = validationConstraint;
            Source = source;
            ErrorMessage = errorMessage;
        }
    }
}