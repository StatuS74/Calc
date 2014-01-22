using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chain.Interfaces
{
	public enum OpCode : byte
	{

		Input =		10,
		Output =	11,
		Pushc =		20,
		Pushv =		21,
		Pop =		22,
		Add =		30,
		Sub =		31,
		Mul =		32,
		Div =		33,

	}
}
