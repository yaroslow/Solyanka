using System;
using System.Linq.Expressions;
using Solyanka.Expressions;

namespace Solyanka.Validator
{
    /// <summary>
    /// Validator of model member
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    /// <typeparam name="TMember">Model member type</typeparam>
    public class ValidatorMember<TModel, TMember>
    {
        private readonly Validator<TModel> _validator;
        private readonly Expression<Func<TModel, TMember>> _member;


        /// <summary/>
        protected internal ValidatorMember(Validator<TModel> validator, Expression<Func<TModel, TMember>> member)
        {
            _validator = validator;
            _member = member;
        }
        

        /// <summary>
        /// Point on member to validate
        /// </summary>
        /// <param name="subMember">Expression pointer on submember to validate</param>
        /// <typeparam name="TSubMember">Submember type</typeparam>
        /// <returns><see cref="ValidatorMember{TModel, TSubMember}"/></returns>
        public ValidatorMember<TModel, TSubMember> For<TSubMember>(Expression<Func<TMember, TSubMember>> subMember)
        {
            var pointer = _member.Compose(subMember);
            return new ValidatorMember<TModel, TSubMember>(_validator, pointer);
        }

        /// <summary>
        /// Impose restriction on member to validate
        /// </summary>
        /// <param name="constraint">Restriction on member to validate</param>
        /// <param name="validationExceptionMessage">Validation exception message on validation failure</param>
        /// <returns><see cref="ValidatorMember{TModel, TMember}"/></returns>
        public ValidatorMember<TModel, TMember> Constrain(Expression<Func<TMember, bool>> constraint, string validationExceptionMessage)
        {
            _validator.Constrain(_member.Compose(constraint), validationExceptionMessage);
            return this;
        }

        /// <summary>
        /// Use constraints from model type validator 
        /// </summary>
        /// <param name="validator"></param>
        /// <returns><see cref="ValidatorMember{TModel,TMember}"/></returns>
        public ValidatorMember<TModel, TMember> From(Validator<TMember> validator)
        {
            foreach (var (constraint, (_, message)) in validator.Constraints)
            {
                Constrain(constraint, message);
            }

            return this;
        }
        
        /// <summary>
        /// Use constraints from model type validator injected in <see cref="Validator"/>
        /// </summary>
        /// <returns><see cref="ValidatorMember{TModel,TMember}"/></returns>
        public ValidatorMember<TModel, TMember> From()
        {
            var validator = Validator.Get<TMember>();
            return From(validator);
        }

        /// <summary>
        /// Compress validation constraints and return validator root
        /// </summary>
        /// <returns><see cref="Validator{TModel}"/></returns>
        public Validator<TModel> Compress()
        {
            return _validator;
        }
    }
}