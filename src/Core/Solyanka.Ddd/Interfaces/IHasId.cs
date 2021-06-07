using System;

namespace Solyanka.Ddd.Interfaces
{
    /// <summary>
    /// Has identificator
    /// </summary>
    public interface IHasId
    {
        /// <summary>
        /// Identificator
        /// </summary>
        object Id { get; }
    }
    
    /// <summary>
    /// Has identificator
    /// </summary>
    /// <typeparam name="TId">Identificator type</typeparam>
    public interface IHasId<out TId> : IHasId where TId : IEquatable<TId>
    {
        /// <summary>
        /// Identificator of specified type
        /// </summary>
        new TId Id { get; }
    }
}