using System;
using System.Collections.Generic;
using Solyanka.Cqrs.Requests;

namespace Solyanka.Cqrs.Events
{
    /// <summary>
    /// Container of <see cref="IEvent"/> that can be stored
    /// </summary>
    public interface IEventStorable
    {
        /// <summary>
        /// Collection of events
        /// </summary>
        ICollection<KeyValuePair<DateTimeOffset, IEvent>> Events { get; }
    }
}