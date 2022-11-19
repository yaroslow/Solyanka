using Solyanka.Exceptions.Exceptions;
using Solyanka.HttpWrapper.Abstractions;

namespace Solyanka.HttpWrapper.PolicyFactories;

/// <inheritdoc />
public class DefaultExceptionPolicyFactory : IExceptionPolicyFactory
{
    /// <summary>
    /// Default exception handler if not specified
    /// </summary>
    /// <exception cref="ControllableException"></exception>
    public static Func<Exception, Task> DefaultHandler =>
        exception => throw new ControllableException(exception.Message, exception);

        
    /// <summary>
    /// Exception handler
    /// </summary>
    public Func<Exception, Task> Handler { get; }

        
    /// <summary>
    /// Конструктор <see cref="DefaultExceptionPolicyFactory"/>
    /// </summary>
    /// <param name="handler">Exception handler</param>
    public DefaultExceptionPolicyFactory(Func<Exception, Task>? handler = null)
    {
        Handler = handler ?? DefaultHandler;
    }

        
    /// <inheritdoc />
    public Func<Exception, Task> GetExceptionPolicy()
    {
        return Handler;
    }


    /// <summary>
    /// Default <see cref="DefaultExceptionPolicyFactory"/>
    /// </summary>
    public static DefaultExceptionPolicyFactory Default = new();
}