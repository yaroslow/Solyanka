using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Solyanka.Expressions
{
    /// <summary>
    /// Cache for compiled expressions
    /// </summary>
    public class ExpressionCache<TIn, TOut>
    {
        private static readonly ConcurrentDictionary<Expression<Func<TIn, TOut>>, Func<TIn, TOut>> Cache = new();

        internal static Func<TIn, TOut> AsFunc(Expression<Func<TIn, TOut>> expression) => 
            Cache.GetOrAdd(expression, e => e.Compile());
    }
}