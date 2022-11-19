using Solyanka.Cqrs.Requests;
using Solyanka.Utils;

namespace Solyanka.Cqrs.Pipelines;

/// <summary>
/// Pipeline of <see cref="IEvent"/>
/// </summary>
/// <typeparam name="TIn">Input data type implementing <see cref="IEvent"/></typeparam>
public interface IEventPipeline<in TIn> : IRequestPipeline<VoidResult> where TIn : IEvent {}