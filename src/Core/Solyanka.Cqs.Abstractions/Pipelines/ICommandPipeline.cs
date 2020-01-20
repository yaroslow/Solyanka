using Solyanka.Cqs.Abstractions.Requests;

namespace Solyanka.Cqs.Abstractions.Pipelines
{
    /// <summary>
    /// Command pipeline
    /// </summary>
    /// <typeparam name="TIn">Input data type implementing <see cref="ICommand{TOut}"/></typeparam>
    /// <typeparam name="TOut">Output data type</typeparam>
    public interface ICommandPipeline<in TIn, TOut> : IRequestPipeline<TOut> where TIn : ICommand<TOut> {}
}
