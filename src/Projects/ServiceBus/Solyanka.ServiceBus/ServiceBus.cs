using Solyanka.ServiceBus.Abstractions;

namespace Solyanka.ServiceBus;

/// <inheritdoc />
public class ServiceBus : IServiceBus
{
    private readonly ServiceBusPublisher _publisher;

        
    /// <summary/>
    public ServiceBus(ServiceBusPublisher publisher)
    {
        _publisher = publisher;
    }
        

    /// <inheritdoc />
    public async Task Publish(IIntegrationEvent @event, CancellationToken cancellationToken = default)
    {
        await _publisher.Publish(@event, cancellationToken);
    }
}