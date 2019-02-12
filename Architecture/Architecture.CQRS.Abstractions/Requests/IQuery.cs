namespace Architecture.CQRS.Abstractions.Requests
{
    public interface IQuery<out TOut> : IRequest<TOut> {}
}
