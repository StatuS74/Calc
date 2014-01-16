using System;

namespace Chain.Interfaces
{
	public interface IRuntime
	{
		Func<string, int> ParameterGetter { get; set; }
		int Execute(byte[] bytes);
	}
}
