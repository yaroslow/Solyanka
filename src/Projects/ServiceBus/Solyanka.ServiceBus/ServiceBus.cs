using System.Threading.Tasks;
using MassTransit;
using Solyanka.ServiceBus.Abstractions;

namespace Solyanka.ServiceBus
{
    /// <inheritdoc />
    public class ServiceBus : IServiceBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        
        /// <summary/>
        public ServiceBus(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        
        /// <inheritdoc />
        public async Task Publish(object integrationEvent)
        {
            await _publishEndpoint.Publish(integrationEvent);
        }
    }
}