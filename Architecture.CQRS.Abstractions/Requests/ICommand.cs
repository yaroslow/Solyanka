namespace Architecture.CQRS.Abstractions.Requests
{
    public interface ICommand<out TOut> : IRequest<TOut> {}

    public interface ICommand : ICommand<VoidResult> {}
}
