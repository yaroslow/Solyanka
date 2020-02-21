using System.Threading;
using System.Threading.Tasks;
using Solyanka.Cqs.Abstractions.Requests;

namespace Solyanka.Cqs.Abstractions.Handlers
{
    /// <summary>
    /// Handler of <see cref="IRequest{TOut}"/>
    /// </summary>
    /// <typeparam name="TIn">Input data type implementing <see cref="IRequest{TOut}"/></typeparam>
    /// <typeparam name="TOut">Output data type</typeparam>
    public interface IRequestHandler<in TIn, TOut> where TIn : IRequest<TOut>
    {
        /// <summary>
        /// Handling
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Task{TOut}"/></returns>
        Task<TOut> Handle(TIn request, CancellationToken cancellationToken);
    }
}
