using System;
using System.Text.RegularExpressions;

namespace Solyanka.Validator.Extensions;

/// <summary>
/// Class-extensions over <see cref="string"/> validation rules
/// </summary>
public static class StringValidationExtensions
{
    /// <summary>
    /// Checks that string is empty
    /// </summary>
    /// <param name="str">String</param>
    /// <returns>Check result</returns>
    public static bool IsEmpty(this string str)
    {
        return str == string.Empty;
    }

    /// <summary>
    /// Checks that string is not empty
    /// </summary>
    /// <param name="str">String</param>
    /// <returns>Check result</returns>
    public static bool IsNotEmpty(this string str)
    {
        return str != string.Empty;
    }
        
    /// <summary>
    /// Checks that string is null or empty
    /// </summary>
    /// <param name="str">String</param>
    /// <returns>Check result</returns>
    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }
        
    /// <summary>
    /// Checks that string is not null or empty
    /// </summary>
    /// <param name="str">String</param>
    /// <returns>Check result</returns>
    public static bool IsNotNullOrEmpty(this string str)
    {
        return !string.IsNullOrEmpty(str);
    }

    /// <summary>
    /// Checks that string is null or whitespace
    /// </summary>
    /// <param name="str">String</param>
    /// <returns>Check result</returns>
    public static bool IsNullOrWhiteSpace(this string str)
    {
        return string.IsNullOrWhiteSpace(str);
    }
        
    /// <summary>
    /// Checks that string is not null or whitespace
    /// </summary>
    /// <param name="str">String</param>
    /// <returns>Check result</returns>
    public static bool IsNotNullOrWhiteSpace(this string str)
    {
        return !string.IsNullOrWhiteSpace(str);
    }

    /// <summary>
    /// Checks that string equals to other
    /// </summary>
    /// <param name="str">One string</param>
    /// <param name="otherStr">Another string</param>
    /// <param name="comparison"><see cref="StringComparison"/></param>
    /// <returns>Check result</returns>
    public static bool IsEqual(this string str, string otherStr, StringComparison comparison = StringComparison.Ordinal)
    {
        return string.Equals(str, otherStr, comparison);
    }

    /// <summary>
    /// Checks that string is not equal to other
    /// </summary>
    /// <param name="str">One string</param>
    /// <param name="otherStr">Another string</param>
    /// <param name="comparison"><see cref="StringComparison"/></param>
    /// <returns>Check result</returns>
    public static bool IsNotEqual(this string str, string otherStr, StringComparison comparison = StringComparison.Ordinal)
    {
        return !string.Equals(str, otherStr, comparison);
    }

    /// <summary>
    /// Checks that string length match set value
    /// </summary>
    /// <param name="str">String</param>
    /// <param name="length">Length</param>
    /// <returns>Check result</returns>
    /// <exception cref="InvalidOperationException">Length value incompatible</exception>
    public static bool HasLength(this string str, int length)
    {
        if (length < 0)
        {
            throw new InvalidOperationException($"Length value: {length} is less that 0");
        }

        return str.Length == length;
    }
        
    /// <summary>
    /// Checks that string length is less (inclusive) or greater (exclusive) than set values
    /// </summary>
    /// <param name="str">String</param>
    /// <param name="lessValue">Less value (inclusive)</param>
    /// <param name="greaterValue">Greater value (exclusive)</param>
    /// <returns>Check result</returns>
    /// <exception cref="InvalidOperationException">Less or greater values incompatible</exception>
    public static bool HasLengthRange(this string str, int lessValue, int greaterValue)
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

        return str.Length >= lessValue && str.Length < greaterValue;
    }

    /// <summary>
    /// Checks that string length is greater or equal set value (inclusive)
    /// </summary>
    /// <param name="str">String</param>
    /// <param name="minLength">Minimum length</param>
    /// <returns>Check result</returns>
    /// <exception cref="InvalidOperationException">Min length value is incompatible</exception>
    public static bool HasMinLength(this string str, int minLength)
    {
        if (minLength < 0)
        {
            throw new InvalidOperationException($"MinLength value: {minLength} is less than 0");
        }

        return str.Length >= minLength;
    }

    /// <summary>
    /// Checks that string length is less or equal set value (inclusive)
    /// </summary>
    /// <param name="str">String</param>
    /// <param name="maxLength">Maximum length</param>
    /// <returns>Check result</returns>
    /// <exception cref="InvalidOperationException">Max length value is incompatible</exception>
    public static bool HasMaxLength(this string str, int maxLength)
    {
        if (maxLength < 0)
        {
            throw new InvalidOperationException($"MaxLength value: {maxLength} is less than 0");
        }

        return str.Length <= maxLength;
    }

    /// <summary>
    /// Checks that string matches regex pattern
    /// </summary>
    /// <param name="str">String</param>
    /// <param name="regexp">Regex pattern</param>
    /// <param name="options"><see cref="RegexOptions"/></param>
    /// <returns>Check result</returns>
    /// <exception cref="InvalidOperationException">Regex pattern is null or empty</exception>
    public static bool IsMatch(this string str, string regexp, RegexOptions options = RegexOptions.None)
    {
        if (string.IsNullOrEmpty(regexp))
        {
            throw new InvalidOperationException($"Regexp string is null or empty");
        }

        var regex = new Regex(regexp, options);
        return regex.IsMatch(str);
    }

    /// <summary>
    /// Checks that string matches regex
    /// </summary>
    /// <param name="str">String</param>
    /// <param name="regex"><see cref="Regex"/></param>
    /// <returns>Check result</returns>
    /// <exception cref="ArgumentNullException">Regex is null</exception>
    public static bool IsMatch(this string str, Regex regex)
    {
        if (regex == null)
        {
            throw new ArgumentNullException(nameof(regex));
        }

        return regex.IsMatch(str);
    }
}