using Solyanka.Cqrs.Requests;

namespace Solyanka.Cqrs.Pipelines
{
    /// <summary>
    /// Pipeline of <see cref="IQuery{TOut}"/>
    /// </summary>
    /// <typeparam name="TIn">Input data type implementing <see cref="IQuery{TOut}"/></typeparam>
    /// <typeparam name="TOut">Output data type</typeparam>
    public interface IQueryPipeline<in TIn, TOut> : IRequestPipeline<TOut> where TIn : IQuery<TOut> {}
}
