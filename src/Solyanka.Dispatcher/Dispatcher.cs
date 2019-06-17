using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Solyanka.Dispatcher.Cqrs;
using Solyanka.Dispatcher.Cqrs.Pipelines;
using Solyanka.Dispatcher.Cqrs.Requests;
using Solyanka.Dispatcher.Events;
using Solyanka.Dispatcher.Internal.Cqrs;
using Solyanka.Dispatcher.Internal.Events;
using Solyanka.Utils;

namespace Solyanka.Dispatcher
{
    /// <summary>
    /// Dispatcher handling requests and notifications
    /// </summary>
    public class Dispatcher : IRequestDispatcher, INotificationDispatcher
    {
        private readonly ServiceFactory _serviceFactory;
        private static readonly ConcurrentDictionary<Type, object> Handlers = new ConcurrentDictionary<Type, object>();
        private static readonly ConcurrentDictionary<Type, INotifier> Notificators = new ConcurrentDictionary<Type, INotifier>();

        /// <summary/>
        /// <param name="serviceFactory"><see cref="ServiceFactory"/></param>
        public Dispatcher(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        /// <inheritdoc />
        public Task<TOut> ProcessRequest<TOut>(IRequest<TOut> request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            IRequestPipeline<TOut> handler;

            var requestType = request.GetType();

            switch (request)
            {
                case IQuery<TOut> _:
                {
                    handler = ConstructPipeline<TOut>(requestType, typeof(QueryPipeline<,>));
                    break;
                }
                case ICommand<TOut> _:
                {
                    handler = ConstructPipeline<TOut>(requestType, typeof(CommandPipeline<,>));
                    break;
                }
                default:
                    throw new InvalidCastException(
                        $"It is impossible to correlate type of {nameof(request)} with type {typeof(IQuery<TOut>)} or {typeof(ICommand<TOut>)}");
            }

            return handler.ProcessPipeline(request, cancellationToken, _serviceFactory);
        }

        /// <inheritdoc />
        public Task Notify<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            if (notification == null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            var notificationType = notification.GetType();

            var notificator = Notificators.GetOrAdd(notificationType,
                t => (INotifier) Activator.CreateInstance(typeof(Notifier<>).MakeGenericType(notificationType)));

            return notificator.Notify(notification, cancellationToken, _serviceFactory, notifiers =>
                {
                    return Task.WhenAll(notifiers.Select(a => a.Invoke()));
                });
        }

        private static IRequestPipeline<TOut> ConstructPipeline<TOut>(Type requestType, Type handlerType)
        {
            var handler = (IRequestPipeline<TOut>) Handlers.GetOrAdd(requestType,
                t => Activator.CreateInstance(handlerType.MakeGenericType(requestType, typeof(TOut))));

            return handler;
        }
    }
}
