using System.Threading;
using System.Threading.Tasks;
using Architecture.CQRS.Abstractions.Requests;

namespace Architecture.CQRS.Abstractions.Handlers
{
    public interface IRequestHandler<in TIn, TOut> where TIn : IRequest<TOut>
    {
        Task<TOut> Handle(TIn request, CancellationToken cancellationToken);
    }
}
