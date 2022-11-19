using Solyanka.Cqrs.Handlers;
using Solyanka.ServiceBus.Abstractions;

namespace Solyanka.ServiceBus;

/// <summary>
/// <see cref="IEventHandler{TEvent}"/> that publish <see cref="IIntegrationEvent"/> to <see cref="IServiceBus"/>
/// </summary>
public class IntegrationEventsHandler : IEventHandler<IIntegrationEvent>
{
    /// <summary>
    /// <see cref="IServiceBus"/>
    /// </summary>
    private readonly IServiceBus _serviceBus;


    /// <summary>
    /// Constructor of <see cref="IntegrationEventsHandler"/>
    /// </summary>
    /// <param name="serviceBus"><see cref="IServiceBus"/></param>
    public IntegrationEventsHandler(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }


    /// <inheritdoc cref="IEventHandler{TEvent}.Handle" />
    public async Task Handle(IIntegrationEvent @event, CancellationToken cancellationToken)
    {
        await _serviceBus.Publish(@event, cancellationToken);
    }
}