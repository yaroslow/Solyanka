using System.Threading.Tasks;

namespace Architecture.CQRS.Abstractions
{
    public delegate Task<TOut> RequestHandlerDelegate<TOut>();
}
