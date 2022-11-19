using System.Linq.Expressions;

namespace Solyanka.Expressions.Utils;

/// <summary>
/// <see cref="ExpressionVisitor"/> that replaces parameters and build expression pipeline
/// </summary>
public class ExpressionParameterReplacer : ExpressionVisitor
{
    private readonly ParameterExpression _searched;
    private readonly Expression _replaced;

        
    /// <summary/>
    public ExpressionParameterReplacer(ParameterExpression searched, Expression replaced)
    {
        _searched = searched ?? throw new ArgumentNullException(nameof(searched));
        _replaced = replaced ?? throw new ArgumentNullException(nameof(replaced));
    }


    /// <inheritdoc />
    protected override Expression VisitParameter(ParameterExpression node)
    {
        return node == _searched ? _replaced : base.VisitParameter(node);
    }
}