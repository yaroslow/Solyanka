using Architecture.CQRS.Abstractions.Requests;

namespace Architecture.CQRS.Abstractions.Handlers
{
    public interface IQueryHandler<in TIn, TOut> : IRequestHandler<TIn, TOut> where TIn : IQuery<TOut> {}
}
