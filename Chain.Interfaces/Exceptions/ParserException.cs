using System;

namespace Chain.Interfaces.Exceptions
{
	[Serializable]
	public class ParserException : Exception
	{
		public ParserException() : base() { }
		public ParserException(string message) : base(message) { }
		public ParserException(string message, Exception innerException) : base(message, innerException) { }
	}
}
