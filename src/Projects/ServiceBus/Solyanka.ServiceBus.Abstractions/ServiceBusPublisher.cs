using System.Threading;
using System.Threading.Tasks;

namespace Solyanka.ServiceBus.Abstractions
{
    /// <summary>
    /// Delegate defining the procedure for publishing the <see cref="IIntegrationEvent"/> to bus
    /// </summary>
    /// <param name="obj">Object that needs to be published</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    public delegate Task ServiceBusPublisher(object obj, CancellationToken cancellationToken);
    
    /// <summary>
    /// Class-extension over <see cref="ServiceBusPublisher"/>
    /// </summary>
    public static class ServiceBusPublisherExtensions
    {
        /// <summary>
        /// Publish <see cref="IIntegrationEvent"/> to service bus
        /// </summary>
        /// <param name="publisher"><see cref="ServiceBusPublisher"/></param>
        /// <param name="event"><see cref="IIntegrationEvent"/> that needs to be published</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Task"/></returns>
        public static async Task Publish(this ServiceBusPublisher publisher, IIntegrationEvent @event, CancellationToken cancellationToken = default)
        {
            await publisher(@event, cancellationToken);
        }
    }
}