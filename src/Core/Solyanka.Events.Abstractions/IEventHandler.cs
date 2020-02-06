using System.Threading;
using System.Threading.Tasks;
using Solyanka.Utils;

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

    /// <summary>
    /// Sync event hanlder
    /// </summary>
    /// <typeparam name="TEvent">Event type</typeparam>
    public abstract class SyncEventHandler<TEvent> : IEventHandler<TEvent> where TEvent : IEvent
    {
        /// <inheritdoc />
        public async Task Handle(TEvent @event, CancellationToken cancellationToken)
        {
            Handle(@event);
            await VoidResult.TaskValue;
        }

        /// <summary>
        /// Sync handling
        /// </summary>
        /// <param name="event"></param>
        protected abstract void Handle(IEvent @event);
    }
}
