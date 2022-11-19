using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Solyanka.Expressions;
using Solyanka.Validator.Results;

namespace Solyanka.Validator;

/// <summary>
/// Validator
/// </summary>
public static class Validator
{
    /// <summary>
    /// Compiled validators to reuse
    /// </summary>
    private static readonly ConcurrentDictionary<Type, object> Validators;
        
        
    /// <summary/>
    static Validator()
    {
        Validators = new ConcurrentDictionary<Type, object>();
    }
        
        
    /// <summary>
    /// Create <see cref="Validator{TModel}"/> and add to validators collection
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    /// <returns><see cref="Validator{TModel}"/></returns>
    /// <exception cref="InvalidOperationException"><see cref="Validator{TModel}"/> already exist</exception>
    public static Validator<TModel> Create<TModel>()
    {
        var modelType = typeof(TModel);
        if (Validators.ContainsKey(modelType))
        {
            throw new InvalidOperationException($"Validator of type {modelType.FullName} already exists. " +
                                                $"If you want to create another one use Validator<TModel>.Create method");
        }
            
        var validator = new Validator<TModel>();
        Validators.TryAdd(typeof(TModel), validator);
        return validator;
    }

    /// <summary>
    /// Get <see cref="Validator{TModel}"/> from validators collection
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    /// <returns><see cref="Validator{TModel}"/></returns>
    public static Validator<TModel> Get<TModel>()
    {
        Validators.TryGetValue(typeof(TModel), out var validator);
        return validator as Validator<TModel>;
    }

    /// <summary>
    /// Create <see cref="Validator"/> inheriting rules from base type
    /// </summary>
    /// <param name="baseValidator"><see cref="Validator"/> of base type</param>
    /// <typeparam name="TModel">Model type</typeparam>
    /// <typeparam name="TBaseModel">Base model type</typeparam>
    /// <returns><see cref="Validator"/></returns>
    public static Validator<TModel> InheritFrom<TModel, TBaseModel>(Validator<TBaseModel> baseValidator) where TModel : TBaseModel
    {
        return ValidatorInheritor<TModel, TBaseModel>.Create(baseValidator);
    }

    /// <summary>
    /// Create <see cref="Validator"/> inheriting rules from base type and add to validators collection
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    /// <typeparam name="TBaseModel">Base model type</typeparam>
    /// <returns><see cref="Validator"/></returns>
    public static Validator<TModel> InheritFrom<TModel, TBaseModel>() where TModel : TBaseModel
    {
        return ValidatorInheritor<TModel, TBaseModel>.Create();
    }

    /// <summary>
    /// Validate model
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="firstErrorStop">Is validating should stop on the first error</param>
    /// <typeparam name="TModel">Model type</typeparam>
    /// <returns><see cref="ValidationResult{TModel}"/></returns>
    public static ValidationResult<TModel> Validate<TModel>(TModel model, bool firstErrorStop = false)
    {
        var validator = Get<TModel>();
        return validator?.Validate(model, firstErrorStop);
    }
}
    
/// <summary>
/// Validator of model
/// </summary>
/// <typeparam name="TModel">Model type</typeparam>
public class Validator<TModel>
{
    /// <summary>
    /// Dictionary with constraints.
    /// Key is tuple of ValidationExpression and ConditionExpression
    /// Values is tuple of:
    /// 1) Compiled ValidationExpression
    /// 2) Compiled ConditionExpression
    /// 3) Error message
    /// </summary>
    internal readonly ConcurrentDictionary<
        (Expression<Func<TModel, bool>> ValidationExpression,
        Expression<Func<TModel, bool>>ConditionExpression,
        Expression<Func<TModel, string>> ErrorMessage),
        (Func<TModel, bool> CompiledValidationExpression,
        Func<TModel, bool> CompiledConditionExpression,
        Func<TModel, string> CompiledErrorMessage)> Rules;

    /// <summary/>
    protected internal Validator()
    {
        Rules = new ConcurrentDictionary<
            (Expression<Func<TModel, bool>> ValidationExpression,
            Expression<Func<TModel, bool>> ConditionExpression,
            Expression<Func<TModel, string>> ErrorMessage),
            (Func<TModel, bool> CompiledValidationExpression,
            Func<TModel, bool> CompiledConditionExpression,
            Func<TModel, string> ErrorMessage)>();
    }

        
    /// <summary>
    /// Create <see cref="Validator{TModel}"/>.
    /// Use if you need several validators of single model type
    /// </summary>
    /// <returns><see cref="Validator{TModel}"/></returns>
    public static Validator<TModel> Create()
    {
        return new Validator<TModel>();
    }

    /// <summary>
    /// Point on member to validate
    /// </summary>
    /// <param name="member">Expression pointer on member to validate</param>
    /// <typeparam name="TMember">Member type</typeparam>
    /// <returns><see cref="MemberValidator{TModel,TMember}"/></returns>
    public MemberValidator<TModel, TMember> For<TMember>(Expression<Func<TModel, TMember>> member)
    {
        if (member == null) throw new ArgumentNullException(nameof(member));
            
        return new MemberValidator<TModel, TMember>(this, member);
    }

    /// <summary>
    /// Point on collection member to validate
    /// </summary>
    /// <param name="member">Expression pointer on collection member to validate</param>
    /// <typeparam name="TMember">Collection member type</typeparam>
    /// <returns><see cref="CollectionMemberValidator{TModel,TMember}"/></returns>
    public CollectionMemberValidator<TModel, TMember> ForEach<TMember>(Expression<Func<TModel, IEnumerable<TMember>>> member)
    {
        if (member == null) throw new ArgumentNullException(nameof(member));
            
        return new CollectionMemberValidator<TModel, TMember>(this, member);
    }

    /// <summary>
    /// Impose restriction on member to validate
    /// </summary>
    /// <param name="rule">Restriction on member to validate</param>
    /// <param name="message">Validation exception message on validation failure</param>
    /// <returns><see cref="Validator{TModel}"/></returns>
    /// <exception cref="InvalidOperationException">Thrown if constrain could not be add</exception>
    public Validator<TModel> Rule(Expression<Func<TModel, bool>> rule, string message = null)
    {
        return Rule(rule, _ => message);
    }

    /// <summary>
    /// Impose restriction on member to validate
    /// </summary>
    /// <param name="rule">Restriction on member to validate</param>
    /// <param name="message">Validation exception message on validation failure</param>
    /// <returns><see cref="Validator{TModel}"/></returns>
    /// <exception cref="InvalidOperationException">Thrown if constrain could not be add</exception>
    public Validator<TModel> Rule(Expression<Func<TModel, bool>> rule, Expression<Func<TModel, string>> message)
    {
        if (rule == null) throw new ArgumentNullException(nameof(rule));
        if (message == null) throw new ArgumentNullException(nameof(message));
            
        var compiledRule = rule.AsFunc();
        var compiledMessage = message.AsFunc();
            
        if (Rules.TryAdd((rule, null, message), (compiledRule, null, compiledMessage)))
        {
            return this;
        }

        throw new InvalidOperationException($"Constraint {rule.Body} could not be add");
    }

    /// <summary>
    /// Impose conditional restriction on member to validate
    /// </summary>
    /// <param name="condition">Condition case to validate</param>
    /// <param name="rule">Restriction on member to validate</param>
    /// <param name="message">Validation exception message on validation failure</param>
    /// <returns><see cref="Validator{TModel}"/></returns>
    /// <exception cref="InvalidOperationException">Thrown if constrain could not be add</exception>
    public Validator<TModel> ConditionalRule(Expression<Func<TModel, bool>> condition,
        Expression<Func<TModel, bool>> rule, string message = null)
    {
        return ConditionalRule(condition, rule, _ => message);
    }

    /// <summary>
    /// Impose conditional restriction on member to validate
    /// </summary>
    /// <param name="condition">Condition case to validate</param>
    /// <param name="rule">Restriction on member to validate</param>
    /// <param name="message">Validation exception message on validation failure</param>
    /// <returns><see cref="Validator{TModel}"/></returns>
    /// <exception cref="InvalidOperationException">Thrown if constrain could not be add</exception>
    public Validator<TModel> ConditionalRule(Expression<Func<TModel, bool>> condition,
        Expression<Func<TModel, bool>> rule, Expression<Func<TModel, string>> message)
    {
        if (condition == null) throw new ArgumentNullException(nameof(condition));
        if (rule == null) throw new ArgumentNullException(nameof(rule));
        if (message == null) throw new ArgumentNullException(nameof(message));
            
        var compiledCondition = condition.AsFunc();
        var compiledRule = rule.AsFunc();
        var compiledMessage = message.AsFunc();
            
        if (Rules.TryAdd((rule, condition, message), (compiledRule, compiledCondition, compiledMessage)))
        {
            return this;
        }
            
        throw new InvalidOperationException($"Constraint {rule.Body} with condition {condition.Body} could not be add");
    }
        
    /// <summary>
    /// Impose restrictions on member to validate by attributes
    /// </summary>
    /// <returns><see cref="Validator{TModel}"/></returns>
    public Validator<TModel> ByAttributesRule()
    {
        var type = typeof(TModel);
        var properties = type.GetProperties();
        
        foreach (var property in properties)
        {
            var attributes = property.GetCustomAttributes(typeof(ValidationAttribute), true);
        
            foreach (var attribute in attributes)
            {
                var castedAttribute = (ValidationAttribute) attribute;
                Rule(value => castedAttribute.IsValid(property.GetValue(value)), castedAttribute.ErrorMessage);
            }
        }
        
        return this;
    }

    /// <summary>
    /// Validate model
    /// </summary>
    /// <param name="model">Model to validate</param>
    /// <param name="firstErrorStop">Is validating should stop on the first error</param>
    /// <returns><see cref="ValidationResult{TModel}"/></returns>
    public ValidationResult<TModel> Validate(TModel model, bool firstErrorStop = false)
    {
        var errors = new List<ValidationError<TModel>>();
        foreach (var ((validationExpression, conditionExpression, _), (validationFunc, conditionFunc, messageFunc)) in Rules)
        {
            try
            {
                if (conditionFunc != null)
                {
                    if (!conditionFunc.Invoke(model))
                    {
                        continue;
                    }
                }

                if (!validationFunc.Invoke(model))
                {
                    errors.Add(new ValidationError<TModel>(validationExpression,
                        conditionExpression, messageFunc.Invoke(model)));
                    if (firstErrorStop)
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                var exceptionMessage = e.InnerException?.Message ?? e.Message;
                errors.Add(new ValidationError<TModel>(validationExpression,
                    conditionExpression, exceptionMessage));

                if (firstErrorStop)
                {
                    break;
                }
            }
        }

        return new ValidationResult<TModel>(errors);
    }
}