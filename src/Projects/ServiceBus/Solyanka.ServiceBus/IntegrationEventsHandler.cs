using System.Threading;
using System.Threading.Tasks;
using Solyanka.Events.Abstractions;
using Solyanka.ServiceBus.Abstractions;

namespace Solyanka.ServiceBus
{
    /// <summary>
    /// <see cref="IEventHandler{TEvent}"/> that publish <see cref="IIntegrationEvent"/> to <see cref="IServiceBus"/>
    /// </summary>
    public class IntegrationEventsHandler : IEventHandler<IIntegrationEvent>
    {
        private readonly IServiceBus _serviceBus;


        /// <summary/>
        public IntegrationEventsHandler(IServiceBus serviceBus)
        {
            _serviceBus = serviceBus;
        }
        
        
        /// <inheritdoc />
        public async Task Handle(IIntegrationEvent @event, CancellationToken cancellationToken)
        {
            await _serviceBus.Publish(@event, cancellationToken);
        }
    }
}