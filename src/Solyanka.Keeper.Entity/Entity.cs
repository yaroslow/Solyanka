using System;
using System.Collections.Generic;
using Solyanka.Dispatcher.Events;

namespace Solyanka.Keeper.Entity
{
    /// <summary>
    /// Entity base
    /// </summary>
    /// <typeparam name="TId">Key type</typeparam>
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
    {
        /// <summary>
        /// Key
        /// </summary>
        public TId Id { get; protected set; }

        /// <summary>
        /// Domain events representating <see cref="INotification"/>
        /// </summary>
        public IEnumerable<INotification> DomainEvents => _domainEvents;
        private readonly List<INotification> _domainEvents = new List<INotification>();

        
        /// <summary/>
        protected Entity(){}

        /// <summary/>
        protected Entity(TId id)
        {
            Id = id;
        }
        
        
        /// <summary>
        /// Add entity domain event
        /// </summary>
        /// <param name="domainEvent"><see cref="INotification"/></param>
        public void AddDomainEvent(INotification domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Clear entity domain events
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
        
        
        /// <inheritdoc />
        public bool Equals(Entity<TId> other)
        {
            return Id.Equals(other);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Entity<TId>) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        /// Operator ==
        /// </summary>
        /// <param name="left">Left operand <see cref="Entity{TId}"/>></param>
        /// <param name="right">Right operand <see cref="Entity{TId}"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Operator !=
        /// </summary>
        /// <param name="left">Left operand <see cref="Entity{TId}"/>></param>
        /// <param name="right">Right operand <see cref="Entity{TId}"/></param>
        /// <returns><see cref="bool"/></returns>
        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !(left == right);
        }
    }
}