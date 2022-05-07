using System.ComponentModel.DataAnnotations;
using ValidatorBench.Utils;

namespace ValidatorBench.Models;

/// <summary>
/// User
/// </summary>
public class User
{
    /// <summary>
    /// Login
    /// </summary>
    [Required(AllowEmptyStrings = false), MaxLength(60)]
    public string Login { get; }
    
    /// <summary>
    /// Password
    /// </summary>
    [Required(AllowEmptyStrings = false), MinLength(12)]
    public string Password { get; }
    
    /// <summary>
    /// Name
    /// </summary>
    [Required(AllowEmptyStrings = false), MaxLength(60)]
    public string Name { get; }
    
    /// <summary>
    /// Surname
    /// </summary>
    [Required(AllowEmptyStrings = false), MaxLength(60)]
    public string Surname { get; }
    
    /// <summary>
    /// Patronymic
    /// </summary>
    [Required(AllowEmptyStrings = false), MaxLength(60)]
    public string Patronymic { get; }
    
    /// <summary>
    /// Age
    /// </summary>
    [Range(14, 40)]
    public int Age { get; }

    
    /// <summary>
    /// Ctor
    /// </summary>
    public User()
    {
        Login = RandomSequenceGenerator.GenerateRandomString();
        Password = RandomSequenceGenerator.GenerateRandomString();
        Name = RandomSequenceGenerator.GenerateRandomString();
        Surname = RandomSequenceGenerator.GenerateRandomString();
        Patronymic = RandomSequenceGenerator.GenerateRandomString();
        Age = RandomSequenceGenerator.GenerateInt();
    }
}