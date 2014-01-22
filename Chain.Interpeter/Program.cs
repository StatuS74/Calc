using System;
using System.Globalization;
using Chain.Components;
using Chain.Interfaces.Exceptions;

namespace Chain.Interpeter
{
	class Program
	{
		static void Main(string[] args)
		{
			var interpreter = new Chain.Components.Interpreter() { ParameterGetter = GetParameter };
			while (true)
			{
				Console.Write("Введите выражение: ");
				string expression = Console.ReadLine();
				if (String.IsNullOrEmpty(expression)) break;

				try
				{
					int result = interpreter.Execute(expression);
					Console.WriteLine("Результат: " + result);

				}
				catch (LexerException ex)
				{
					Console.WriteLine("Lexer: " + ex.Message);
				}
				catch (ParserException ex)
				{
					Console.WriteLine("Parser: " + ex.Message);
				}
			}
		}


		static int GetParameter(string name)
		{
			while (true)
			{
				Console.Write("Введите параметр " + name + ": ");
				string input = Console.ReadLine();
				int result;
				if (!Int32.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out result) || result <= 0)
				{
					Console.WriteLine("Не верное значение (введенные данные могут быть в деопазоне 1...2147483647 ).");
				}
				else
				{
					return result;
				}
			}
		}
	}
}
