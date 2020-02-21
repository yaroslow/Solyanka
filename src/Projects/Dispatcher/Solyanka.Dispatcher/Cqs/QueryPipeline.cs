using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Solyanka.Cqs.Abstractions;
using Solyanka.Cqs.Abstractions.Handlers;
using Solyanka.Cqs.Abstractions.Pipelines;
using Solyanka.Cqs.Abstractions.PipelineUnits;
using Solyanka.Cqs.Abstractions.Requests;
using Solyanka.Utils;

namespace Solyanka.Dispatcher.Cqs
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
