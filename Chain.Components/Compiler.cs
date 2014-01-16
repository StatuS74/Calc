using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Chain.Interfaces;

namespace Chain.Components
{
	public class Compiler : ICompiler
	{
		public byte[] Compile(string input)
		{
			var lexer = new Lexer();
			var parser = new Parser();
			var lexemes = lexer.Analyze(input);
			var expression = parser.Parse(lexemes);

			ParameterVisitor visitor = new ParameterVisitor();
			var parameters = visitor.GetParameters(expression)
				.Select(x => x.Name).OrderBy(x => x).Distinct()
				.ToList();

			code.Clear();
			foreach (var parameter in parameters)
			{
				code.Add((byte)OpCode.Input);
				code.Add((byte)parameter[0]);
				//Console.WriteLine("input " + parameter);
			}

			CompileInternal(expression);

			code.Add((byte)OpCode.Output);
			//Console.WriteLine("output");

			return code.ToArray();
		}

		private void CompileInternal(Expression expression)
		{
			if (expression.NodeType == ExpressionType.Add)
			{
				var e = expression as BinaryExpression;
				CompileInternal(e.Left);
				CompileInternal(e.Right);
				code.Add((byte)OpCode.Add);
				//Console.WriteLine("add");
				return;
			}
			if (expression.NodeType == ExpressionType.Divide)
			{
				var e = expression as BinaryExpression;
				CompileInternal(e.Left);
				CompileInternal(e.Right);
				code.Add((byte)OpCode.Div);
				//Console.WriteLine("div");
				return;
			}
			if (expression.NodeType == ExpressionType.Multiply)
			{
				var e = expression as BinaryExpression;
				CompileInternal(e.Left);
				CompileInternal(e.Right);
				code.Add((byte)OpCode.Mul);
				//Console.WriteLine("mul");
				return;
			}
			if (expression.NodeType == ExpressionType.Subtract)
			{
				var e = expression as BinaryExpression;
				CompileInternal(e.Left);
				CompileInternal(e.Right);
				code.Add((byte)OpCode.Sub);
				//Console.WriteLine("sub");
				return;
			}
			if (expression.NodeType == ExpressionType.Constant)
			{
				var e = expression as ConstantExpression;
				code.Add((byte)OpCode.Pushc);
				code.AddRange(BitConverter.GetBytes((int)e.Value));
				//Console.WriteLine("pushc {0}", (int)e.Value);
				return;
			}
			if (expression.NodeType == ExpressionType.Parameter)
			{
				var e = expression as ParameterExpression;
				code.Add((byte)OpCode.Pushv);
				code.Add((byte)e.Name[0]);
				//Console.WriteLine("pushv {0}", e.Name);
				return;
			}
			throw new InvalidOperationException();
		}

		private List<byte> code = new List<byte>();
	}
}
