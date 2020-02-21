namespace Solyanka.Cqs.Abstractions.Requests
{
    /// <summary>
    /// <see cref="IRequest{TOut}"/> that gets data
    /// </summary>
    /// <typeparam name="TOut">Output data type</typeparam>
    public interface IQuery<out TOut> : IRequest<TOut> {}
}
