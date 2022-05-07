using System.Threading;
using System.Threading.Tasks;
using Solyanka.Cqrs.Requests;
using Solyanka.Utils;

namespace Solyanka.Cqrs.Handlers
{
    /// <summary>
    /// Handler of <see cref="IEvent"/>
    /// </summary>
    /// <typeparam name="TEvent">Event type</typeparam>
    public interface IEventHandler<in TEvent> : IRequestHandler<TEvent, VoidResult> where TEvent : IEvent
    {
        /// <summary>
        /// Handling
        /// </summary>
        /// <param name="request">Event that implements <see cref="IEvent"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Task"/></returns>
        new Task Handle(TEvent request, CancellationToken cancellationToken);

        
        async Task<VoidResult> IRequestHandler<TEvent, VoidResult>.Handle(TEvent request, CancellationToken cancellationToken)
        {
            await Handle(request, cancellationToken);
            return await VoidResult.TaskValue;
        }
    }

    /// <summary>
    /// Sync handler of <see cref="IEvent"/>
    /// </summary>
    /// <typeparam name="TEvent">Event type</typeparam>
    public interface ISyncEventHandler<in TEvent> : IEventHandler<TEvent> where TEvent : IEvent
    {
        /// <summary>
        /// Sync handling
        /// </summary>
        /// <param name="request">Event that implements <see cref="IEvent"/></param>
        void Handle(TEvent request);

        
        Task IEventHandler<TEvent>.Handle(TEvent request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Handle(request);
            return VoidResult.TaskValue;
        }
    }
}
