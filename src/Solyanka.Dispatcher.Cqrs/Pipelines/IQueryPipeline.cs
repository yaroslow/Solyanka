using Solyanka.Dispatcher.Cqrs.Requests;

namespace Solyanka.Dispatcher.Cqrs.Pipelines
{
    /// <summary>
    /// Query pipeline
    /// </summary>
    /// <typeparam name="TIn">Input data type implementing <see cref="IQuery{TOut}"/></typeparam>
    /// <typeparam name="TOut">Output data type</typeparam>
    public interface IQueryPipeline<in TIn, TOut> : IRequestPipeline<TOut> where TIn : IQuery<TOut> {}
}
