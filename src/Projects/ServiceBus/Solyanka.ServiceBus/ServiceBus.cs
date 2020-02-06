using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solyanka.Events.Abstractions;
using Solyanka.ServiceBus.Abstractions;

namespace Solyanka.ServiceBus
{
    /// <inheritdoc cref="IServiceBus" />
    public class ServiceBus : IServiceBus
    {
        private readonly ServiceBusPublisher _publisher;
        
        /// <inheritdoc />
        public IList<IEvent> Events { get; }

        
        /// <summary/>
        public ServiceBus(ServiceBusPublisher publisher)
        {
            _publisher = publisher;
            Events = new List<IEvent>();
        }
        

        /// <inheritdoc />
        public void AddEvent(IEvent @event)
        {
            Events.Add(@event);
        }

        /// <inheritdoc />
        public void ClearEvents()
        {
            Events.Clear();
        }

        /// <inheritdoc />
        public async Task PublishAsync(CancellationToken cancellationToken = default)
        {
            foreach (var @event in Events)
            {
                await _publisher.PublishAsync(@event, cancellationToken);
            }
            ClearEvents();
        }
    }
}