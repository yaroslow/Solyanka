using Solyanka.Cqrs.Requests;

namespace Solyanka.Cqrs.Pipelines;

/// <summary>
/// Pipeline of <see cref="ICommand{TOut}"/>
/// </summary>
/// <typeparam name="TIn">Input data type implementing <see cref="ICommand{TOut}"/></typeparam>
/// <typeparam name="TOut">Output data type</typeparam>
public interface ICommandPipeline<in TIn, TOut> : IRequestPipeline<TOut> where TIn : ICommand<TOut> {}