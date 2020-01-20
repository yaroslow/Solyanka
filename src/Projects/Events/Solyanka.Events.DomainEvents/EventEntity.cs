using System.Collections.Generic;
using Solyanka.Domain.Abstractions.Abstractions;
using Solyanka.Events.Abstractions;

namespace Solyanka.Events.DomainEvents
{
    /// <summary>
    /// Entity containing domain events
    /// </summary>
    /// <typeparam name="TIdentifier"></typeparam>
    public abstract class EventEntity<TIdentifier> : Entity<TIdentifier>, IEventContainer
    {
        /// <summary>
        /// Event list that need to be handled
        /// </summary>
        public virtual IList<IEvent> Events { get; set; }
    }
}