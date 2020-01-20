using System.Threading;
using System.Threading.Tasks;
using Solyanka.Cqs.Abstractions.Requests;

namespace Solyanka.Cqs.Abstractions.Handlers
{
    /// <summary>
    /// Query handler
    /// </summary>
    /// <typeparam name="TIn">Input data type implementing <see cref="IQuery{TOut}"/></typeparam>
    /// <typeparam name="TOut">Output data type</typeparam>
    public interface IQueryHandler<in TIn, TOut> : IRequestHandler<TIn, TOut> where TIn : IQuery<TOut> {}
    
    /// <summary>
    /// Sync query handler
    /// </summary>
    /// <typeparam name="TIn">Input data type implementing <see cref="IQuery{TOut}"/></typeparam>
    /// <typeparam name="TOut">Output data type</typeparam>
    public abstract class SyncQueryHandler<TIn, TOut> : IQueryHandler<TIn, TOut> where TIn : IQuery<TOut>
    {
        /// <inheritdoc />
        Task<TOut> IRequestHandler<TIn, TOut>.Handle(TIn request, CancellationToken cancellationToken) =>
            Task.FromResult(Handle(request));

        /// <summary>
        /// Sync handling
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Output value of specified type</returns>
        protected abstract TOut Handle(TIn request);
    }
}
