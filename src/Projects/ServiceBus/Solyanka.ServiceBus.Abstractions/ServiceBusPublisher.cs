using System.Threading;
using System.Threading.Tasks;

namespace Solyanka.ServiceBus.Abstractions
{
    /// <summary>
    /// Delegate defining the procedure for publishing the object to service bus
    /// </summary>
    /// <param name="objToPublish">Object that needs to be published</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    public delegate Task ServiceBusPublisher(object objToPublish, CancellationToken cancellationToken);
    
    /// <summary>
    /// Class-extension over <see cref="ServiceBusPublisher"/>
    /// </summary>
    public static class ServiceBusPublisherExtensions
    {
        /// <summary>
        /// Asyncronously publish object to service bus
        /// </summary>
        /// <param name="publisher"><see cref="ServiceBusPublisher"/></param>
        /// <param name="objToPublish">Object that needs to be published</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns></returns>
        public static async Task PublishAsync(this ServiceBusPublisher publisher, object objToPublish, CancellationToken cancellationToken = default)
        {
            await publisher(objToPublish, cancellationToken);
        }

        /// <summary>
        /// Publish object to service bus
        /// </summary>
        /// <param name="publisher"><see cref="ServiceBusPublisher"/></param>
        /// <param name="objToPublish">Object that needs to be published</param>
        public static void Publish(this ServiceBusPublisher publisher, object objToPublish)
        {
            publisher(objToPublish, CancellationToken.None).GetAwaiter().GetResult();
        }
    }
}