using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Chain.Interfaces;
using Chain.Interfaces.Exceptions;

namespace Chain.Components
{
    public class Lexer : ILexer
    {

        public IList<ILexeme> Analyze(string input)
        {
            if (input == null) throw new ArgumentNullException();

            IList<ILexeme> lexemes = new List<ILexeme>();

            for (int current = 0; current < input.Length; )
            {
                if (input[current] == '+')
                {
                    lexemes.Add(new Lexeme(LexemeType.OperatorAdd));
                    current++;
                    continue;
                }
                else if (input[current] == '/')
                {
                    lexemes.Add(new Lexeme(LexemeType.OperatorDiv));
                    current++;
                    continue;
                }
                else if (input[current] == '*')
                {
                    lexemes.Add(new Lexeme(LexemeType.OperatorMul));
                    current++;
                    continue;
                }
                else if (input[current] == '-')
                {
                    lexemes.Add(new Lexeme(LexemeType.OperatorSub));
                    current++;
                    continue;
                }
                else if (input[current] == '(')
                {
                    lexemes.Add(new Lexeme(LexemeType.ParenthesesLeft));
                    current++;
                    continue;
                }
                else if (input[current] == ')')
                {
                    lexemes.Add(new Lexeme(LexemeType.ParenthesesRight));
                    current++;
                    continue;
                }
                else if (input[current] >= '1' && input[current] <= '9')
                {
                    int literalposition = current;
                    var number = new StringBuilder();
                    do
                    {
                        number.Append(input[current++]);
                    }
                    while (current < input.Length && input[current] >= '0' && input[current] <= '9');
                    int result;
                    if (Int32.TryParse(number.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
                    {
                        lexemes.Add(new Lexeme(result));
                        continue;
                    }
                    else
                    {
                       
                        throw new LexerException(String.Format("Неверный  literal '{0}' в позиции {1}", number, literalposition));
                    }
                }

                throw new LexerException(String.Format("Неверный символ  в позиции {0}", (int)(current)));
            }

            return lexemes;
        }
    }
}
