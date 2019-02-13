using System.Threading;
using System.Threading.Tasks;
using Architecture.CQRS.Abstractions.Requests;
using Architecture.Utils;

namespace Architecture.CQRS.Abstractions.Pipelines
{
    public interface IRequestPipeline<TOut>
    {
        Task<TOut> ProcessPipeline(IRequest<TOut> request, CancellationToken cancellationToken, ServiceFactory serviceFactory);
    }
}
