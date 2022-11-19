using Solyanka.Utils;

namespace Solyanka.Cqrs.Requests;

/// <summary>
/// <see cref="IRequest"/> that notify about event
/// </summary>
public interface IEvent : IRequest<VoidResult> {}