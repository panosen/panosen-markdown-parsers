using System;
using System.Collections.Generic;
using System.Text;

namespace Panosen.Markdown.Parser2
{
    public class Tokenizer
    {
        public static List<Token> GetTokenList(string text)
        {
            List<Token> tokens = new List<Token>();
            if (string.IsNullOrEmpty(text))
            {
                return tokens;
            }

            StringBuilder current = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                switch (text[i])
                {
                    case '!':
                        ProcessToken(tokens, current);
                        tokens.Add(new Token { Text = text[i].ToString(), TokenType = TokenType.Excalmatory });

                        break;
                    case '[':
                        ProcessToken(tokens, current);
                        tokens.Add(new Token { Text = text[i].ToString(), TokenType = TokenType.LeftMiddleBracket });

                        break;
                    case ']':
                        ProcessToken(tokens, current);
                        tokens.Add(new Token { Text = text[i].ToString(), TokenType = TokenType.RightMiddleBracket });

                        break;
                    case '(':
                        ProcessToken(tokens, current);
                        tokens.Add(new Token { Text = text[i].ToString(), TokenType = TokenType.LeftSmallBracktet });

                        break;
                    case ')':
                        ProcessToken(tokens, current);
                        tokens.Add(new Token { Text = text[i].ToString(), TokenType = TokenType.RightSmallBracket });

                        break;
                    case '*':
                        ProcessToken(tokens, current);
                        tokens.Add(new Token { Text = text[i].ToString(), TokenType = TokenType.Star });

                        break;
                    default:
                        current.Append(text[i]);

                        break;
                }
            }
            if (current.Length > 0)
            {
                tokens.Add(new Token { Text = current.ToString(), TokenType = TokenType.Plain });
            }

            return tokens;
        }

        private static void ProcessToken(List<Token> tokens, StringBuilder current)
        {
            if (current.Length == 0)
            {
                return;
            }

            tokens.Add(new Token { Text = current.ToString(), TokenType = TokenType.Plain });
            current.Clear();
        }
    }
}
