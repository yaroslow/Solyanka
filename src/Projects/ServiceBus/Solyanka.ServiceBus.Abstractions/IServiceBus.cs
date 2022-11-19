namespace Solyanka.ServiceBus.Abstractions;

/// <summary>
/// Bus that connect and coordinate services
/// </summary>
public interface IServiceBus
{
    /// <summary>
    /// Publish <see cref="IIntegrationEvent"/> in bus
    /// </summary>
    /// <param name="event"><see cref="IIntegrationEvent"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Result of publishing</returns>
    Task Publish(IIntegrationEvent @event, CancellationToken cancellationToken = default);
}