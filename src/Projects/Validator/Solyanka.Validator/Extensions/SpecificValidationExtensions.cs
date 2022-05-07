using System;
using System.ComponentModel.DataAnnotations;

namespace Solyanka.Validator.Extensions;

/// <summary>
/// Class-extensions over specific data validation rules
/// </summary>
public static class SpecificValidationExtensions
{
    /// <summary>
    /// Checks that string is a valid email address
    /// </summary>
    /// <param name="email">Email</param>
    /// <returns>Check result</returns>
    public static bool IsEmail(this string email)
    {
        return new EmailAddressAttribute().IsValid(email);
    }

    /// <summary>
    /// Checks that guid is not empty
    /// </summary>
    /// <param name="guid">Guid</param>
    /// <returns>Check result</returns>
    public static bool IsNotEmptyGuid(this Guid guid)
    {
        return guid != Guid.Empty;
    }
    
    /// <summary>
    /// Checks that value is defined in enum
    /// </summary>
    /// <param name="value">Value</param>
    /// <typeparam name="TEnum">Enum type</typeparam>
    /// <typeparam name="TValue">Value type</typeparam>
    /// <returns>Check result</returns>
    public static bool IsEnumValueDefined<TEnum, TValue>(this TValue value) 
        where TEnum : Enum 
        where TValue : struct
    {
        var enumType = typeof(TEnum);
        return Enum.IsDefined(enumType, value);
    }

    /// <summary>
    /// Checks that name is defined in enum
    /// </summary>
    /// <param name="name">Name</param>
    /// <param name="caseSensitive">Case sensitive</param>
    /// <typeparam name="TEnum">Enum type</typeparam>
    /// <returns>Check result</returns>
    public static bool IsEnumNameDefined<TEnum>(this string name, bool caseSensitive = false)
    {
        var enumType = typeof(TEnum);
        return Enum.TryParse(enumType, name, caseSensitive, out _);
    }
}