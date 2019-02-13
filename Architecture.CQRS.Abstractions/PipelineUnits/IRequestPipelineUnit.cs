using System.Threading;
using System.Threading.Tasks;
using Architecture.CQRS.Abstractions.Requests;

namespace Architecture.CQRS.Abstractions.PipelineUnits
{
    public interface IRequestPipelineUnit<in TIn, TOut> where TIn : IRequest<TOut>
    {
        Task<TOut> Handle(TIn request, CancellationToken cancellationToken, RequestHandlerDelegate<TOut> next);
    }
}