using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Architecture.CQRS.Abstractions;
using Architecture.CQRS.Abstractions.Handlers;
using Architecture.CQRS.Abstractions.Pipelines;
using Architecture.CQRS.Abstractions.PipelineUnits;
using Architecture.CQRS.Abstractions.Requests;

namespace Architecture.Internal
{
    internal class QueryPipeline<TIn, TOut> : HandlerFactory, IQueryPipeline<TIn, TOut> where TIn : IQuery<TOut>
    {
        public Task<TOut> ProcessPipeline(IRequest<TOut> request, CancellationToken cancellationToken, ServiceFactory serviceFactory)
        {
            Task<TOut> Handler() => GetHandler<IQueryHandler<TIn, TOut>>(serviceFactory).Handle((TIn) request, cancellationToken);
            var handler = (RequestHandlerDelegate<TOut>) Handler;

            return serviceFactory
                .GetServices<IQueryPipelineUnit<TIn, TOut>>()
                .Reverse()
                .Aggregate(handler, (next, pipelineUnit) => () => pipelineUnit.Handle((TIn) request, cancellationToken, next))();
        }
    }
}
