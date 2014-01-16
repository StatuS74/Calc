using System;
using Chain.Interfaces;

namespace Chain.Components
{
	public class Lexeme : ILexeme
	{
		public Lexeme(LexemeType type) { Type = type; }
		public Lexeme(string name) { Type = LexemeType.Parameter; Name = name; }
		public Lexeme(int value) { Type = LexemeType.IntegerLiteral; Value = value; }

		public LexemeType Type { get; set; }
		public string Name { get; set; }
		public Nullable<int> Value { get; set; }

		public override string ToString()
		{
			switch (Type)
			{
				case LexemeType.IntegerLiteral: return Type.ToString() + " (" + Value + ")";
				case LexemeType.Parameter: return Type.ToString() + " (" + Name + ")";
				default: return Type.ToString();
			}
		}
	}
}
