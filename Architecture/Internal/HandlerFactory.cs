using System;
using Architecture.Utils;

namespace Architecture.Internal
{
    internal abstract class HandlerFactory
    {
        protected static THandler GetHandler<THandler>(ServiceFactory serviceFactory)
        {
            THandler handler;

            try
            {
                handler = serviceFactory.GetService<THandler>();
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException(
                    $"Error constructing handler {typeof(THandler)}. It is necessary to register handler in container",
                    exception);
            }

            if (handler == null)
            {
                throw new InvalidOperationException(
                    $"Handler {typeof(THandler)} not found. It is necessary to register handler in container");
            }

            return handler;
        }
    }
}
