using Solyanka.Cqs.Abstractions.Requests;
using Solyanka.Events.Abstractions;

namespace Solyanka.Events.ApplicationEvents
{
    /// <summary>
    /// <see cref="ICommand{TOut}"/> that requires handling events
    /// </summary>
    /// <typeparam name="TOut">Output data type</typeparam>
    public interface IEventCommand<out TOut> : ICommand<TOut>, IEventContainer {}
    
    /// <summary>
    /// <see cref="ICommand"/> that requires handling events
    /// </summary>
    public interface IEventCommand : ICommand, IEventContainer {}
}