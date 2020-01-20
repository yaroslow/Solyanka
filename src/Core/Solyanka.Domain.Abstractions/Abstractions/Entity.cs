using System;
using System.Collections.Generic;
using Solyanka.Domain.Abstractions.Abstractions.FunctionalInterfaces;
using Solyanka.Domain.Abstractions.Abstractions.MarkerInterfaces;

namespace Solyanka.Domain.Abstractions.Abstractions
{
    /// <summary>
    /// Entity with identificator
    /// </summary>
    /// <typeparam name="TIdentificator"></typeparam>
    public abstract class Entity<TIdentificator> : IIdentifiable<TIdentificator>, IEntity, IEquatable<Entity<TIdentificator>>
    {
        /// <inheritdoc cref="IIdentifiable{TIdentificator}" />
        public TIdentificator Id { get; protected set; }

        
        /// <summary/>
        protected Entity() {}
        
        /// <summary>
        /// Ctor of <see cref="Entity{TIdentificator}"/>
        /// </summary>
        /// <param name="id">Entity identificator</param>
        protected Entity(TIdentificator id)
        {
            Id = id;
        }

        
        /// <inheritdoc />
        public virtual bool Equals(Entity<TIdentificator> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<TIdentificator>.Default.Equals(Id, other.Id);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Entity<TIdentificator>) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return EqualityComparer<TIdentificator>.Default.GetHashCode(Id);
        }
        
        
        /// <summary>
        /// Operator ==
        /// </summary>
        /// <param name="left">Left operand of type <see cref="Entity{TIdentificator}"/></param>
        /// <param name="right">Right operand of type <see cref="Entity{TIdentificator}"/></param>
        /// <returns>Equality</returns>
        public static bool operator ==(Entity<TIdentificator> left, Entity<TIdentificator> right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Operator !=
        /// </summary>
        /// <param name="left">Left operand of type <see cref="Entity{TIdentificator}"/></param>
        /// <param name="right">Right operand of type <see cref="Entity{TIdentificator}"/></param>
        /// <returns>Inequality</returns>
        public static bool operator !=(Entity<TIdentificator> left, Entity<TIdentificator> right)
        {
            return !Equals(left, right);
        }
    }
}