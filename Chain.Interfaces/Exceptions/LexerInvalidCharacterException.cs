using System;

namespace Chain.Interfaces.Exceptions
{
	[Obsolete]
	[Serializable]
	public class LexerInvalidCharacterException : LexerException
	{
		public LexerInvalidCharacterException() : base() { }
		public LexerInvalidCharacterException(string message) : base(message) { }
		public LexerInvalidCharacterException(string message, Exception innerException) : base(message, innerException) { }

		public LexerInvalidCharacterException(int position, char character)
			: base(String.Format("Invalid character U+{0:x4} at position {1}", (int)character, position))
		{
			Position = position;
			Character = character;
		}
		public LexerInvalidCharacterException(string message, int position, char character) : base(message)
		{
			Position = position;
			Character = character;
		}

		public int Position { get; set; }
		public char Character { get; set; }
	}
}
