using Solyanka.Events.Abstractions;
using Solyanka.ServiceBus.Abstractions;

namespace Solyanka.Events.IntegrationEvents
{
    /// <summary>
    /// <see cref="IEventHandler{TEvent}"/> that queue integration events for publishing
    /// </summary>
    public class IntegrationEventHandler : SyncEventHandler<IIntegrationEvent>
    {
        private readonly IServiceBus _serviceBus;


        /// <summary/>
        public IntegrationEventHandler(IServiceBus serviceBus)
        {
            _serviceBus = serviceBus;
        }


        /// <inheritdoc />
        protected override void Handle(IEvent @event)
        {
            _serviceBus.AddEvent(@event);
        }
    }
}