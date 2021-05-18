using System.Threading;
using System.Threading.Tasks;
using Solyanka.Cqrs.Requests;

namespace Solyanka.Cqrs.Handlers
{
    /// <summary>
    /// Handler of <see cref="IQuery{TOut}"/>
    /// </summary>
    /// <typeparam name="TIn">Input data type implementing <see cref="IQuery{TOut}"/></typeparam>
    /// <typeparam name="TOut">Output data type</typeparam>
    public interface IQueryHandler<in TIn, TOut> : IRequestHandler<TIn, TOut> where TIn : IQuery<TOut> {}
    
    /// <summary>
    /// Sync handler of <see cref="IQuery{TOut}"/>
    /// </summary>
    /// <typeparam name="TIn">Input data type implementing <see cref="IQuery{TOut}"/></typeparam>
    /// <typeparam name="TOut">Output data type</typeparam>
    public interface ISyncQueryHandler<in TIn, TOut> : IQueryHandler<TIn, TOut> where TIn : IQuery<TOut>
    {
        /// <summary>
        /// Sync handling
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Output value of specified type</returns>
        TOut Handle(TIn request);


        Task<TOut> IRequestHandler<TIn, TOut>.Handle(TIn request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(Handle(request));
        }
    }
}
