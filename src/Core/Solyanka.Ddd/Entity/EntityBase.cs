namespace Solyanka.Ddd.Entity;

/// <summary>
/// Entity
/// </summary>
/// <typeparam name="TId"><see cref="Id{TId}"/></typeparam>
/// <typeparam name="TIdType">Identificator type</typeparam>
public abstract class EntityBase<TId, TIdType> : IEquatable<EntityBase<TId, TIdType>>
    where TId: Id<TIdType>
    where TIdType : IEquatable<TIdType>, IComparable<TIdType>, IComparable
{
    /// <summary>
    /// Identificator
    /// </summary>
    public TId? Id { get; }
        

    /// <summary>
    /// Workaround constructor of <see cref="EntityBase{TId, TIdType}"/>
    /// </summary>
    protected EntityBase() {}

    /// <summary>
    /// Constructor of <see cref="EntityBase{TId, TIdType}"/>
    /// </summary>
    /// <param name="id">Identificator</param>
    public EntityBase(TId id)
    {
        Id = id;
    }


    /// <inheritdoc />
    public bool Equals(EntityBase<TId, TIdType>? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((EntityBase<TId, TIdType>) obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return EqualityComparer<TId>.Default.GetHashCode(Id!);
    }

    /// <summary>
    /// Equality operator
    /// </summary>
    /// <param name="left">Left operand</param>
    /// <param name="right">Right operand</param>
    /// <returns>Equality</returns>
    public static bool operator ==(EntityBase<TId, TIdType> left, EntityBase<TId, TIdType> right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Inequality operator
    /// </summary>
    /// <param name="left">Left operand</param>
    /// <param name="right">Right operand</param>
    /// <returns>Inequality</returns>
    public static bool operator !=(EntityBase<TId, TIdType> left, EntityBase<TId, TIdType> right)
    {
        return !Equals(left, right);
    }
}