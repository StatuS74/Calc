using System;

namespace Chain.Interfaces.Exceptions
{
	[Serializable]
	public class LexerException : Exception
	{
		public LexerException() : base() { }
		public LexerException(string message) : base(message) { }
		public LexerException(string message, Exception innerException) : base(message, innerException) { }
	}
}
