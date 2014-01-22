using System.Collections.Generic;
using System.Linq.Expressions;

namespace Chain.Interfaces
{

	public interface IParser
	{
		Expression Parse(IList<ILexeme> lexemes);
	}
}
