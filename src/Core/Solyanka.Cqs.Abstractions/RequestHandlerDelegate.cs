using System.Threading.Tasks;

namespace Solyanka.Cqs.Abstractions
{
    /// <summary>
    /// Delegate wrapping next stage of pipeline
    /// </summary>
    /// <typeparam name="TOut">Output data type</typeparam>
    /// <returns><see cref="Task{TOut}"/></returns>
    public delegate Task<TOut> RequestHandlerDelegate<TOut>();
}
