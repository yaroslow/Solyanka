using System.Threading;
using System.Threading.Tasks;

namespace Solyanka.Events.Abstractions
{
    /// <summary>
    /// Event handler
    /// </summary>
    /// <typeparam name="TEvent">Event type</typeparam>
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        /// <summary>
        /// Handling
        /// </summary>
        /// <param name="event">Event implementing <see cref="IEvent"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Task"/></returns>
        Task Handle(TEvent @event, CancellationToken cancellationToken);
    }
}
