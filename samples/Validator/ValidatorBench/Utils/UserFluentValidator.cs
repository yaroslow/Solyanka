using FluentValidation;
using ValidatorBench.Models;

namespace ValidatorBench.Utils;

/// <summary>
/// Fluent validator of <see cref="User"/>
/// </summary>
public class UserFluentValidator : AbstractValidator<User>
{
    /// <summary>
    /// Ctor
    /// </summary>
    public UserFluentValidator()
    {
        RuleFor(a => a.Login).NotEmpty().MaximumLength(60);
        RuleFor(a => a.Password).NotEmpty().MinimumLength(12);
        RuleFor(a => a.Name).NotEmpty().MaximumLength(60);
        RuleFor(a => a.Surname).NotEmpty().MaximumLength(60);
        RuleFor(a => a.Patronymic).NotEmpty().MaximumLength(60);
        RuleFor(a => a.Age).GreaterThan(10).LessThan(40);
    }
}