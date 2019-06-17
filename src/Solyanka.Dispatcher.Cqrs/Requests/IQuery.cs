namespace Solyanka.Dispatcher.Cqrs.Requests
{
    /// <summary>
    /// Request to get data
    /// </summary>
    /// <typeparam name="TOut">Output data type</typeparam>
    public interface IQuery<out TOut> : IRequest<TOut> {}
}
