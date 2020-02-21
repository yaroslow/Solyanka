using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Solyanka.Cqs.Abstractions;
using Solyanka.Cqs.Abstractions.Pipelines;
using Solyanka.Cqs.Abstractions.Requests;
using Solyanka.Dispatcher.Cqs;
using Solyanka.Dispatcher.Events;
using Solyanka.Events.Abstractions;
using Solyanka.Utils;

namespace Solyanka.Dispatcher
{
    /// <summary>
    /// Dispatcher handling requests and events
    /// </summary>
    public class Dispatcher : IRequestDispatcher, IEventDispatcher
    {
        private readonly ServiceFactory _serviceFactory;
        private static readonly ConcurrentDictionary<Type, object> Handlers = new ConcurrentDictionary<Type, object>();
        private static readonly ConcurrentDictionary<Type, INotifier> Notificators = new ConcurrentDictionary<Type, INotifier>();

        
        /// <summary/>
        public Dispatcher(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        
        /// <inheritdoc />
        public async Task<TOut> ProcessRequest<TOut>(IRequest<TOut> request, CancellationToken cancellationToken = default)
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

            return await handler.ProcessPipeline(request, cancellationToken, _serviceFactory);
        }

        /// <inheritdoc />
        public async Task Notify<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent
        {
            if (@event == null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            var eventType = @event.GetType();

            var notificator = Notificators.GetOrAdd(eventType,
                t => (INotifier) Activator.CreateInstance(typeof(Notifier<>).MakeGenericType(eventType)));

            await notificator.Notify(@event, cancellationToken, _serviceFactory, async notifiers =>
            {
                await Task.WhenAll(notifiers.Select(async a => await a.Invoke()));
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
