using Solyanka.Dispatcher.Cqrs.Requests;

namespace Solyanka.Dispatcher.Cqrs.PipelineUnits
{
    /// <summary>
    /// Command pipeline stage
    /// </summary>
    /// <typeparam name="TIn">Input data type implementing <see cref="ICommand{TOut}"/></typeparam>
    /// <typeparam name="TOut">Output data type</typeparam>
    public interface ICommandPipelineUnit<in TIn, TOut> : IRequestPipelineUnit<TIn, TOut> where TIn : ICommand<TOut> {}
}
