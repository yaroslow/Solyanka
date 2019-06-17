using System.Threading;
using System.Threading.Tasks;
using Solyanka.Dispatcher.Cqrs.Requests;

namespace Solyanka.Dispatcher.Cqrs
{
    /// <summary>
    /// Dispatcher handling requests
    /// </summary>
    public interface IRequestDispatcher
    {
        /// <summary>
        /// Handling
        /// </summary>
        /// <typeparam name="TOut">Output data type</typeparam>
        /// <param name="request">Request</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Task{TOut}"/></returns>
        Task<TOut> ProcessRequest<TOut>(IRequest<TOut> request, CancellationToken cancellationToken = default);
    }
}
