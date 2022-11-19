using Solyanka.Exceptions.Exceptions;

namespace Solyanka.Exceptions;

/// <summary>
/// Handler of concrete and inherited exception type 
/// </summary>
/// <typeparam name="TException">Exception type inherited of <see cref="ControllableException"/></typeparam>
public interface IExceptionHandler<TException> where TException : ControllableException
{
    /// <summary>
    /// Handle exception of concrete of inherited type
    /// </summary>
    /// <param name="exception">Exception</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="ExceptionResult"/></returns>
    public Task<ExceptionResult> Handle(in TException exception, CancellationToken cancellationToken);
}