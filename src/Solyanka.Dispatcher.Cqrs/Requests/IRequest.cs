namespace Solyanka.Dispatcher.Cqrs.Requests
{
    /// <summary>
    /// Request interface
    /// </summary>
    /// <typeparam name="TOut">Output data type</typeparam>
    public interface IRequest<out TOut> {}
}
