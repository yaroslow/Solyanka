using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Solyanka.Cqrs;
using Solyanka.Cqrs.CrossCuttingConcerns;
using Solyanka.Cqrs.Handlers;
using Solyanka.Cqrs.Pipelines;
using Solyanka.Cqrs.Requests;
using Solyanka.Utils;

namespace Solyanka.Dispatcher.Cqrs
{
    internal class QueryPipeline<TIn, TOut> : HandlerFactory, IQueryPipeline<TIn, TOut> where TIn : IQuery<TOut>
    {
        public Task<TOut> Process(IRequest<TOut> request, ServiceFactory serviceFactory, CancellationToken cancellationToken)
        {
            Task<TOut> Handler() => GetHandler<IQueryHandler<TIn, TOut>>(serviceFactory).Handle((TIn) request, cancellationToken);
            var handler = (RequestHandlerDelegate<TOut>) Handler;

            return serviceFactory
                .GetServices<IQueryCrossCuttingConcern<TIn, TOut>>()
                .Reverse()
                .Aggregate(handler, (next, pipelineUnit) => 
                    () => pipelineUnit.Concern((TIn) request, next, cancellationToken))();
        }
    }
}
