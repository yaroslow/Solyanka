using System.Threading;
using System.Threading.Tasks;
using Solyanka.Cqs.Abstractions.Requests;

namespace Solyanka.Cqs.Abstractions.PipelineUnits
{
    /// <summary>
    /// Pipeline stage
    /// </summary>
    /// <typeparam name="TIn">Input data type implementing <see cref="IRequest{TOut}"/></typeparam>
    /// <typeparam name="TOut">Output data type</typeparam>
    public interface IRequestPipelineUnit<in TIn, TOut> where TIn : IRequest<TOut>
    {
        /// <summary>
        /// Handling pipeline stage
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <param name="next"><see cref="RequestHandlerDelegate{TOut}"/></param>
        /// <returns><see cref="Task{TOut}"/></returns>
        Task<TOut> Handle(TIn request, CancellationToken cancellationToken, RequestHandlerDelegate<TOut> next);
    }
}
