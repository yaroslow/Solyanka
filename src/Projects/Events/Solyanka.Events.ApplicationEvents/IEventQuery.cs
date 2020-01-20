using Solyanka.Cqs.Abstractions.Requests;
using Solyanka.Events.Abstractions;

namespace Solyanka.Events.ApplicationEvents
{
    /// <summary>
    /// <see cref="IQuery{TOut}"/> that requires handling events
    /// </summary>
    /// <typeparam name="TOut">Output data type</typeparam>
    public interface IEventQuery<out TOut> : IQuery<TOut>, IEventContainer {}
}