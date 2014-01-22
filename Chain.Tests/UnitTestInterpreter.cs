using System;
using System.Collections.Generic;
using System.Globalization;
using Chain.Interfaces;
using Chain.Interfaces.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chain.Tests
{
	[TestClass]
	public class UnitTestInterpreter
	{

		[TestInitialize]
		public void TestInitialize()
		{
			TestTarget context = new TestTarget();
			interpreter = context.Interpreter;
		}


		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Interpreter_NullArgument()
		{
			interpreter.ParameterGetter = _ => 0;
			interpreter.Execute(null);
		}


		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Interpreter_EmptyArgument()
		{
			interpreter.ParameterGetter = _ => 0;
			interpreter.Execute(String.Empty);
		}

		[TestMethod]
		public void Interpreter_SingleOperation()
		{
			interpreter.ParameterGetter = _ => 0;
			Assert.IsTrue(interpreter.Execute("1+2") == 3);
		}


		[TestMethod]
		public void Interpreter_Precedence()
		{
			interpreter.ParameterGetter = _ => 0;
			Assert.IsTrue(interpreter.Execute("1+2*3") == 7);
		}

		[TestMethod]
		public void Interpreter_PrecedenceParentheses1()
		{
			interpreter.ParameterGetter = _ => 0;
			Assert.IsTrue(interpreter.Execute("(1+2)*3") == 9);
		}

		[TestMethod]
		public void Interpreter_PrecedenceParentheses2()
		{
			interpreter.ParameterGetter = _ => 0;
			Assert.IsTrue(interpreter.Execute("(1+2)*(3+4)") == 21);
		}

		[TestMethod]
		public void Interpreter_RedundantParentheses1()
		{
			interpreter.ParameterGetter = _ => 0;
			Assert.IsTrue(interpreter.Execute("(1)") == 1);
		}

		[TestMethod]
		public void Interpreter_RedundantParentheses2()
		{
			interpreter.ParameterGetter = _ => 0;
			Assert.IsTrue(interpreter.Execute("((1))") == 1);
		}

		[TestMethod]
		public void Interpreter_RedundantParentheses3()
		{
			interpreter.ParameterGetter = _ => 0;
			Assert.IsTrue(interpreter.Execute("(1)+(2)") == 3);
		}

		[TestMethod]
		public void Interpreter_RedundantParentheses4()
		{
			interpreter.ParameterGetter = _ => 0;
			Assert.IsTrue(interpreter.Execute("((1)+(2))") == 3);
		}

		

		

		[TestMethod]
		[ExpectedException(typeof(InterpreterException))]
		public void Interpreter_DivisionByZero()
		{
			interpreter.ParameterGetter = _ => 0;
			interpreter.Execute("1/(1-1)");
		}

		[TestMethod]
		[ExpectedException(typeof(InterpreterException))]
		public void Interpreter_Overflow1()
		{
			interpreter.ParameterGetter = _ => 0;
			interpreter.Execute(Int32.MaxValue.ToString(CultureInfo.InvariantCulture) + "+1");
		}

		[TestMethod]
		[ExpectedException(typeof(InterpreterException))]
		public void Interpreter_Overflow2()
		{
			interpreter.ParameterGetter = _ => 0;
			interpreter.Execute(String.Format("1-{0}-{0}", Int32.MaxValue.ToString(CultureInfo.InvariantCulture)));
		}

		

		

		[TestMethod]
		[ExpectedException(typeof(ParserException))]
		public void Interpreter_InvalidExpression3()
		{
			interpreter.ParameterGetter = _ => 0;
			interpreter.Execute("(1");
		}

		[TestMethod]
		[ExpectedException(typeof(ParserException))]
		public void Interpreter_InvalidExpression4()
		{
			interpreter.ParameterGetter = _ => 0;
			interpreter.Execute("1)");
		}

		[TestMethod]
		[ExpectedException(typeof(ParserException))]
		public void Interpreter_InvalidExpression5()
		{
			interpreter.ParameterGetter = _ => 0;
			interpreter.Execute("(1+2)*(3+4)*5/");
		}

		[TestMethod]
		[ExpectedException(typeof(ParserException))]
		public void Interpreter_InvalidExpression6()
		{
			interpreter.ParameterGetter = _ => 0;
			interpreter.Execute("(1+2)*(7+(3+4*(8-9))");
		}

		private IInterpreter interpreter;
	}
}
