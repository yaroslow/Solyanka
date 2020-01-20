using System.Threading.Tasks;

namespace Solyanka.ServiceBus.Abstractions
{
    /// <summary>
    /// Bus that connect and coordinate services
    /// </summary>
    public interface IServiceBus
    {
        /// <summary>
        /// Publish integration event in bus
        /// </summary>
        /// <param name="integrationEvent">Integration event</param>
        /// <returns>Result of publishing</returns>
        Task Publish(object integrationEvent);
    }
}