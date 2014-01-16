using System;

namespace Chain.Interfaces.Exceptions
{
	[Obsolete]
	[Serializable]
	public class LexerIntegerLiteralException : LexerException
	{
		public LexerIntegerLiteralException() : base() { }
		public LexerIntegerLiteralException(string message) : base(message) { }
		public LexerIntegerLiteralException(string message, Exception innerException) : base(message, innerException) { }

		public LexerIntegerLiteralException(int position, string value)
			: base(String.Format("Invalid integer literal '{0}' at position {1}", value, position))
		{
			Position = position;
			Value = value;
		}
		public LexerIntegerLiteralException(string message, int position, string value)
			: base(message)
		{
			Position = position;
			Value = value;
		}

		public int Position { get; set; }
		public string Value { get; set; }
	}
}
