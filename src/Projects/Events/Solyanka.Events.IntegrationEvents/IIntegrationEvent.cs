using Solyanka.Events.Abstractions;
using Solyanka.ServiceBus.Abstractions;

namespace Solyanka.Events.IntegrationEvents
{
    /// <summary>
    /// <see cref="IEvent"/> that will be published to <see cref="IServiceBus"/>
    /// </summary>
    public interface IIntegrationEvent : IEvent {}
}