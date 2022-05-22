using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Solyanka.Expressions;

namespace Solyanka.Validator
{
    /// <summary>
    /// Validator of model member
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    /// <typeparam name="TMember">Model member type</typeparam>
    public class MemberValidator<TModel, TMember>
    {
        private readonly Validator<TModel> _validator;
        private readonly Expression<Func<TModel, TMember>> _member;


        /// <summary/>
        protected internal MemberValidator(Validator<TModel> validator, Expression<Func<TModel, TMember>> member)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _member = member ?? throw new ArgumentNullException(nameof(member));
        }
        

        /// <summary>
        /// Point on member to validate
        /// </summary>
        /// <param name="subMember">Expression pointer on submember to validate</param>
        /// <typeparam name="TSubMember">Submember type</typeparam>
        /// <returns><see cref="MemberValidator{TModel,TSubMember}"/></returns>
        public MemberValidator<TModel, TSubMember> For<TSubMember>(Expression<Func<TMember, TSubMember>> subMember)
        {
            if (subMember == null) throw new ArgumentNullException(nameof(subMember));
            
            var pointer = _member.Compose(subMember);
            return new MemberValidator<TModel, TSubMember>(_validator, pointer);
        }

        /// <summary>
        /// Point on collection member to validate
        /// </summary>
        /// <param name="subMember">Expression pointer on collection submember to validate</param>
        /// <typeparam name="TSubMember">Submember type</typeparam>
        /// <returns><see cref="CollectionMemberValidator{TModel,TMember}"/></returns>
        public CollectionMemberValidator<TModel, TSubMember> ForEach<TSubMember>(
            Expression<Func<TMember, IEnumerable<TSubMember>>> subMember)
        {
            if (subMember == null) throw new ArgumentNullException(nameof(subMember));

            var pointer = _member.Compose(subMember);
            return new CollectionMemberValidator<TModel, TSubMember>(_validator, pointer);
        }

        /// <summary>
        /// Impose restriction on member to validate
        /// </summary>
        /// <param name="rule">Restriction on member to validate</param>
        /// <param name="message">Validation exception message on validation failure</param>
        /// <returns><see cref="MemberValidator{TModel,TMember}"/></returns>
        public MemberValidator<TModel, TMember> Rule(Expression<Func<TMember, bool>> rule, string message = null)
        {
            return Rule(rule, _ => message);
        }

        /// <summary>
        /// Impose restriction on member to validate
        /// </summary>
        /// <param name="rule">Restriction on member to validate</param>
        /// <param name="message">Validation exception message on validation failure</param>
        /// <returns><see cref="MemberValidator{TModel,TMember}"/></returns>
        public MemberValidator<TModel, TMember> Rule(Expression<Func<TMember, bool>> rule, Expression<Func<TMember, string>> message)
        {
            if (rule == null) throw new ArgumentNullException(nameof(rule));
            if (message == null) throw new ArgumentNullException(nameof(message));
            
            _validator.Rule(_member.Compose(rule), _member.Compose(message));
            return this;
        }

        /// <summary>
        /// Impose conditional restriction on member to validate
        /// </summary>
        /// <param name="condition">Condition case to validate</param>
        /// <param name="rule">Restriction on member to validate</param>
        /// <param name="message">Validation exception message on validation failure</param>
        /// <returns><see cref="MemberValidator{TModel,TMember}"/></returns>
        public MemberValidator<TModel, TMember> ConditionalRule(Expression<Func<TMember, bool>> condition,
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
        /// <returns><see cref="MemberValidator{TModel,TMember}"/></returns>
        public MemberValidator<TModel, TMember> ConditionalRule(Expression<Func<TMember, bool>> condition, 
            Expression<Func<TMember, bool>> rule, Expression<Func<TMember, string>> message)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (rule == null) throw new ArgumentNullException(nameof(rule));
            if (message == null) throw new ArgumentNullException(nameof(message));
            
            _validator.ConditionalRule(_member.Compose(condition), _member.Compose(rule), _member.Compose(message));
            return this;
        }

        /// <summary>
        /// Impose restrictions on member to validate by attributes
        /// </summary>
        /// <returns><see cref="MemberValidator{TModel, TMember}"/></returns>
        public MemberValidator<TModel, TMember> ByAttributesRule()
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
        public MemberValidator<TModel, TMember> From(Validator<TMember> validator)
        {
            if (validator == null) throw new ArgumentNullException(nameof(validator));
            
            foreach (var ((rule, condition, message), (_, _, _)) in validator.Rules)
            {
                var composedRule = _member.Compose(rule);
                var composedCondition = condition != null ? _member.Compose(condition) : null;
                var composedMessage = _member.Compose(message);

                var compiledRule = composedRule.AsFunc();
                var compiledCondition = composedCondition?.AsFunc();
                var compiledMessage = composedMessage.AsFunc();
                
                if (_validator.Rules.TryAdd((composedRule, composedCondition, composedMessage), (compiledRule, compiledCondition, compiledMessage)))
                {
                    continue;
                }

                throw new InvalidOperationException(
                    $"Constraint {composedRule.Body} with condition {composedCondition?.Body} could not be add");
            }
        
            return this;
        }
        
        /// <summary>
        /// Use constraints from model type validator injected in <see cref="Validator"/>
        /// </summary>
        /// <returns><see cref="MemberValidator{TModel,TMember}"/></returns>
        public MemberValidator<TModel, TMember> From()
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
    }
}