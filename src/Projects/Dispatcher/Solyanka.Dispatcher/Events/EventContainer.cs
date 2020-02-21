using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solyanka.Cqs.Abstractions.Requests;
using Solyanka.Events.Abstractions;

namespace Solyanka.Dispatcher.Events
{
    /// <summary>
    /// <see cref="IEventContainer"/> that push all events
    /// occured during handling <see cref="ICommand"/> and <see cref="IQuery{TOut}"/>
    /// </summary>
    public class EventContainer : IEventContainer
    {
        private readonly IEventDispatcher _dispatcher;
        
        private ICollection<IEvent> Events { get; }
        
        
        /// <summary/>
        public EventContainer(IEventDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            Events = new List<IEvent>();
        }


        /// <inheritdoc />
        public void Push(IEvent @event) => Events.Add(@event);

        /// <inheritdoc />
        public void Clear() => Events.Clear();
        
        /// <inheritdoc />
        public async Task Handle(CancellationToken cancellationToken = default)
        {
            foreach (var @event in Events)
            {
                await _dispatcher.Notify(@event, cancellationToken);
            }
            Clear();
        }
    }
}