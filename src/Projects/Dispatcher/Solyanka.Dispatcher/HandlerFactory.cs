using Solyanka.Utils;

namespace Solyanka.Dispatcher;

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
                $"Error in constructing handler {typeof(THandler)}: {exception.Message}",
                exception);
        }

        if (handler == null)
        {
            throw new InvalidOperationException(
                $"Handler {typeof(THandler)} not found. You need to add it to DI container");
        }

        return handler;
    }
}