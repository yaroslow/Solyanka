namespace Solyanka.Ddd.Entity;

/// <summary>
/// Strongly identificator
/// </summary>
/// <typeparam name="TId">Identificator type</typeparam>
public abstract class Id<TId>: IEquatable<Id<TId>>, IComparable<Id<TId>>, IComparable 
    where TId : IEquatable<TId>, IComparable<TId>, IComparable 
{
    /// <summary>
    /// Value of identificator
    /// </summary>
    public TId Value { get; }

    
    /// <summary>
    /// Constructor of <see cref="Id{TId}"/>
    /// </summary>
    /// <param name="value">Value of id</param>
    public Id(TId value)
    {
        Value = value;
    }


    /// <inheritdoc />
    public bool Equals(Id<TId>? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value.Equals(other.Value);
    }
    
    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (!(obj.GetType() != GetType())) return false;
        return Equals((Id<TId>) obj);
    }

    /// <summary>
    /// Equality operator
    /// </summary>
    /// <param name="left">Left operand</param>
    /// <param name="right">Right operand</param>
    /// <returns>Equality</returns>
    public static bool operator ==(Id<TId> left, Id<TId> right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Inequality operator
    /// </summary>
    /// <param name="left">Left operand</param>
    /// <param name="right">Right operand</param>
    /// <returns>Inequality</returns>
    public static bool operator !=(Id<TId> left, Id<TId> right)
    {
        return !Equals(left, right);
    }
    
    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    /// <inheritdoc />
    public int CompareTo(Id<TId>? other)
    {
        return Value.CompareTo(other!.Value);
    }

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        if (ReferenceEquals(null, obj)) return 1;
        if (ReferenceEquals(this, obj)) return 0;
        return obj is Id<TId> other
            ? CompareTo(other)
            : throw new ArgumentException($"Object must be of type {nameof(Id<TId>)}");
    }

    /// <inheritdoc />
    public override string? ToString()
    {
        return Value.ToString();
    }
}