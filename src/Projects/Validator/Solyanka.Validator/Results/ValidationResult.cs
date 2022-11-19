using System;
using System.Collections.Generic;
using System.Linq;
using Solyanka.Validator.Exceptions;

namespace Solyanka.Validator.Results;

/// <summary>
/// Result of validation
/// </summary>
/// <typeparam name="TModel">Type of model that was validated</typeparam>
public class ValidationResult<TModel>
{
    /// <summary>
    /// Validation errors
    /// </summary>
    public IEnumerable<ValidationError<TModel>> Errors { get; }

    /// <summary>
    /// Success validation
    /// </summary>
    public bool Success => !Errors.Any();


    /// <summary/>
    public ValidationResult(IEnumerable<ValidationError<TModel>> validationErrors)
    {
        Errors = validationErrors;
    }

        
    /// <summary>
    /// Throws the first <see cref="ValidationException"/> if it exists
    /// </summary>
    /// <exception cref="ValidationException">First validation exception</exception>
    public void Raise()
    {
        var error = Errors.FirstOrDefault();
        if (error == null)
        {
            return;
        }

        throw new ValidationException(error.Message, error.Source, error.Condition, error.Exception);
    }

    /// <summary>
    /// Throws <see cref="ValidationAggregateException"/> if there are any error
    /// </summary>
    /// <exception cref="AggregateException">Aggregated validation exception</exception>
    public void RaiseAggregated()
    {
        if (Success)
        {
            return;
        }

        var exceptions = Errors.Select(error =>
            new ValidationException(error.Message, error.Source, error.Condition, error.Exception));
        throw new AggregateException(exceptions);
    }
}