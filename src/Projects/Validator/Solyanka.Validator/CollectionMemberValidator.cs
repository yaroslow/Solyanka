using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using Solyanka.Expressions;

namespace Solyanka.Validator;

/// <summary>
/// Validator of model collection member
/// </summary>
/// <typeparam name="TModel">Model type</typeparam>
/// <typeparam name="TMember">Model collection member type</typeparam>
public class CollectionMemberValidator<TModel, TMember>
{
    private readonly Validator<TModel> _validator;
    private readonly Expression<Func<TModel, IEnumerable<TMember>>> _collection;


    /// <summary/>
    protected internal CollectionMemberValidator(Validator<TModel> validator, Expression<Func<TModel, IEnumerable<TMember>>> collection)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _collection = collection ?? throw new ArgumentNullException(nameof(collection));
    }

    
    /// <summary>
    /// Point on member to validate (selected collection of members)
    /// </summary>
    /// <param name="subMember">Expression pointer on submember to validate</param>
    /// <typeparam name="TSubMember">Submember type</typeparam>
    /// <returns><see cref="CollectionMemberValidator{TModel,TMember}"/></returns>
    public CollectionMemberValidator<TModel, TSubMember> For<TSubMember>(Expression<Func<TMember, TSubMember>> subMember)
    {
        if (subMember == null) throw new ArgumentNullException(nameof(subMember));
        
        var pointer = _collection.Compose(ForSelect(subMember));
        return new CollectionMemberValidator<TModel, TSubMember>(_validator, pointer);
    }

    /// <summary>
    /// Point on collection member to validate (selected collection of collection members)
    /// </summary>
    /// <param name="subMember">Expression pointer on collection submember to validate</param>
    /// <typeparam name="TSubMember">Submember type</typeparam>
    /// <returns><see cref="CollectionMemberValidator{TModel,TMember}"/></returns>
    public CollectionMemberValidator<TModel, TSubMember> ForEach<TSubMember>(Expression<Func<TMember, IEnumerable<TSubMember>>> subMember)
    {
        if (subMember == null) throw new ArgumentNullException(nameof(subMember));

        var pointer = _collection.Compose(ForManySelect(subMember));
        return new CollectionMemberValidator<TModel, TSubMember>(_validator, pointer);
    }

    /// <summary>
    /// Impose restriction on member to validate
    /// </summary>
    /// <param name="rule">Restriction on member to validate</param>
    /// <param name="message">Validation exception message on validation failure</param>
    /// <returns><see cref="CollectionMemberValidator{TModel,TMember}"/></returns>
    public CollectionMemberValidator<TModel, TMember> Rule(Expression<Func<TMember, bool>> rule, string message = null)
    {
        return Rule(rule, _ => message);
    }

    /// <summary>
    /// Impose restriction on member to validate
    /// </summary>
    /// <param name="rule">Restriction on member to validate</param>
    /// <param name="message">Validation exception message on validation failure</param>
    /// <returns><see cref="CollectionMemberValidator{TModel,TMember}"/></returns>
    public CollectionMemberValidator<TModel, TMember> Rule(Expression<Func<TMember, bool>> rule, 
        Expression<Func<IEnumerable<TMember>, string>> message)
    {
        if (rule == null) throw new ArgumentNullException(nameof(rule));
        if (message == null) throw new ArgumentNullException(nameof(message));
        
        _validator.Rule(_collection.Compose(ForAll(rule)), _collection.Compose(message));
        return this;
    }

    /// <summary>
    /// Impose conditional restriction on collection member to validate
    /// </summary>
    /// <param name="condition">Condition case to validate</param>
    /// <param name="rule">Restriction on member to validate</param>
    /// <param name="message">Validation exception message on validation failure</param>
    /// <returns><see cref="CollectionMemberValidator{TModel,TMember}"/></returns>
    public CollectionMemberValidator<TModel, TMember> CollectionConditionalRule(Expression<Func<IEnumerable<TMember>, bool>> condition, 
        Expression<Func<TMember, bool>> rule, string message = null)
    {
        return CollectionConditionalRule(condition, rule, _ => message);
    }

    /// <summary>
    /// Impose conditional restriction on collection member to validate
    /// </summary>
    /// <param name="condition">Condition case to validate</param>
    /// <param name="rule">Restriction on member to validate</param>
    /// <param name="message">Validation exception message on validation failure</param>
    /// <returns><see cref="CollectionMemberValidator{TModel,TMember}"/></returns>
    public CollectionMemberValidator<TModel, TMember> CollectionConditionalRule(Expression<Func<IEnumerable<TMember>, bool>> condition,
        Expression<Func<TMember, bool>> rule, Expression<Func<IEnumerable<TMember>, string>> message)
    {
        if (condition == null) throw new ArgumentNullException(nameof(condition));
        if (rule == null) throw new ArgumentNullException(nameof(rule));
        if (message == null) throw new ArgumentNullException(nameof(message));
        
        _validator.ConditionalRule(
            _collection.Compose(condition),
            _collection.Compose(ForAll(rule)),
            _collection.Compose(message));
        return this;
    }

    /// <summary>
    /// Impose conditional restriction on member to validate
    /// </summary>
    /// <param name="condition">Condition case to validate</param>
    /// <param name="rule">Restriction on member to validate</param>
    /// <param name="message">Validation exception message on validation failure</param>
    /// <returns><see cref="CollectionMemberValidator{TModel,TMember}"/></returns>
    public CollectionMemberValidator<TModel, TMember> ConditionalRule(Expression<Func<TMember, bool>> condition,
        Expression<Func<TMember, bool>> rule, string message = null)
    {
        return ConditionalRule(condition, rule, _ => message);
    }

    /// <summary>
    /// Impose conditional restriction on member to validate
    /// </summary>
    /// <param name="condition">Condition case to validate</param>
    /// <param name="rule">Restriction on member to validate</param>
    /// <param name="message">Validation exception message on validation failure</param>
    /// <returns><see cref="CollectionMemberValidator{TModel,TMember}"/></returns>
    public CollectionMemberValidator<TModel, TMember> ConditionalRule(Expression<Func<TMember, bool>> condition,
        Expression<Func<TMember, bool>> rule, Expression<Func<IEnumerable<TMember>, string>> message)
    {
        if (condition == null) throw new ArgumentNullException(nameof(condition));
        if (rule == null) throw new ArgumentNullException(nameof(rule));
        if (message == null) throw new ArgumentNullException(nameof(message));
        
        _validator.Rule(_collection.Compose(ForAllWhere(condition, rule)), _collection.Compose(message));
        return this;
    }

    /// <summary>
    /// Impose restrictions on collection member to validate by attributes
    /// </summary>
    /// <returns><see cref="CollectionMemberValidator{TModel,TMember}"/></returns>
    public CollectionMemberValidator<TModel, TMember> ByAttributesRule()
    {
        var type = typeof(TMember);
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
    /// Use constraints from model type validator 
    /// </summary>
    /// <param name="validator"></param>
    /// <returns><see cref="MemberValidator{TModel,TMember}"/></returns>
    public CollectionMemberValidator<TModel, TMember> From(Validator<TMember> validator)
    {
        if (validator == null) throw new ArgumentNullException(nameof(validator));
            
        foreach (var ((rule, condition, message), (_, _, _)) in validator.Rules)
        {
            var composedRule = _collection.Compose(ForAllWhere(condition, rule));
            var composedMessage = _collection.Compose(ForFirst(message));

            var compiledRule = composedRule.AsFunc();
            var compiledMessage = composedMessage.AsFunc();
                
            if (_validator.Rules.TryAdd((composedRule, null, composedMessage), (compiledRule, null, compiledMessage)))
            {
                continue;
            }

            throw new InvalidOperationException(
                $"Constraint {composedRule.Body} with condition {null} could not be add");
        }
        
        return this;
    }
        
    /// <summary>
    /// Use constraints from model type validator injected in <see cref="Validator"/>
    /// </summary>
    /// <returns><see cref="MemberValidator{TModel,TMember}"/></returns>
    public CollectionMemberValidator<TModel, TMember> From()
    {
        var validator = Validator.Get<TMember>();
        return From(validator);
    }
    
    /// <summary>
    /// Return validator root
    /// </summary>
    /// <returns><see cref="Validator{TModel}"/></returns>
    public Validator<TModel> ToRoot()
    {
        return _validator;
    }


    private static Expression<Func<IEnumerable<TMember>, IEnumerable<TSubMember>>> ForSelect<TSubMember>(
        Expression<Func<TMember, TSubMember>> selector) => elements => elements.Select(selector.Compile());

    private static Expression<Func<IEnumerable<TMember>, IEnumerable<TSubMember>>> ForManySelect<TSubMember>(
        Expression<Func<TMember, IEnumerable<TSubMember>>> selector) => elements => elements.SelectMany(selector.Compile());

    private static Expression<Func<IEnumerable<TMember>, string>> ForFirst(Expression<Func<TMember, string>> message) =>
        elements => message.Compile().Invoke(elements.FirstOrDefault());
    
    private static Expression<Func<IEnumerable<TMember>, bool>> ForAll(Expression<Func<TMember, bool>> rule) =>
        elements => elements.All(rule.Compile());

    private static Expression<Func<IEnumerable<TMember>, bool>> ForAllWhere(Expression<Func<TMember, bool>> condition,
        Expression<Func<TMember, bool>> rule) => elements => elements.Where(condition.Compile()).All(rule.Compile());
}