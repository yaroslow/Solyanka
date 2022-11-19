using Solyanka.Cqrs.Requests;

namespace Solyanka.Cqrs.Events;

/// <summary>
/// Container of <see cref="IEvent"/> that can be raised
/// </summary>
public interface IEventRaisable
{
    /// <summary>
    /// Raise all <see cref="IEvent"/> in container
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task Raise(CancellationToken cancellationToken = default);
}