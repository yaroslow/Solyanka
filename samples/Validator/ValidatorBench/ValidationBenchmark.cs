using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Solyanka.Validator;
using ValidatorBench.Models;
using ValidatorBench.Utils;
using Validator = System.ComponentModel.DataAnnotations.Validator;

namespace ValidatorBench;

/// <summary>
/// Benchmark of validators
/// </summary>
public class ValidationBenchmark
{
    private List<User> _users;
    private readonly Validator<User> _solyankaValidator;
    private readonly UserFluentValidator _fluentValidator;

    
    /// <summary>
    /// Number of models to validate
    /// </summary>
    [Params(10000, 1000000)]
    public int NumberOfModels;
    
    /// <summary>
    /// Setup
    /// </summary>
    [GlobalSetup]
    public void Setup()
    {
        _users = new List<User>(NumberOfModels);
        for (var i = 0; i < NumberOfModels; i++)
        {
            _users.Append(new User());
        }
    }
    
    
    /// <summary>
    /// Ctor
    /// </summary>
    public ValidationBenchmark()
    {
        _solyankaValidator = new UserSolyankaValidator().Validator;
        _fluentValidator = new UserFluentValidator();
    }


    /// <summary>
    /// Microsoft validator
    /// </summary>
    [Benchmark]
    public void MicrosoftValidator()
    {
        foreach (var user in _users)
        {
            try
            {
                var context = new ValidationContext(user);
                Validator.ValidateObject(user, context);
            }
            catch (Exception)
            {
                // ignored
            }
        }
        
    }

    /// <summary>
    /// FlunentValidation validator
    /// </summary>
    [Benchmark]
    public void FluentValidator()
    {
        foreach (var user in _users)
        {
            try
            {
                var result = _fluentValidator.Validate(user);
                var firstError = result.Errors.FirstOrDefault();
                if (firstError != null)
                {
                    throw new ValidationException(firstError.ErrorMessage);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }

    /// <summary>
    /// Solyanka validator
    /// </summary>
    [Benchmark]
    public void SolyankaValidator()
    {
        foreach (var user in _users)
        {
            try
            {
                _solyankaValidator.Validate(user).Raise();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}