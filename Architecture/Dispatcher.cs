using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Architecture.CQRS.Abstractions;
using Architecture.CQRS.Abstractions.Pipelines;
using Architecture.CQRS.Abstractions.Requests;
using Architecture.DE.Abstractions;
using Architecture.Internal;
using Architecture.Utils;

namespace Architecture
{
    public class Dispatcher : IRequestDispatcher, INotificationDispatcher
    {
        private readonly ServiceFactory _serviceFactory;
        private static readonly ConcurrentDictionary<Type, object> Handlers = new ConcurrentDictionary<Type, object>();
        private static readonly ConcurrentDictionary<Type, INotificator> Notificators = new ConcurrentDictionary<Type, INotificator>();

        public Dispatcher(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

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
                    handler = ConstructPipeline<TOut>(requestType, typeof(IQueryPipeline<,>));
                    break;
                }
                case ICommand<TOut> _:
                {
                    handler = ConstructPipeline<TOut>(requestType, typeof(ICommandPipeline<,>));
                    break;
                }
                default:
                    throw new InvalidCastException(
                        $"It is impossible to match {nameof(request)} with types {typeof(IQuery<TOut>)} or {typeof(ICommand<TOut>)}");
            }

            return handler.ProcessPipeline(request, cancellationToken, _serviceFactory);
        }

        public Task Notify<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            if (notification == null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            var notificationType = notification.GetType();

            var notificator = Notificators.GetOrAdd(notificationType,
                t => (INotificator) Activator.CreateInstance(typeof(Notificator<>).MakeGenericType(notificationType)));

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