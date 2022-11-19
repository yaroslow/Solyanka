using Solyanka.Cqrs.Events;
using Solyanka.Cqrs.Requests;

namespace Solyanka.Ddd.Entity;

/// <summary>
/// Entity that has inner collection of <see cref="IEvent"/>.
/// (It is necessary to make <c>Events</c> not mappable to Db/Storage)
/// </summary>
/// <typeparam name="TId"><see cref="Id{TId}"/></typeparam>
/// <typeparam name="TIdType">Identificator type</typeparam>
public abstract class EntityWithEventsBase<TId, TIdType> : EntityBase<TId, TIdType>, IEventStorable 
    where TId: Id<TIdType>
    where TIdType : IEquatable<TIdType>, IComparable<TIdType>, IComparable
{
    /// <summary>
    /// Workaround constructor of <see cref="EntityWithEventsBase{TId, TIdType}"/>
    /// </summary>
    protected EntityWithEventsBase() {}

    /// <summary>
    /// Constructor of <see cref="EntityWithEventsBase{TId, TIdType}"/>
    /// </summary>
    /// <param name="id">Identificator</param>
    public EntityWithEventsBase(TId id) : base(id) {}


    /// <inheritdoc />
    public ICollection<KeyValuePair<DateTimeOffset, IEvent>> Events { get; } = new Dictionary<DateTimeOffset, IEvent>();
}