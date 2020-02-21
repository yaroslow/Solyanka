using System.Threading;
using System.Threading.Tasks;
using Solyanka.Cqs.Abstractions.Requests;
using Solyanka.Utils;

namespace Solyanka.Cqs.Abstractions.Handlers
{
    /// <summary>
    /// Handler of <see cref="ICommand{TOut}"/>
    /// </summary>
    /// <typeparam name="TIn">Input data type implementing <see cref="ICommand"/></typeparam>
    /// <typeparam name="TOut">Output data type</typeparam>
    public interface ICommandHandler<in TIn, TOut> : IRequestHandler<TIn, TOut> where TIn : ICommand<TOut> {}

    /// <summary>
    /// Sync handler of <see cref="ICommand"/>
    /// </summary>
    /// <typeparam name="TIn">Input data type implementing <see cref="ICommand{TOut}"/></typeparam>
    /// <typeparam name="TOut">Тип выходного значения</typeparam>
    public abstract class SyncCommandHandler<TIn, TOut> : ICommandHandler<TIn, TOut> where TIn : ICommand<TOut>
    {
        /// <inheritdoc />
        async Task<TOut> IRequestHandler<TIn, TOut>.Handle(TIn request, CancellationToken cancellationToken) =>
            await Task.FromResult(Handle(request));

        /// <summary>
        /// Sync handling
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Output value of specified type</returns>
        protected abstract TOut Handle(TIn request);
    }

    /// <summary>
    /// Handler of <see cref="ICommand"/>
    /// </summary>
    /// <typeparam name="TIn">Input data type implementing <see cref="ICommand"/></typeparam>
    public abstract class CommandHandler<TIn> : ICommandHandler<TIn, VoidResult> where TIn : ICommand
    {
        /// <inheritdoc />
        async Task<VoidResult> IRequestHandler<TIn, VoidResult>.Handle(TIn request, CancellationToken cancellationToken)
        {
            await Handle(request, cancellationToken);
            return await VoidResult.TaskValue;
        }

        /// <summary>
        /// Handling
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Task"/></returns>
        protected abstract Task Handle(TIn request, CancellationToken cancellationToken);
    }
    
    /// <summary>
    /// Sync handler of <see cref="ICommand"/>
    /// </summary>
    /// <typeparam name="TIn">Input data type implementing <see cref="ICommand"/></typeparam>
    public abstract class SyncCommandHandler<TIn> : ICommandHandler<TIn, VoidResult> where TIn : ICommand
    {
        /// <inheritdoc />
        public async Task<VoidResult> Handle(TIn request, CancellationToken cancellationToken)
        {
            Handle(request);
            return await VoidResult.TaskValue;
        }

        /// <summary>
        /// Sync handling
        /// </summary>
        /// <param name="request">Request</param>
        protected abstract void Handle(TIn request);
    }
}
