using System.Threading;
using System.Threading.Tasks;
using Solyanka.Cqs.Abstractions.Requests;
using Solyanka.Utils;

namespace Solyanka.Cqs.Abstractions.Pipelines
{
    /// <summary>
    /// Pipeline of <see cref="IRequest{TOut}"/>
    /// </summary>
    /// <typeparam name="TOut">Output data type</typeparam>
    public interface IRequestPipeline<TOut>
    {
        /// <summary>
        /// Build and run pipeline
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <param name="serviceFactory"><see cref="Task{TResult}"/></param>
        /// <returns><see cref="ServiceFactory"/></returns>
        Task<TOut> ProcessPipeline(IRequest<TOut> request, CancellationToken cancellationToken, ServiceFactory serviceFactory);
    }
}
