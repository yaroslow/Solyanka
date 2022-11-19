namespace Solyanka.Ddd.Entity;

/// <summary>
/// Value-object
/// </summary>
public abstract class ValueObjectBase : IEquatable<ValueObjectBase>
{
    /// <summary>
    /// Get components of value-object for equality comparing
    /// </summary>
    /// <returns>Value-object components</returns>
    protected abstract IEnumerable<object> GetEqualityComponents();

    
    /// <inheritdoc />
    public bool Equals(ValueObjectBase? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        if (GetType() != other.GetType()) return false;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ValueObjectBase) obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return CombineHashCodes(GetEqualityComponents());
    }

    /// <summary>
    /// Equality operator
    /// </summary>
    /// <param name="left">Left operand</param>
    /// <param name="right">Right operand</param>
    /// <returns>Equality</returns>
    public static bool operator ==(ValueObjectBase left, ValueObjectBase right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Inequality operator
    /// </summary>
    /// <param name="left">Left operand</param>
    /// <param name="right">Right operand</param>
    /// <returns>Inequality</returns>
    public static bool operator !=(ValueObjectBase left, ValueObjectBase right)
    {
        return !Equals(left, right);
    }

    
    private static int CombineHashCodes(IEnumerable<object> collection)
    {
        unchecked
        {
            return collection.Aggregate(9859, (current, item) => current + HashCode.Combine(item, current));
        }
    }
}