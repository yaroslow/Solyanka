using System.Linq.Expressions;
using Solyanka.Expressions.Utils;

namespace Solyanka.Expressions;

/// <summary>
/// Class-extensions over <see cref="Expression"/>
/// </summary>
public static class ExpressionExtensions
{
    /// <summary>
    /// Compile and cache expression
    /// </summary>
    /// <param name="expression"><see cref="Expression"/></param>
    /// <typeparam name="TIn">Expression input data type</typeparam>
    /// <typeparam name="TOut">Expression output data type</typeparam>
    /// <returns><see cref="Func{TIn, TOut}"/></returns>
    public static Func<TIn, TOut> AsFunc<TIn, TOut>(this Expression<Func<TIn, TOut>> expression) => 
        ExpressionCache<TIn, TOut>.AsFunc(expression);
        
    /// <summary>
    /// Combines the first expression with the second
    /// </summary>
    /// <param name="left">Expression that accepts <c>TIn</c> and return <c>TInter</c></param>
    /// <param name="right">Expression that accepts <c>TInter</c> and return <c>TOut</c></param>
    /// <typeparam name="TIn">Input type</typeparam>
    /// <typeparam name="TInter">Intermediate type</typeparam>
    /// <typeparam name="TOut">Output type</typeparam>
    /// <returns>Expression that accepts <c>TIn</c> and return <c>TOut</c></returns>
    public static Expression<Func<TIn, TOut>> Compose<TIn, TInter, TOut>(this Expression<Func<TIn, TInter>> left, 
        Expression<Func<TInter, TOut>> right)
    {
        var merged = new ExpressionParameterReplacer(right.Parameters[0], left.Body).Visit(right.Body);
        return Expression.Lambda<Func<TIn, TOut>>(merged, left.Parameters[0]);
    }
        
    /// <summary>
    /// Combines the first expression with the second using the specified merge function.
    /// </summary>
    public static Expression<T> Compose<T>(this LambdaExpression first, LambdaExpression second,
        Func<Expression, Expression, Expression> merge)
    {
        var map = first.Parameters
            .Select((f, i) => new { f, s = second.Parameters[i] })
            .ToDictionary(p => p.s, p => p.f);

        var secondBody = new ExpressionParameterRebinder(map).Visit(second.Body);
        return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
    }
        
    /// <summary>
    /// Combines the first predicate with the second using the logical "and".
    /// </summary>
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
    {
        return first.Compose<Func<T, bool>>(second, Expression.AndAlso);
    }

    /// <summary>
    /// Combines the first predicate with the second using the logical "or".
    /// </summary>
    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
    {
        return first.Compose<Func<T, bool>>(second, Expression.OrElse);
    }

    /// <summary>
    /// Negates the predicate.
    /// </summary>
    public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
    {
        var negated = Expression.Not(expression.Body);
        return Expression.Lambda<Func<T, bool>>(negated, expression.Parameters);
    }
}