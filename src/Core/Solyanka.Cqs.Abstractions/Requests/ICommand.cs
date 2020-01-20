namespace Solyanka.Cqs.Abstractions.Requests
{
    /// <summary>
    /// Request to execute command with output data
    /// </summary>
    /// <typeparam name="TOut">Output data type</typeparam>
    public interface ICommand<out TOut> : IRequest<TOut> {}

    /// <summary>
    /// Request to execute command without output data
    /// </summary>
    public interface ICommand : ICommand<VoidResult> {}
}
