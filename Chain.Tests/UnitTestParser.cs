using System.Collections.Generic;
using Chain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chain.Tests
{
	[TestClass]
	public class UnitTestParser
	{

		[TestInitialize]
		public void TestInitialize()
		{
			TestTarget context = new TestTarget();
			parser = context.Parser;
		}


		private IParser parser;
	}
}
