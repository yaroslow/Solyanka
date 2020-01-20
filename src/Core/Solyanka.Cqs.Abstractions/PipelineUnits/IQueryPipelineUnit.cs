using Solyanka.Cqs.Abstractions.Requests;

namespace Solyanka.Cqs.Abstractions.PipelineUnits
{
    /// <summary>
    /// Query pipeline stage
    /// </summary>
    /// <typeparam name="TIn">Input data type implementing <see cref="IQuery{TOut}"/></typeparam>
    /// <typeparam name="TOut">Output data type</typeparam>
    public interface IQueryPipelineUnit<in TIn, TOut> : IRequestPipelineUnit<TIn, TOut> where TIn : IQuery<TOut> {}
}
