using System.Threading;
using System.Threading.Tasks;

namespace Solyanka.Events.Abstractions
{
    /// <summary>
    /// Dispacther handling <see cref="IEvent"/>
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
        Task Notify<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent;
    }
}