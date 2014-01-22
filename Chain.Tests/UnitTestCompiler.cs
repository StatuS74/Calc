using Chain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chain.Tests
{
	[TestClass]
	public class UnitTestCompiler
	{

		[TestInitialize]
		public void TestInitialize()
		{
			TestTarget context = new TestTarget();
			compiler = context.Compiler;
		}

		private ICompiler compiler;
	}
}
