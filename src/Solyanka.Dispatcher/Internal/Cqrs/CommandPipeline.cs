using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Solyanka.Dispatcher.Cqrs;
using Solyanka.Dispatcher.Cqrs.Handlers;
using Solyanka.Dispatcher.Cqrs.Pipelines;
using Solyanka.Dispatcher.Cqrs.PipelineUnits;
using Solyanka.Dispatcher.Cqrs.Requests;
using Solyanka.Utils;

namespace Solyanka.Dispatcher.Internal.Cqrs
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
