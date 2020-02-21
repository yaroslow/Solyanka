using Solyanka.Cqs.Abstractions.Requests;

namespace Solyanka.Cqs.Abstractions.PipelineUnits
{
    /// <summary>
    /// Pipeline stage of <see cref="IQuery{TOut}"/> pipeline
    /// </summary>
    /// <typeparam name="TIn">Input data type implementing <see cref="IQuery{TOut}"/></typeparam>
    /// <typeparam name="TOut">Output data type</typeparam>
    public interface IQueryPipelineUnit<in TIn, TOut> : IRequestPipelineUnit<TIn, TOut> where TIn : IQuery<TOut> {}
}
