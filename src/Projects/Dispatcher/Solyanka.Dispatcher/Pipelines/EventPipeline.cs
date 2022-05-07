using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Solyanka.Cqrs.Handlers;
using Solyanka.Cqrs.Pipelines;
using Solyanka.Cqrs.Requests;
using Solyanka.Utils;

namespace Solyanka.Dispatcher.Pipelines
{
    internal class EventPipeline<TEvent>: IEventPipeline<TEvent> where TEvent : IEvent
    {
        public async Task<VoidResult> Process(IRequest<VoidResult> request, ServiceFactory serviceFactory, CancellationToken cancellationToken)
        {
            var handlers = serviceFactory.GetServices<IEventHandler<TEvent>>()
                .Select(a => new Func<Task>(() => a.Handle((TEvent) request, cancellationToken)))
                .ToList();

            await Task.WhenAll(handlers.Select(async a => await a.Invoke()));
            return VoidResult.Value;
        }
    }
}
