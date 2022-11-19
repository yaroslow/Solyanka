using Solyanka.Cqrs.Requests;

namespace Solyanka.Cqrs.Events;

/// <summary>
/// Store of <see cref="IEvent"/>
/// </summary>
public interface IEventStore : IEventStorable, IEventRaisable {}