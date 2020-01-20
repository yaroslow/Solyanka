using System.Threading;
using System.Threading.Tasks;
using Solyanka.Events.Abstractions;
using Solyanka.ServiceBus.Abstractions;

namespace Solyanka.Events.IntegrationEvents
{
    /// <summary>
    /// <see cref="IEventHandler{TEvent}"/> that publishes integration events to service bus
    /// </summary>
    public class IntegrationEventHandler : IEventHandler<IIntegrationEvent>
    {
        private readonly IIntegrationEventsPublisher _eventsPublisher;


        /// <summary/>
        public IntegrationEventHandler(IIntegrationEventsPublisher eventsPublisher)
        {
            _eventsPublisher = eventsPublisher;
        }


        /// <inheritdoc />
        public async Task Handle(IIntegrationEvent @event, CancellationToken cancellationToken)
        {
            await _eventsPublisher.PublishInBus(@event);
        }
    }
}