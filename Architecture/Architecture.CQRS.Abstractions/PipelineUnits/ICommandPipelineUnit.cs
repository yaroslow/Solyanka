using Architecture.CQRS.Abstractions.Requests;

namespace Architecture.CQRS.Abstractions.PipelineUnits
{
    public interface ICommandPipelineUnit<in TIn, TOut> : IRequestPipelineUnit<TIn, TOut> where TIn : ICommand<TOut> {}
}
