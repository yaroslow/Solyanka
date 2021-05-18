using Solyanka.Cqrs.Requests;

namespace Solyanka.Cqrs.CrossCuttingConcerns
{
    /// <summary>
    /// Cross cutting concern of <see cref="ICommand{TOut}"/> pipeline
    /// </summary>
    /// <typeparam name="TIn">Input data type implementing <see cref="ICommand{TOut}"/></typeparam>
    /// <typeparam name="TOut">Output data type</typeparam>
    public interface ICommandCrossCuttingConcern<in TIn, TOut> : IRequestCrossCuttingConcern<TIn, TOut> where TIn : ICommand<TOut> {}
}
