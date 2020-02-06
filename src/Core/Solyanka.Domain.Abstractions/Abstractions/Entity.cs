using Solyanka.Domain.Abstractions.Abstractions.FunctionalInterfaces;
using Solyanka.Domain.Abstractions.Abstractions.MarkerInterfaces;

namespace Solyanka.Domain.Abstractions.Abstractions
{
    /// <summary>
    /// Entity with identificator
    /// </summary>
    /// <typeparam name="TIdentificator"></typeparam>
    public abstract class Entity<TIdentificator> : IEntity, IIdentifiable<TIdentificator>
    {
        /// <inheritdoc cref="IIdentifiable{TIdentificator}" />
        public TIdentificator Id { get; protected set; }

        
        /// <summary/>
        protected Entity() {}
        
        /// <summary>
        /// Ctor of <see cref="Entity{TIdentificator}"/>
        /// </summary>
        /// <param name="id">Entity identificator</param>
        protected Entity(TIdentificator id)
        {
            Id = id;
        }
    }
}