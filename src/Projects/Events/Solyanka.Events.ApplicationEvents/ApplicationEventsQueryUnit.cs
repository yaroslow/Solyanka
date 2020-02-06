using System.Threading;
using System.Threading.Tasks;
using Solyanka.Cqs.Abstractions;
using Solyanka.Cqs.Abstractions.PipelineUnits;
using Solyanka.Cqs.Abstractions.Requests;
using Solyanka.Events.Abstractions;

namespace Solyanka.Events.ApplicationEvents
{
    /// <summary>
    /// <see cref="IQueryPipelineUnit{TIn,TOut}"/> that handles application events
    /// </summary>
    /// <typeparam name="TIn">Input data type implementing <see cref="IQuery{TOut}"/></typeparam>
    /// <typeparam name="TOut">Output data type</typeparam>
    public sealed class ApplicationEventsQueryUnit<TIn, TOut> : IQueryPipelineUnit<TIn, TOut> where TIn : IQuery<TOut>
    {
        private readonly IEventDispatcher _eventDispatcher;


        /// <summary/>
        public ApplicationEventsQueryUnit(IEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
        }
        
        
        /// <inheritdoc />
        public async Task<TOut> Handle(TIn request, CancellationToken cancellationToken, RequestHandlerDelegate<TOut> next)
        {
            if (request is IEventContainer eventContainer)
            {
                //Prehandling
                await HandleEvents(eventContainer, cancellationToken);

                //Handling
                var result = await next();

                //Posthandling
                await HandleEvents(eventContainer, cancellationToken);

                return result;
            }

            return await next();
        }

        
        private async Task HandleEvents(IEventContainer eventContainer, CancellationToken cancellationToken)
        {
            foreach (var @event in eventContainer.Events)
            {
                await _eventDispatcher.NotifyAsync(@event, cancellationToken);
            }
            eventContainer.ClearEvents();
        }
    }
}