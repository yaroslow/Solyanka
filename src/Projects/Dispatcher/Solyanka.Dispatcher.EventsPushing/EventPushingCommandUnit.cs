using System.Threading;
using System.Threading.Tasks;
using Solyanka.Cqs.Abstractions;
using Solyanka.Cqs.Abstractions.PipelineUnits;
using Solyanka.Cqs.Abstractions.Requests;
using Solyanka.Events.Abstractions;

namespace Solyanka.Dispatcher.EventsPushing
{
    /// <summary>
    /// <see cref="ICommandPipelineUnit{TIn,TOut}"/> that handles all events in <see cref="IEventContainer"/>
    /// </summary>
    /// <typeparam name="TIn">Input data type implementing <see cref="ICommand{TOut}"/></typeparam>
    /// <typeparam name="TOut">Output data type</typeparam>
    public class EventPushingCommandUnit<TIn, TOut> : ICommandPipelineUnit<TIn, TOut> where TIn : ICommand<TOut>
    {
        private readonly IEventContainer _eventContainer;


        /// <summary/>
        public EventPushingCommandUnit(IEventContainer eventContainer)
        {
            _eventContainer = eventContainer;
        }

        
        /// <inheritdoc />
        public async Task<TOut> Handle(TIn request, CancellationToken cancellationToken, RequestHandlerDelegate<TOut> next)
        {
            //PreHandling
            await _eventContainer.Handle(cancellationToken);
            
            var result = await next();

            //PostHanding
            await _eventContainer.Handle(cancellationToken);
            
            return result;
        }
    }
}