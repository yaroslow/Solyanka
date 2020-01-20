using System.Collections.Generic;
using System.Threading.Tasks;
using Solyanka.ServiceBus.Abstractions;

namespace Solyanka.ServiceBus
{
    /// <inheritdoc />
    public class IntegrationEventsPublisher: IIntegrationEventsPublisher
    {
        private readonly IServiceBus _serviceBus;
        private readonly List<object> _events = new List<object>();

        
        /// <summary/>
        public IntegrationEventsPublisher(IServiceBus serviceBus)
        {
            _serviceBus = serviceBus;
        }

        
        /// <inheritdoc />
        public void Publish(object integrationEvent)
        {
            if (integrationEvent != null)
            {
                _events.Add(integrationEvent);
            }
        }

        /// <inheritdoc />
        public async Task PublishAll()
        {
            foreach (var integrationEvent in _events)
            {
                await PublishInBus(integrationEvent);
            }
        }

        /// <inheritdoc />
        public async Task PublishInBus(object integrationEvent)
        {
            await _serviceBus.Publish(integrationEvent);
        }
    }
}