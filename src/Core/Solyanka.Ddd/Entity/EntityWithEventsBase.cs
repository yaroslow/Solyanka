using System;
using System.Collections.Generic;
using Solyanka.Cqrs.Events;
using Solyanka.Cqrs.Requests;

namespace Solyanka.Ddd.Entity
{
    /// <summary>
    /// Entity that has inner collection of <see cref="IEvent"/>.
    /// (It is necessary to make <c>Events</c> not mappable to Db/Storage)
    /// </summary>
    /// <typeparam name="TId">Type of identificator</typeparam>
    public abstract class EntityWithEventsBase<TId> : EntityBase<TId>, IEventStorable where TId: IEquatable<TId>
    {
        /// <summary>
        /// Protected default constructor
        /// </summary>
        protected EntityWithEventsBase() {}

        /// <inheritdoc />
        public EntityWithEventsBase(TId id) : base(id)
        {
            Events = new Dictionary<DateTimeOffset, IEvent>();
        }


        /// <inheritdoc />
        public ICollection<KeyValuePair<DateTimeOffset, IEvent>> Events { get; }
    }
}