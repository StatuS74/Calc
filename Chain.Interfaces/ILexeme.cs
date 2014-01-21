using System;

namespace Chain.Interfaces
{
	public interface ILexeme
	{

		LexemeType Type { get; set; }
		string Name { get; set; }
		Nullable<int> Value { get; set; }
	}
}
