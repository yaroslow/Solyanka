using System.Threading;
using System.Threading.Tasks;

namespace Solyanka.Events.Abstractions
{
    /// <summary>
    /// Dispacther handling events
    /// </summary>
    public interface IEventDispatcher
    {
        /// <summary>
        /// Notify handlers
        /// </summary>
        /// <typeparam name="TEvent">Event type</typeparam>
        /// <param name="event">Event</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Task"/></returns>
        Task NotifyAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent;
    }
}