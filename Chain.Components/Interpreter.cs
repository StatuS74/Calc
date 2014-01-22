using System;
using Chain.Interfaces;
using Chain.Interfaces.Exceptions;

namespace Chain.Components
{
	public class Interpreter : IInterpreter
	{
		public Func<string, int> ParameterGetter { get; set; }

		public int Execute(string input)
		{
			if (input == null) throw new ArgumentNullException();
			if (String.IsNullOrEmpty(input)) throw new ArgumentException();
			var lexer = new Lexer();
			var parser = new Parser();
			var evaluator = new Evaluator();
			var lexemes = lexer.Analyze(input);
			var expression = parser.Parse(lexemes);
			try
			{
				return evaluator.Evaluate(expression, ParameterGetter);
			}
			catch (ArithmeticException ex)
			{
				throw new InterpreterException("Арифметическая ошибка", ex);
			}
		}
	}
}
