namespace Solyanka.Cqrs.Requests;

/// <summary>
/// <see cref="IRequest{TOut}"/> that query data
/// </summary>
/// <typeparam name="TOut">Output data type</typeparam>
public interface IQuery<out TOut> : IRequest<TOut> {}