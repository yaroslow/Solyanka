using Solyanka.Cqrs;
using Solyanka.Cqrs.CrossCuttingConcerns;
using Solyanka.Cqrs.Handlers;
using Solyanka.Cqrs.Pipelines;
using Solyanka.Cqrs.Requests;
using Solyanka.Utils;

namespace Solyanka.Dispatcher.Pipelines;

internal class CommandPipeline<TIn, TOut> : HandlerFactory, ICommandPipeline<TIn, TOut> where TIn : ICommand<TOut>
{
    public Task<TOut> Process(IRequest<TOut> request, ServiceFactory serviceFactory, CancellationToken cancellationToken)
    {
        Task<TOut> Handler() => GetHandler<ICommandHandler<TIn, TOut>>(serviceFactory).Handle((TIn) request, cancellationToken);
        var handler = (RequestHandlerDelegate<TOut>) Handler;

        return serviceFactory
            .GetServices<ICommandCrossCuttingConcern<TIn, TOut>>()
            .Reverse()
            .Aggregate(handler, (next, pipelineUnit) => 
                () => pipelineUnit.Concern((TIn) request, next, cancellationToken))();
    }
}