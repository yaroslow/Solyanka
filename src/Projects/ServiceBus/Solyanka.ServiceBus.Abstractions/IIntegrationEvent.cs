using Solyanka.Cqrs.Requests;

namespace Solyanka.ServiceBus.Abstractions
{
    /// <summary>
    /// <see cref="IEvent"/> that will be published to <see cref="IServiceBus"/>
    /// </summary>
    public interface IIntegrationEvent : IEvent {}
}