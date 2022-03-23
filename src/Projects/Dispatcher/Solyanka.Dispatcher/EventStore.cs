using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Solyanka.Cqrs;
using Solyanka.Cqrs.Events;
using Solyanka.Cqrs.Requests;

namespace Solyanka.Dispatcher
{
    /// <summary>
    /// <see cref="IEventStore"/> and <see cref="IEventRaisable"/> that push all events to <see cref="IRequestDispatcher"/>
    /// occured during handling <see cref="ICommand"/> and <see cref="IQuery{TOut}"/>
    /// </summary>
    public class EventStore : IEventStore, IEventRaisable
    {
        private readonly IRequestDispatcher _dispatcher;


        /// <summary>
        /// Constructor of <see cref="EventStore"/>
        /// </summary>
        /// <param name="dispatcher"><see cref="IRequestDispatcher"/></param>
        public EventStore(IRequestDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            Events = new ConcurrentDictionary<DateTimeOffset, IEvent>();
        }


        /// <inheritdoc />
        public ICollection<KeyValuePair<DateTimeOffset, IEvent>> Events { get; }

        
        /// <inheritdoc />
        public async Task Raise(CancellationToken cancellationToken = default)
        {
            foreach (var (_, @event) in Events.OrderBy(a => a.Key))
            {
                if (@event != null)
                {
                    await _dispatcher.Handle(@event, cancellationToken);
                }
            }

            Events.Clear();
        }
    }
}