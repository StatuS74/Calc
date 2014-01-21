using System.Collections.Generic;

namespace Chain.Interfaces
{
	public interface ILexer
	{

		IList<ILexeme> Analyze(string input);

	}
}
