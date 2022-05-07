using Solyanka.Validator;
using ValidatorBench.Models;

namespace ValidatorBench.Utils;

/// <summary>
/// Solyanka validator of <see cref="User"/>
/// </summary>
public class UserSolyankaValidator
{
    private readonly Validator<User> _validator;
    
    /// <summary>
    /// Ctor
    /// </summary>
    public UserSolyankaValidator()
    {
        _validator = Solyanka.Validator.Validator.Create<User>()
            .Constrain(a => string.IsNullOrEmpty(a.Login), null)
            .Constrain(a => string.IsNullOrEmpty(a.Password), null)
            .Constrain(a => string.IsNullOrEmpty(a.Name), null)
            .Constrain(a => string.IsNullOrEmpty(a.Surname), null)
            .Constrain(a => string.IsNullOrEmpty(a.Patronymic), null)
            .Constrain(a => a.Login.Length < 60, null)
            .Constrain(a => a.Password.Length > 12, null)
            .Constrain(a => a.Name.Length < 60, null)
            .Constrain(a => a.Surname.Length < 60, null)
            .Constrain(a => a.Patronymic.Length < 60, null)
            .Constrain(a => a.Age > 14 && a.Age < 40, null);
    }

    /// <summary>
    /// Get validator
    /// </summary>
    public Validator<User> Validator => _validator;
}