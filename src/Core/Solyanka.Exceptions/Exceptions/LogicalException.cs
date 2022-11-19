namespace Solyanka.Exceptions.Exceptions;

/// <summary>
/// Logical exception
/// </summary>
[Serializable]
public class LogicalException : ControllableException
{
    /// <inheritdoc />
    protected LogicalException() {}
        
    /// <inheritdoc />
    public LogicalException(string? message, string? code = null, string? subCode = null) : base(message, code, subCode) {}
        
    /// <inheritdoc />
    public LogicalException(string? message, Exception? innerException, string? code = null, string? subCode = null) : 
        base(message, innerException, code, subCode) {}
}