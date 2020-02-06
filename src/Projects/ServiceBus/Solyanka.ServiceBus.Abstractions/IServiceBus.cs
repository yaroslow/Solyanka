using System.Threading;
using System.Threading.Tasks;
using Solyanka.Events.Abstractions;

namespace Solyanka.ServiceBus.Abstractions
{
    /// <summary>
    /// Bus that connect and coordinate services
    /// </summary>
    public interface IServiceBus : IEventContainer
    {
        /// <summary>
        /// Publish integration events in bus
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>Result of publishing</returns>
        Task PublishAsync(CancellationToken cancellationToken = default);
    }
}