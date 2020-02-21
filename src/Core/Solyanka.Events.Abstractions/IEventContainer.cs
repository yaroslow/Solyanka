using System.Threading;
using System.Threading.Tasks;

namespace Solyanka.Events.Abstractions
{
    /// <summary>
    /// Container of <see cref="IEvent"/>
    /// </summary>
    public interface IEventContainer
    {
        /// <summary>
        /// Add <see cref="IEvent"/> to container
        /// </summary>
        /// <param name="event"><see cref="IEvent"/></param>
        void Push(IEvent @event);

        /// <summary>
        /// Clear container
        /// </summary>
        void Clear();
        
        /// <summary>
        /// Handle all <see cref="IEvent"/> in container
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Task"/></returns>
        Task Handle(CancellationToken cancellationToken = default);
    }
}