using Architecture.CQRS.Abstractions.Requests;

namespace Architecture.CQRS.Abstractions.Handlers
{
    public interface ICommandHandler<in TIn, TOut> : IRequestHandler<TIn, TOut> where TIn : ICommand<TOut> {}
}
