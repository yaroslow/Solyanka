using System.Threading.Tasks;

namespace Solyanka.ServiceBus.Abstractions
{
    /// <summary>
    /// Publisher of integration events
    /// </summary>
    public interface IIntegrationEventsPublisher
    {
        /// <summary>
        /// Queue integration event
        /// </summary>
        /// <param name="integrationEvent">Integration event</param>
        void Publish(object integrationEvent);

        /// <summary>
        /// Publish all queued integration events to service bus
        /// </summary>
        /// <returns>Result of publishing</returns>
        Task PublishAll();
        
        /// <summary>
        /// Publish all integration events to service bus
        /// </summary>
        /// <param name="integrationEvent">Integration event</param>
        /// <returns>Result of publishing</returns>
        Task PublishInBus(object integrationEvent);
    }
}