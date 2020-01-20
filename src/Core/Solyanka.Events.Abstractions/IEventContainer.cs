using System.Collections.Generic;

namespace Solyanka.Events.Abstractions
{
    /// <summary>
    /// Container that contains collection of <see cref="IEvent"/>
    /// </summary>
    public interface IEventContainer
    {
        /// <summary>
        /// Collection of <see cref="IEvent"/>
        /// </summary>
        IList<IEvent> Events { get; set; }

        /// <summary>
        /// Add event to <c>Events</c>
        /// </summary>
        /// <param name="event"><see cref="IEvent"/></param>
        void AddEvent(IEvent @event) => Events.Add(@event);

        /// <summary>
        /// Clear <c>Events</c>
        /// </summary>
        void ClearEvents() => Events.Clear();
    }
}