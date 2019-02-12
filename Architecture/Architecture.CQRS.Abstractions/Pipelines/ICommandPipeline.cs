using Architecture.CQRS.Abstractions.Requests;

namespace Architecture.CQRS.Abstractions.Pipelines
{
    public interface ICommandPipeline<in TIn, TOut> : IRequestPipeline<TOut> where TIn : ICommand<TOut> {}
}
