using System;
using System.Collections.Generic;

namespace Chain.Interfaces
{
	public interface IInterpreter
	{
		Func<string, int> ParameterGetter { get; set; }
		int Execute(string input);
	}
}
