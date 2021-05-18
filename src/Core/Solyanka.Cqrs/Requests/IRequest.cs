namespace Solyanka.Cqrs.Requests
{
    /// <summary>
    /// Request that returned data
    /// </summary>
    /// <typeparam name="TOut">Output data type</typeparam>
    public interface IRequest<out TOut> : IRequest {}
    
    /// <summary>
    /// Request
    /// </summary>
    public interface IRequest {}
}