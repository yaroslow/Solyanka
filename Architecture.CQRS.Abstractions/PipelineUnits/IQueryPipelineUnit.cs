using Architecture.CQRS.Abstractions.Requests;

namespace Architecture.CQRS.Abstractions.PipelineUnits
{
    public interface IQueryPipelineUnit<in TIn, TOut> : IRequestPipelineUnit<TIn, TOut> where TIn : IQuery<TOut> {}
}
