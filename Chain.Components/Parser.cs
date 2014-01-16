using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Chain.Interfaces;
using Chain.Interfaces.Exceptions;

namespace Chain.Components
{
	public class Parser : IParser
	{
		public Expression Parse(IList<ILexeme> lexemes)
		{
			enumerator = lexemes.GetEnumerator();
			MoveNext();

			Expression expression = ParseExpression();
			if (token.Type == LexemeTypeEnd)
			{
				return expression;
			}
			else
			{
				throw new ParserException("Конец ожидаемого выражения");
			}
		}

		private Expression ParseExpression()
		{
			return ParseExpressionAdditive();
		}
		private Expression ParseExpressionAdditive()
		{
			Expression left = ParseExpressionMultiplicative();
			while (true)
			{
				switch (token.Type)
				{
					case LexemeType.OperatorAdd:
						{
							MoveNext();
							var right = ParseExpressionMultiplicative();
                            if (right == null) throw new ParserException("Непредвиденный конец выражения");
							left = Expression.Add(left, right);
						}
						break;
					case LexemeType.OperatorSub:
						{
							MoveNext();
							var right = ParseExpressionMultiplicative();
                            if (right == null) throw new ParserException("Непредвиденный конец выражения");
							left = Expression.Subtract(left, right);
						}
						break;
					default:
						return left;
				}
			}
		}
		private Expression ParseExpressionMultiplicative()
		{
			Expression left = ParseExpressionPrimary();
			while (true)
			{
				switch (token.Type)
				{
					case LexemeType.OperatorMul:
						{
							MoveNext();
							var right = ParseExpressionPrimary();
                            if (right == null) throw new ParserException("Непредвиденный конец выраженияn");
							left = Expression.Multiply(left, right);
						}
						break;
					case LexemeType.OperatorDiv:
						{
							MoveNext();
							var right = ParseExpressionPrimary();
                            if (right == null) throw new ParserException("Непредвиденный конец выражения");
							left = Expression.Divide(left, right);
						}
						break;
					default:
						return left;
				}
			}
		}
		private Expression ParseExpressionPrimary()
		{
			switch (token.Type)
			{
				case LexemeTypeEnd:
					return null;
				case LexemeType.IntegerLiteral:
					int integerliteral = token.Value.Value;
					MoveNext();
					return Expression.Constant(integerliteral, typeof(int));
				case LexemeType.Parameter:
					string parameter = token.Name;
					MoveNext();
					return Expression.Parameter(typeof(int), parameter);
				case LexemeType.ParenthesesLeft:
					MoveNext();
					Expression e = ParseExpression();
					if (token.Type != LexemeType.ParenthesesRight)
					{
						throw new ParserException("Правая скобка ожидается");
					}
					MoveNext();
					return e;
				default:
					throw new ParserException("Ожидается первичное  выражение");
			}
		}

		private void MoveNext()
		{
			if (token == LexemeEnd)
			{
				throw new InvalidOperationException("Не удается переместить предыдущую лексему");
			}
			token = enumerator.MoveNext() ? enumerator.Current : LexemeEnd;
		}

		private readonly Lexeme LexemeEnd = new Lexeme(LexemeTypeEnd);
		private const LexemeType LexemeTypeEnd = (LexemeType)(-1);

		private IEnumerator<ILexeme> enumerator;
		private ILexeme token;
	}
}
