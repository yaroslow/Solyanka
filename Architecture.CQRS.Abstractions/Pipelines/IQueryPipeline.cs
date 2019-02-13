using Architecture.CQRS.Abstractions.Requests;

namespace Architecture.CQRS.Abstractions.Pipelines
{
    public interface IQueryPipeline<in TIn, TOut> : IRequestPipeline<TOut> where TIn : IQuery<TOut> {}
}
