using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solyanka.Cqrs.Requests;

namespace Solyanka.Cqrs
{
    /// <summary>
    /// Store of <see cref="IEvent"/>
    /// </summary>
    public interface IEventStore
    {
        /// <summary>
        /// Collection of events
        /// </summary>
        ICollection<IEvent> Events { get; }

        /// <summary>
        /// Raise all <see cref="IEvent"/> in store
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Task"/></returns>
        Task Raise(CancellationToken cancellationToken = default);
    }
}