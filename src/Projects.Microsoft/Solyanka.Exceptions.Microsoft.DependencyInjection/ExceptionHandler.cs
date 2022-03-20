using System;
using System.Threading;
using System.Threading.Tasks;
using Solyanka.Exceptions.Exceptions;

namespace Solyanka.Exceptions.Microsoft.DependencyInjection
{
    /// <summary>
    /// Delegate that handles <see cref="ControllableException"/> and derived types
    /// </summary>
    public delegate Task<ExceptionResult> ExceptionHandlerDelegate(in ControllableException exception, CancellationToken cancellationToken);
    
    
    /// <summary>
    /// Wrapper over <see cref="IExceptionHandler{TException}"/>
    /// </summary>
    public class ExceptionHandler
    {
        /// <summary>
        /// Type of exception
        /// </summary>
        public Type ExceptionType { get; }
        
        /// <summary>
        /// <see cref="ExceptionHandlerDelegate"/>
        /// </summary>
        public ExceptionHandlerDelegate Delegate { get; }


        /// <summary>
        /// Constructor of <see cref="ExceptionHandler"/>
        /// </summary>
        /// <param name="exceptionType">Type of controllable exception</param>
        /// <param name="delegate"><see cref="ExceptionHandlerDelegate"/></param>
        public ExceptionHandler(Type exceptionType, ExceptionHandlerDelegate @delegate)
        {
            ExceptionType = exceptionType;
            Delegate = @delegate;
        }


        /// <summary>
        /// Handle catched exception
        /// </summary>
        /// <param name="exception">Catched exception that should be handled</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <typeparam name="TException">Exception type inherited from <see cref="ControllableException"/></typeparam>
        /// <returns><see cref="ExceptionResult"/></returns>
        /// <exception cref="InvalidOperationException">If handler could not handle exception type</exception>
        public async Task<ExceptionResult> Handle<TException>(TException exception, CancellationToken cancellationToken)
            where TException : ControllableException
        {
            if (ExceptionType.IsAssignableFrom(typeof(TException)))
            {
                throw new InvalidOperationException(
                    $"Could not handle exception of type {typeof(TException).FullName} by {ExceptionType.FullName} handler");
            }

            return await Delegate(exception, cancellationToken);
        }
    }
}