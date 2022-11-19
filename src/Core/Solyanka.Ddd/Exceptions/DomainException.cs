using Solyanka.Exceptions.Exceptions;

namespace Solyanka.Ddd.Exceptions;

/// <summary>
/// Exception in domain logic
/// </summary>
[Serializable]
public class DomainException : LogicalException
{
    /// <inheritdoc />
    protected DomainException() {}

    /// <inheritdoc />
    public DomainException(string? message, string? code = null, string? subCode = null) : base(message, code, subCode) {}

    /// <inheritdoc />
    public DomainException(string? message, Exception? inner) : base(message, inner) {}
}