using System.Collections.Generic;
using System.Linq.Expressions;

namespace Chain.Components
{
	internal class ParameterVisitor : ExpressionVisitor
	{
		public List<ParameterExpression> GetParameters(Expression expression)
		{
			parameters.Clear();
			Visit(expression);
			return parameters;
		}


		protected override Expression VisitParameter(ParameterExpression node)
		{
			parameters.Add(node);
			return base.VisitParameter(node);
		}
		private readonly List<ParameterExpression> parameters = new List<ParameterExpression>();
	}
}
