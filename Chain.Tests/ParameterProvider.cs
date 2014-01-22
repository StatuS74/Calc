using System;
using System.Collections.Generic;

namespace Chain.Tests
{
	class ParameterProvider
	{
		public ParameterProvider(Dictionary<string, int> values)
		{
			this.values = values;
			foreach (var key in values.Keys)
			{
				callcount[key] = 0;
			}
		}


		public int Get(string name)
		{
			if (values.ContainsKey(name))
			{
				callcount[name]++;
				return values[name];
			}
			else
			{
				throw new ArgumentException();
			}
		}

		public bool AssertAllParametersAreProvided()
		{
			foreach (var count in callcount.Values)
			{
				if (count == 0) return false;
			}
			return true;
		}
		public bool AssertAllParametersAreProvidedOnlyOnce()
		{
			foreach (var count in callcount.Values)
			{
				if (count > 1) return false;
			}
			return true;
		}

		private readonly Dictionary<string, int> callcount = new Dictionary<string, int>();
		private readonly Dictionary<string, int> values;
	}
}
