using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Architecture.CQRS.Abstractions;
using Architecture.CQRS.Abstractions.Handlers;
using Architecture.CQRS.Abstractions.Pipelines;
using Architecture.CQRS.Abstractions.PipelineUnits;
using Architecture.CQRS.Abstractions.Requests;
using Architecture.Utils;

namespace Architecture.Internal
{
    internal class CommandPipeline<TIn, TOut> : HandlerFactory, ICommandPipeline<TIn, TOut> where TIn : ICommand<TOut>
    {
        public Task<TOut> ProcessPipeline(IRequest<TOut> request, CancellationToken cancellationToken, ServiceFactory serviceFactory)
        {
            Task<TOut> Handler() => GetHandler<ICommandHandler<TIn, TOut>>(serviceFactory).Handle((TIn) request, cancellationToken);
            var handler = (RequestHandlerDelegate<TOut>) Handler;

            return serviceFactory
                .GetServices<ICommandPipelineUnit<TIn, TOut>>()
                .Reverse()
                .Aggregate(handler, (next, pipelineUnit) => () => pipelineUnit.Handle((TIn) request, cancellationToken, next))();
        }
    }
}
