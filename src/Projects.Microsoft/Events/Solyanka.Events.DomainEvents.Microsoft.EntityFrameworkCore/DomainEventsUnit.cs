using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solyanka.Cqs.Abstractions;
using Solyanka.Cqs.Abstractions.PipelineUnits;
using Solyanka.Cqs.Abstractions.Requests;
using Solyanka.Events.Abstractions;

namespace Solyanka.Events.DomainEvents.Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// <see cref="ICommandPipelineUnit{TIn,TOut}"/> that handles domain events
    /// </summary>
    /// <typeparam name="TIn">Input data type implementing <see cref="ICommand"/></typeparam>
    /// <typeparam name="TOut">Output data type</typeparam>
    public class DomainEventsUnit<TIn, TOut> : ICommandPipelineUnit<TIn, TOut> where TIn : ICommand<TOut>
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly DbContext _dbContext;


        /// <summary/>
        public DomainEventsUnit(IEventDispatcher eventDispatcher, DbContext dbContext)
        {
            _eventDispatcher = eventDispatcher;
            _dbContext = dbContext;
        }
        
        
        /// <inheritdoc />
        public async Task<TOut> Handle(TIn request, CancellationToken cancellationToken, RequestHandlerDelegate<TOut> next)
        {
            var result = await next();

            var entities = _dbContext.ChangeTracker
                .Entries<IEventContainer>()
                .Where(a => a.Entity.Events != null && a.Entity.Events.Any())
                .Select(a => a.Entity)
                .ToList();

            var domainEvents = entities
                .SelectMany(a => a.Events)
                .ToList();
            
            entities.ForEach(a => a.ClearEvents());
            
            foreach (var domainEvent in domainEvents)
            {
                await _eventDispatcher.NotifyAsync(domainEvent, cancellationToken);
            }

            return result;
        }
    }
}