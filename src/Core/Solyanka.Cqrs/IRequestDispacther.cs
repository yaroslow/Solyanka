using Solyanka.Cqrs.Requests;

namespace Solyanka.Cqrs;

/// <summary>
/// Dispatcher handling <see cref="IQuery{TOut}"/> or <see cref="ICommand{TOut}"/>
/// </summary>
public interface IRequestDispatcher
{
    /// <summary>
    /// Handling
    /// </summary>
    /// <typeparam name="TOut">Output data type</typeparam>
    /// <param name="request">Request of type <see cref="IQuery{TOut}"/> or <see cref="ICommand{TOut}"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task{TOut}"/></returns>
    Task<TOut> Handle<TOut>(IRequest<TOut> request, CancellationToken cancellationToken = default);
}