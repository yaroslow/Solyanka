using Solyanka.Cqs.Abstractions.Requests;

namespace Solyanka.Cqs.Abstractions.PipelineUnits
{
    /// <summary>
    /// Pipeline stage of <see cref="ICommand{TOut}"/> pipeline
    /// </summary>
    /// <typeparam name="TIn">Input data type implementing <see cref="ICommand"/></typeparam>
    /// <typeparam name="TOut">Output data type</typeparam>
    public interface ICommandPipelineUnit<in TIn, TOut> : IRequestPipelineUnit<TIn, TOut> where TIn : ICommand<TOut> {}
}
