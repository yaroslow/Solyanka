using Solyanka.Cqrs.Requests;

namespace Solyanka.Cqrs.CrossCuttingConcerns
{
    /// <summary>
    /// Cross cutting concern of <see cref="IQuery{TOut}"/> pipeline
    /// </summary>
    /// <typeparam name="TIn">Input data type implementing <see cref="IQuery{TOut}"/></typeparam>
    /// <typeparam name="TOut">Output data type</typeparam>
    public interface IQueryCrossCuttingConcern<in TIn, TOut> : IRequestCrossCuttingConcern<TIn, TOut> where TIn : IQuery<TOut> {}
}
