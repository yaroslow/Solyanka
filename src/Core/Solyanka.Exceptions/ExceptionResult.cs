namespace Solyanka.Exceptions;

/// <summary>
/// Result of exception handling
/// </summary>
public class ExceptionResult
{
    /// <summary>
    /// Message to display
    /// </summary>
    public string DisplayedMessage { get; }
        
    /// <summary>
    /// Status code
    /// </summary>
    public int StatusCode { get; }
        
    /// <summary>
    /// Data
    /// </summary>
    public Dictionary<string, string> Data { get; }


    /// <summary>
    /// Constructor of <see cref="ExceptionResult"/>
    /// </summary>
    /// <param name="message">Message to display</param>
    /// <param name="statusCode">Status code</param>
    public ExceptionResult(string message, int statusCode)
    {
        DisplayedMessage = message;
        StatusCode = statusCode;
        Data = new Dictionary<string, string>();
    }
}