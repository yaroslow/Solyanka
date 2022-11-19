using Solyanka.Cqrs.Requests;
using Solyanka.Utils;

namespace Solyanka.Cqrs.Handlers;

/// <summary>
/// Handler of <see cref="ICommand{TOut}"/>
/// </summary>
/// <typeparam name="TIn">Input data type implementing <see cref="ICommand"/></typeparam>
/// <typeparam name="TOut">Output data type</typeparam>
public interface ICommandHandler<in TIn, TOut> : IRequestHandler<TIn, TOut> where TIn : ICommand<TOut> {}

/// <summary>
/// Sync handler of <see cref="ICommand"/>
/// </summary>
/// <typeparam name="TIn">Input data type implementing <see cref="ICommand{TOut}"/></typeparam>
/// <typeparam name="TOut">Output data type</typeparam>
public interface ISyncCommandHandler<in TIn, TOut> : ICommandHandler<TIn, TOut> where TIn : ICommand<TOut>
{
    /// <summary>
    /// Sync handling
    /// </summary>
    /// <param name="request">Command that implements <see cref="ICommand{TOut}"/></param>
    /// <returns>Output value of specified type</returns>
    TOut Handle(TIn request);


    Task<TOut> IRequestHandler<TIn, TOut>.Handle(TIn request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(Handle(request));
    }
}

/// <summary>
/// Handler of <see cref="ICommand"/>
/// </summary>
/// <typeparam name="TIn">Input data type implementing <see cref="ICommand"/></typeparam>
public interface ICommandHandler<in TIn> : ICommandHandler<TIn, VoidResult> where TIn : ICommand
{
    /// <summary>
    /// Handling
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    new Task Handle(TIn request, CancellationToken cancellationToken);
        

    async Task<VoidResult> IRequestHandler<TIn, VoidResult>.Handle(TIn request, CancellationToken cancellationToken)
    {
        await Handle(request, cancellationToken);
        return await VoidResult.TaskValue;
    }
}
    
/// <summary>
/// Sync handler of <see cref="ICommand"/>
/// </summary>
/// <typeparam name="TIn">Input data type implementing <see cref="ICommand"/></typeparam>
public interface ISyncCommandHandler<in TIn> : ISyncCommandHandler<TIn, VoidResult> where TIn : ICommand
{
    /// <summary>
    /// Sync handling
    /// </summary>
    /// <param name="request">Request</param>
    new void Handle(TIn request);


    VoidResult ISyncCommandHandler<TIn, VoidResult>.Handle(TIn request)
    {
        Handle(request);
        return VoidResult.Value;
    }
}