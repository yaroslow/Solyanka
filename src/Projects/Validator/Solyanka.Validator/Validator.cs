using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using Solyanka.Expressions;
using Solyanka.Validator.Results;

namespace Solyanka.Validator
{
    /// <summary>
    /// Validator
    /// </summary>
    public static class Validator
    {
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
        public static Validator<TModel> Create<TModel>()
        {
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
        /// Validate model
        /// </summary>
        /// <param name="model">Model</param>
        /// <typeparam name="TModel">Model type</typeparam>
        /// <returns><see cref="ValidationResult{TModel}"/></returns>
        public static ValidationResult<TModel> Validate<TModel>(TModel model)
        {
            var validator = Get<TModel>();
            return validator?.Validate(model);
        }
    }
    
    /// <summary>
    /// Validator of model
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    public class Validator<TModel>
    {
        internal readonly ConcurrentDictionary<Expression<Func<TModel, bool>>, KeyValuePair<Func<TModel, bool>, string>> Constraints;

        
        /// <summary/>
        protected internal Validator()
        {
            Constraints = new ConcurrentDictionary<Expression<Func<TModel, bool>>, KeyValuePair<Func<TModel, bool>, string>>();
        }

        
        /// <summary>
        /// Create <see cref="Validator{TModel}"/>. Use if you need several validators of single model type
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
        /// <returns><see cref="ValidatorMember{TModel,TMember}"/></returns>
        public ValidatorMember<TModel, TMember> For<TMember>(Expression<Func<TModel, TMember>> member)
        {
            return new ValidatorMember<TModel, TMember>(this, member);
        }

        /// <summary>
        /// Impose restriction on member to validate
        /// </summary>
        /// <param name="constraint">Restriction on member to validate</param>
        /// <param name="validationExceptionMessage">Validation exception message on validation failure</param>
        /// <returns><see cref="Validator{TModel}"/></returns>
        /// <exception cref="InvalidOperationException">Throws if constrain could not be add</exception>
        public Validator<TModel> Constrain(Expression<Func<TModel, bool>> constraint, string validationExceptionMessage)
        {
            var compiledExpression = constraint.AsFunc();
            if(Constraints.TryAdd(constraint, new KeyValuePair<Func<TModel, bool>, string>(compiledExpression, validationExceptionMessage)))
                return this;
            throw new InvalidOperationException("Constraint could not be add");
        }

        /// <summary>
        /// Validate model
        /// </summary>
        /// <param name="model">Model to validate</param>
        /// <returns><see cref="ValidationResult{TModel}"/></returns>
        public ValidationResult<TModel> Validate(TModel model)
        {
            var errors = new List<ValidationError<TModel>>();
            foreach (var (expression, (func, errorMessage)) in Constraints)
            {
                if (!func.Invoke(model))
                {
                    //TODO: source
                    errors.Add(new ValidationError<TModel>(expression, "source", errorMessage));
                }
            }

            return new ValidationResult<TModel>(errors);
        }
    }
}