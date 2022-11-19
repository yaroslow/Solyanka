using System.Linq.Expressions;

namespace Solyanka.Expressions.Utils;

/// <summary>
/// <see cref="ExpressionVisitor"/> that rebinds parameters and build pipeline
/// </summary>
public class ExpressionParameterRebinder : ExpressionVisitor
{
    private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

        
    /// <summary/>
    public ExpressionParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
    {
        _map = map;
    }
        

    /// <inheritdoc />
    protected override Expression VisitParameter(ParameterExpression p)
    {
        if (_map.TryGetValue(p, out var replacement))
        {
            p = replacement;
        }

        return base.VisitParameter(p);
    }
}