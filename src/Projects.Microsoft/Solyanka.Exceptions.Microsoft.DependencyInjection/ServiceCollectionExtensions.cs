using Microsoft.Extensions.DependencyInjection;
using Solyanka.Exceptions.Exceptions;

namespace Solyanka.Exceptions.Microsoft.DependencyInjection;

/// <summary>
/// Class-extensions over <see cref="IServiceCollection"/> to inject
/// <see cref="IExceptionHandler{TException}"/> for concrete <see cref="ControllableException"/> inheritor
/// to Microsoft DI container
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add exception handler for <c>TException</c> type
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="exceptionHandler"><see cref="IExceptionHandler{TException}"/></param>
    /// <typeparam name="TException">Controllable exception or its inheritor type</typeparam>
    public static void AddExceptionHandler<TException>(this IServiceCollection services, 
        IExceptionHandler<TException> exceptionHandler) where TException : ControllableException
    {
        services.AddScoped(_ =>
        {
            return new ExceptionHandler(typeof(TException),
                (in ControllableException exception, CancellationToken token) =>
                    exceptionHandler.Handle((TException) exception, token));
        });
    }

    /// <summary>
    /// Add <see cref="ExceptionFilter"/>
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    public static void AddExceptionFilter(this IServiceCollection services)
    {
        services.AddScoped<ExceptionFilter>();
    }
}