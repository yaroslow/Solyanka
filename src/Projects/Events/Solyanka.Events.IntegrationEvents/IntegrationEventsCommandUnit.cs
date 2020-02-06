using System.Threading;
using System.Threading.Tasks;
using Solyanka.Cqs.Abstractions;
using Solyanka.Cqs.Abstractions.PipelineUnits;
using Solyanka.Cqs.Abstractions.Requests;
using Solyanka.ServiceBus.Abstractions;

namespace Solyanka.Events.IntegrationEvents
{
    /// <summary>
    /// <see cref="ICommandPipelineUnit{TIn,TOut}"/> that publishes events to <see cref="IServiceBus"/>
    /// </summary>
    /// <typeparam name="TIn">Input data type implementing <see cref="ICommand{TOut}"/></typeparam>
    /// <typeparam name="TOut">Output data type</typeparam>
    public class IntegrationEventsCommandUnit<TIn, TOut> : ICommandPipelineUnit<TIn, TOut> where TIn : ICommand<TOut>
    {
        private readonly IServiceBus _serviceBus;


        /// <summary/>
        public IntegrationEventsCommandUnit(IServiceBus serviceBus)
        {
            _serviceBus = serviceBus;
        }

        
        /// <inheritdoc />
        public async Task<TOut> Handle(TIn request, CancellationToken cancellationToken, RequestHandlerDelegate<TOut> next)
        {
            var result = await next();

            await _serviceBus.PublishAsync(cancellationToken);
            
            return result;
        }
    }
}