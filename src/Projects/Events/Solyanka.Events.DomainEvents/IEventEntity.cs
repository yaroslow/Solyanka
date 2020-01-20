using Solyanka.Domain.Abstractions.Abstractions.MarkerInterfaces;
using Solyanka.Events.Abstractions;

namespace Solyanka.Events.DomainEvents
{
    /// <summary>
    /// Entity containing domain events
    /// </summary>
    public interface IEventEntity : IEntity, IEventContainer {}
}