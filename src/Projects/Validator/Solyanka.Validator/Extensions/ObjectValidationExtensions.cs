namespace Solyanka.Validator.Extensions;

/// <summary>
/// Class-extensions over <see cref="object"/> validation rules
/// </summary>
public static class ObjectValidationExtensions
{
    /// <summary>
    /// Checks that object is null
    /// </summary>
    /// <param name="obj">Object</param>
    /// <returns>Check result</returns>
    public static bool IsNull(this object obj)
    {
        return obj == null;
    }
    
    /// <summary>
    /// Checks that object is not null
    /// </summary>
    /// <param name="obj">Object</param>
    /// <returns>Check result</returns>
    public static bool IsNotNull(this object obj)
    {
        return obj != null;
    }
    
    /// <summary>
    /// Checks that object equals to other
    /// </summary>
    /// <param name="obj">One object</param>
    /// <param name="other">Another object</param>
    /// <returns>Check result</returns>
    public static bool IsEqual(this object obj, object other)
    {
        return obj.Equals(other);
    }
        
    /// <summary>
    /// Checks that object is not equal to other
    /// </summary>
    /// <param name="obj">One object</param>
    /// <param name="other">Another object</param>
    /// <returns>Check result</returns>
    public static bool IsNotEqual(this object obj, object other)
    {
        return !obj.Equals(other);
    }
}