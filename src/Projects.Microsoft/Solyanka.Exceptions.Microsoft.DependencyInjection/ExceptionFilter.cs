using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Solyanka.Exceptions.Exceptions;

namespace Solyanka.Exceptions.Microsoft.DependencyInjection
{
    /// <inheritdoc />
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        private readonly IServiceProvider _serviceProvider;

        
        /// <summary>
        /// Constructor of <see cref="ExceptionFilter"/>
        /// </summary>
        /// <param name="serviceProvider"><see cref="IServiceProvider"/></param>
        public ExceptionFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        
        /// <inheritdoc />
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var tokenSource = _serviceProvider.GetService<CancellationTokenSource>();
            var token = tokenSource?.Token ?? CancellationToken.None;
            ExceptionResult result;
            
            var exceptionType = context.Exception.GetType();
            if (exceptionType.IsAssignableFrom(typeof(ControllableException)))
            {
                var handlers = _serviceProvider.GetServices<ExceptionHandler>().ToList();
                var handler = FindNearest(exceptionType, handlers);
                if (handler != null)
                {
                    result = await handler.Handle((ControllableException)context.Exception, token);
                }
                else
                {
                    result = await HandleDefaultControllableException((ControllableException)context.Exception, token);
                }
            }
            else
            {
                result = await HandleDefaultUncontrollableException(context.Exception, token);
            }

            context.HttpContext.Response.StatusCode = result.StatusCode;
            context.Result = new JsonResult(result);
            context.ExceptionHandled = true;
        }

        /// <summary>
        /// Default handler for controllable exception
        /// </summary>
        /// <param name="exception">Controllable exception</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="ExceptionResult"/></returns>
        protected virtual Task<ExceptionResult> HandleDefaultControllableException(ControllableException exception, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = new ExceptionResult(exception.Message, 400);
            
            if (!string.IsNullOrEmpty(exception.Code))
            {
                result.Data.Add(nameof(exception.Code), exception.Code);
            }
            if (!string.IsNullOrEmpty(exception.SubCode))
            {
                result.Data.Add(nameof(exception.SubCode), exception.SubCode);
            }

            return Task.FromResult(result);
        }

        /// <summary>
        /// Default handler for uncontrollable exception
        /// </summary>
        /// <param name="exception">Uncontrollable exception</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="ExceptionResult"/></returns>
        protected virtual Task<ExceptionResult> HandleDefaultUncontrollableException(Exception exception, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = new ExceptionResult(exception.Message, 500);

            return Task.FromResult(result);
        }
        
        
        private static ExceptionHandler FindNearest(Type exceptionType, List<ExceptionHandler> handlers)
        {
            var handler = handlers.FirstOrDefault(a => a.ExceptionType == exceptionType);
            if (handler != null)
            {
                return handler;
            }

            if (exceptionType == typeof(ControllableException))
            {
                return null;
            }
                
            var parentType = exceptionType.BaseType;
            return FindNearest(parentType, handlers);
        }
    }
}