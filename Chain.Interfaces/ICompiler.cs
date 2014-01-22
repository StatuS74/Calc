using System.Collections.Generic;

namespace Chain.Interfaces
{
	public interface ICompiler
	{

		byte[] Compile(string input);
	}
}
