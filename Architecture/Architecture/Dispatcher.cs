using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Architecture.CQRS.Abstractions;
using Architecture.CQRS.Abstractions.Pipelines;
using Architecture.CQRS.Abstractions.Requests;

namespace Architecture
{
    public class Dispatcher : IRequestDispatcher
    {
        private readonly ServiceFactory _serviceFactory;
        private static readonly ConcurrentDictionary<Type, object> Handlers = new ConcurrentDictionary<Type, object>();

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

        private static IRequestPipeline<TOut> ConstructPipeline<TOut>(Type requestType, Type handlerType)
        {
            var handler = (IRequestPipeline<TOut>) Handlers.GetOrAdd(requestType,
                t => Activator.CreateInstance(handlerType.MakeGenericType(requestType, typeof(TOut))));

            return handler;
        }
    }
}
