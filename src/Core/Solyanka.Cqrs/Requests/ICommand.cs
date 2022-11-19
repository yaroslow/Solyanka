using Solyanka.Utils;

namespace Solyanka.Cqrs.Requests;

/// <summary>
/// <see cref="IRequest{TOut}"/> that executes command with output data
/// </summary>
/// <typeparam name="TOut">Output data type</typeparam>
public interface ICommand<out TOut> : IRequest<TOut> {}

/// <summary>
/// <see cref="IRequest{TOut}"/> that executes command without output data
/// </summary>
public interface ICommand : ICommand<VoidResult> {}