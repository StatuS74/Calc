using System;
using System.Linq;
using System.Linq.Expressions;
using Chain.Interfaces;

namespace Chain.Components
{
	public class Evaluator
	{
		public int Evaluate(Expression expression, Func<string, int> parametergetter)
		{
			ParameterVisitor visitor = new ParameterVisitor();
			var parameters = visitor.GetParameters(expression)
				.Select(x => x.Name).OrderBy(x => x).Distinct()
				.ToDictionary(x => x, x => parametergetter(x));
			return EvaluateInternal(expression, x => parameters[x]);
		}

		private int EvaluateInternal(Expression expression, Func<string, int> parametergetter)
		{
			if (expression.NodeType == ExpressionType.Add)
			{
				var e = (BinaryExpression)expression;
				return checked(EvaluateInternal(e.Left, parametergetter) + EvaluateInternal(e.Right, parametergetter));
			}
			if (expression.NodeType == ExpressionType.Divide)
			{
				var e = (BinaryExpression)expression;
				return checked(EvaluateInternal(e.Left, parametergetter) / EvaluateInternal(e.Right, parametergetter));
			}
			if (expression.NodeType == ExpressionType.Multiply)
			{
				var e = (BinaryExpression)expression;
				return checked(EvaluateInternal(e.Left, parametergetter) * EvaluateInternal(e.Right, parametergetter));
			}
			if (expression.NodeType == ExpressionType.Subtract)
			{
				var e = (BinaryExpression)expression;
				return checked(EvaluateInternal(e.Left, parametergetter) - EvaluateInternal(e.Right, parametergetter));
			}
			if (expression.NodeType == ExpressionType.Constant)
			{
				var e = (ConstantExpression)expression;
				return (int)e.Value;
			}
			if (expression.NodeType == ExpressionType.Parameter)
			{
				var e = (ParameterExpression)expression;
				return parametergetter(e.Name);
			}
			throw new InvalidOperationException();
		}
	}
}
