using System;

namespace Solyanka.Validator.Extensions;

/// <summary>
/// Class-extensions over <see cref="int"/> validation rules
/// </summary>
public static class IntValidationExtensions
{
    /// <summary>
    /// Checks that value is less than other
    /// </summary>
    /// <param name="value">One value</param>
    /// <param name="other">Another value</param>
    /// <returns>Check result</returns>
    public static bool IsLessThan(this int value, int other)
    {
        return value < other;
    }

    /// <summary>
    /// Checks that value is less than other or equal
    /// </summary>
    /// <param name="value">One value</param>
    /// <param name="other">Another value</param>
    /// <returns>Check result</returns>
    public static bool IsLessThanOrEqual(this int value, int other)
    {
        return value <= other;
    }

    /// <summary>
    /// Checks that value is greater than other
    /// </summary>
    /// <param name="value">One value</param>
    /// <param name="other">Another value</param>
    /// <returns>Check result</returns>
    public static bool IsGreaterThan(this int value, int other)
    {
        return value > other;
    }

    /// <summary>
    /// Checks that value is greater than other or equal
    /// </summary>
    /// <param name="value">One value</param>
    /// <param name="other">Another value</param>
    /// <returns>Check result</returns>
    public static bool IsGreaterThanOrEqual(this int value, int other)
    {
        return value >= other;
    }
    
    /// <summary>
    /// Check that value is less (inclusive) or greater (exclusive) than set values
    /// </summary>
    /// <param name="value">Value</param>
    /// <param name="lessValue">Less value (inclusive)</param>
    /// <param name="greaterValue">Greater value (exclusive)</param>
    /// <returns>Check result</returns>
    /// <exception cref="InvalidOperationException">Less or greater values incompatible</exception>
    public static bool IsBetween(this int value, int lessValue, int greaterValue)
    {
        if (lessValue < 0)
        {
            throw new InvalidOperationException($"Less value: {lessValue} is less that 0");
        }
        if (greaterValue < 0)
        {
            throw new InvalidOperationException($"Greater value: {lessValue} is less that 0");
        }
        if (greaterValue <= lessValue)
        {
            throw new InvalidOperationException($"Greater value: {greaterValue} is less or equal than: {lessValue}");
        }

        return value >= lessValue && value < greaterValue;
    }
}