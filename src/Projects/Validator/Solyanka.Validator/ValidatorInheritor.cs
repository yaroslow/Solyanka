using System;
using System.Linq.Expressions;
using Solyanka.Expressions;

namespace Solyanka.Validator;

/// <summary>
/// Validator inheritor
/// </summary>
/// <typeparam name="TModel">Inhereted type</typeparam>
/// <typeparam name="TBaseModel">Base type</typeparam>
internal static class ValidatorInheritor<TModel, TBaseModel> where TModel : TBaseModel
{
    /// <summary>
    /// Create <see cref="ValidatorInheritor{TModel,TBaseModel}"/>
    /// </summary>
    /// <param name="baseValidator"><see cref="Validator{TModel}"/> of base model type</param>
    /// <returns><see cref="Validator{TModel}"/></returns>
    internal static Validator<TModel> Create(Validator<TBaseModel> baseValidator)
    {
        var validator = Validator<TModel>.Create();
        InjectRules(baseValidator, validator);
        return validator;
    }

    /// <summary>
    /// Create <see cref="ValidatorInheritor{TModel,TBaseModel}"/>
    /// </summary>
    /// <returns><see cref="Validator{TModel}"/></returns>
    internal static Validator<TModel> Create()
    {
        var baseValidator = Validator.Get<TBaseModel>();
        var validator = Validator.Create<TModel>();
        InjectRules(baseValidator, validator);
        return validator;
    }
        
        
    private static Expression<Func<TModel, TBaseModel>> ToBase() => model => model;
        
    private static Validator<TModel> InjectRules(Validator<TBaseModel> baseValidator, Validator<TModel> validator)
    {
        foreach (var ((validationExpression, conditionExpression, messageExpression),_) in baseValidator.Rules)
        {
            var composedValidationExpression = ToBase().Compose(validationExpression);
            var composedMessageExpression = ToBase().Compose(messageExpression);

            if (conditionExpression != null)
            {
                var composedConditionExpression = ToBase().Compose(conditionExpression);
                validator.ConditionalRule(composedConditionExpression, composedValidationExpression, composedMessageExpression);
            }
            else
            {
                validator.Rule(composedValidationExpression, composedMessageExpression);
            }
        }

        return validator;
    }
}