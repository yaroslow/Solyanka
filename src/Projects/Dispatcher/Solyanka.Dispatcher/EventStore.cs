using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solyanka.Cqrs;
using Solyanka.Cqrs.Requests;

namespace Solyanka.Dispatcher
{
    /// <summary>
    /// <see cref="IEventStore"/> that push all events
    /// occured during handling <see cref="ICommand"/> and <see cref="IQuery{TOut}"/>
    /// </summary>
    public class EventStore : IEventStore
    {
        private readonly IRequestDispatcher _dispatcher;
        private readonly ICollection<IEvent> _events;


        /// <summary>
        /// Constructor of <see cref="EventStore"/>
        /// </summary>
        /// <param name="dispatcher"><see cref="IRequestDispatcher"/></param>
        public EventStore(IRequestDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            _events = new List<IEvent>();
        }
        
        
        /// <inheritdoc />
        ICollection<IEvent> IEventStore.Events => _events;

        /// <inheritdoc />
        public async Task Raise(CancellationToken cancellationToken = default)
        {
            foreach (var @event in _events)
            {
                await _dispatcher.Handle(@event, cancellationToken);
            }
            _events.Clear();
        }
    }
}