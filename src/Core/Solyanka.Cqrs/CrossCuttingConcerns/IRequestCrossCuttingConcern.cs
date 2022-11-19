using Solyanka.Cqrs.Requests;

namespace Solyanka.Cqrs.CrossCuttingConcerns;

/// <summary>
/// Cross cutting concern
/// </summary>
/// <typeparam name="TIn">Input data type implementing <see cref="IRequest"/></typeparam>
/// <typeparam name="TOut">Output data type</typeparam>
public interface IRequestCrossCuttingConcern<in TIn, TOut> where TIn : IRequest<TOut>
{
    /// <summary>
    /// Concern pipeline stage
    /// </summary>
    /// <param name="request">Request that implements <see cref="IRequest{TOut}"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <param name="next"><see cref="RequestHandlerDelegate{TOut}"/></param>
    /// <returns><see cref="Task{TOut}"/></returns>
    Task<TOut> Concern(TIn request, RequestHandlerDelegate<TOut> next, CancellationToken cancellationToken);
}