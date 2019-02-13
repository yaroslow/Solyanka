using System.Threading;
using System.Threading.Tasks;
using Architecture.CQRS.Abstractions.Requests;

namespace Architecture.CQRS.Abstractions
{
    public interface IRequestDispatcher
    {
        Task<TOut> ProcessRequest<TOut>(IRequest<TOut> request, CancellationToken cancellationToken = default);
    }
}
