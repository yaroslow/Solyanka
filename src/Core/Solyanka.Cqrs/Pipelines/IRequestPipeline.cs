using Solyanka.Cqrs.Requests;
using Solyanka.Utils;

namespace Solyanka.Cqrs.Pipelines;

/// <summary>
/// Pipeline of <see cref="IRequest{TOut}"/>
/// </summary>
/// <typeparam name="TOut">Output data type</typeparam>
public interface IRequestPipeline<TOut>
{
    /// <summary>
    /// Build and run pipeline
    /// </summary>
    /// <param name="request">Request that implements <see cref="IRequest{TOut}"/></param>
    /// <param name="serviceFactory"><see cref="ServiceFactory"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="ServiceFactory"/></returns>
    Task<TOut> Process(IRequest<TOut> request, ServiceFactory serviceFactory, CancellationToken cancellationToken);
}