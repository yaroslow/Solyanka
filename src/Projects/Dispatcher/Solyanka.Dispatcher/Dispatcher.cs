using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Solyanka.Cqrs;
using Solyanka.Cqrs.Pipelines;
using Solyanka.Cqrs.Requests;
using Solyanka.Dispatcher.Cqrs;
using Solyanka.Utils;

namespace Solyanka.Dispatcher
{
    /// <summary>
    /// Dispatcher handling requests and events
    /// </summary>
    public class Dispatcher : IRequestDispatcher
    {
        private readonly ServiceFactory _serviceFactory;
        private static readonly ConcurrentDictionary<Type, object> Pipelines = new();

        
        /// <summary/>
        public Dispatcher(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }


        /// <inheritdoc />
        public Task<TOut> Handle<TOut>(IRequest<TOut> request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            IRequestPipeline<TOut> handler;

            var requestType = request.GetType();

            switch (request)
            {
                case IQuery<TOut>:
                {
                    handler = ConstructPipeline<TOut>(requestType, typeof(QueryPipeline<,>));
                    break;
                }
                case ICommand<TOut>:
                {
                    handler = ConstructPipeline<TOut>(requestType, typeof(CommandPipeline<,>));
                    break;
                }
                case IEvent:
                {
                    handler = ConstructPipeline<TOut>(requestType, typeof(EventPipeline<>));
                    break;
                }
                default:
                    throw new InvalidCastException(
                        $"It is impossible to correlate type of {nameof(request)} with types " +
                        $"{typeof(IQuery<TOut>)}, {typeof(ICommand<TOut>)} or {typeof(IEvent)}");
            }

            return handler.Process(request, _serviceFactory, cancellationToken);
        }
        
        
        private static IRequestPipeline<TOut> ConstructPipeline<TOut>(Type requestType, Type handlerType)
        {
            return (IRequestPipeline<TOut>) Pipelines.GetOrAdd(requestType,
                t => Activator.CreateInstance(handlerType.MakeGenericType(requestType, typeof(TOut))));
        }
    }
}
