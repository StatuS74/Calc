using System;
using System.Collections.Generic;
using System.Globalization;
using Chain.Interfaces;
using Chain.Interfaces.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chain.Tests
{
	[TestClass]
	public class UnitTestLexer
	{
		[TestInitialize]
		public void Lexer_TestInitialize()
		{
			TestTarget context = new TestTarget();
			lexer = context.Lexer;
		}



		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Lexer_NullArgument()
		{
			lexer.Analyze(null);
		}


		[TestMethod]
		public void Lexer_EmptyArgument()
		{
			var lexemes = lexer.Analyze(String.Empty);
			Assert.IsNotNull(lexemes);
			Assert.IsTrue(lexemes.Count == 0);
		}

		[TestMethod]
		public void Lexer_AdditiveOperations()
		{
			var lexemes = lexer.Analyze("+-");
			Assert.IsNotNull(lexemes);
			Assert.IsTrue(lexemes.Count == 2);
			Assert.IsTrue(lexemes[0].Type == LexemeType.OperatorAdd);
			Assert.IsTrue(lexemes[1].Type == LexemeType.OperatorSub);
		}

		[TestMethod]
		public void Lexer_MultiplicativeOperations()
		{
			var lexemes = lexer.Analyze("*/");
			Assert.IsNotNull(lexemes);
			Assert.IsTrue(lexemes.Count == 2);
			Assert.IsTrue(lexemes[0].Type == LexemeType.OperatorMul);
			Assert.IsTrue(lexemes[1].Type == LexemeType.OperatorDiv);
		}

		[TestMethod]
		public void Lexer_ValidParantheses()
		{
			var lexemes = lexer.Analyze("()");
			Assert.IsNotNull(lexemes);
			Assert.IsTrue(lexemes.Count == 2);
			Assert.IsTrue(lexemes[0].Type == LexemeType.ParenthesesLeft);
			Assert.IsTrue(lexemes[1].Type == LexemeType.ParenthesesRight);
		}

		

		[TestMethod]
		public void Lexer_IntegerLiteralLexemeMin()
		{
			var lexemes = lexer.Analyze("1");
			Assert.IsNotNull(lexemes);
			Assert.IsTrue(lexemes.Count == 1);
			Assert.IsTrue(lexemes[0].Type == LexemeType.IntegerLiteral);
			Assert.IsTrue(lexemes[0].Value == 1);
		}

		[TestMethod]
		public void Lexer_IntegerLiteralLexemeMax()
		{
			var lexemes = lexer.Analyze(Int32.MaxValue.ToString(CultureInfo.InvariantCulture));
			Assert.IsNotNull(lexemes);
			Assert.IsTrue(lexemes.Count == 1);
			Assert.IsTrue(lexemes[0].Type == LexemeType.IntegerLiteral);
			Assert.IsTrue(lexemes[0].Value == Int32.MaxValue);
		}

		[TestMethod]
		[ExpectedException(typeof(LexerException))]
		public void Lexer_InvalidIntegerLiteralLexemeStartingFromZero()
		{
			var lexemes = lexer.Analyze("078");
		}

		[TestMethod]
		[ExpectedException(typeof(LexerException))]
		public void Lexer_InvalidIntegerLiteralLexemeZero()
		{
			var lexemes = lexer.Analyze("0");
		}

		[TestMethod]
		[ExpectedException(typeof(LexerException))]
		public void Lexer_InvalidIntegerLiteralLexemeTooLarge()
		{
			long max = (long)Int32.MaxValue + 1;
			var lexemes = lexer.Analyze(max.ToString(CultureInfo.InvariantCulture));
		}

		[TestMethod]
		public void Lexer_InvalidCharacter()
		{
			List<char> valids = new List<char>();
			valids.AddRange(new char[] { '+', '-', '*', '/', '(', ')' });
			for (char c = 'A'; c <= 'Z'; c++) valids.Add(c);
			for (char c = '0'; c <= '9'; c++) valids.Add(c);

			for (int i = 0; i < 255; i++)
			{
				char c = Convert.ToChar(i);
				if (valids.Contains(c)) continue;
				string s = Convert.ToString(c);
				try
				{
					lexer.Analyze(s);
					Assert.Fail();
				}
				catch (LexerException)
				{
				}
			}
		}

		private ILexer lexer;
	}
}
