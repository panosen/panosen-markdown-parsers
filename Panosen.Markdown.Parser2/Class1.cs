using Panosen.Markdown.Blocks;
using Panosen.Markdown.Inlines;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panosen.Markdown.Parser2
{
    public class Class1
    {
        public static MarkdownDocument Process(string text)
        {
            MarkdownDocument markdownDocument = new MarkdownDocument();

            var tokens = NewMethod(text);

            return markdownDocument;
        }

        private static TokenCollection NewMethod(string text)
        {
            TokenCollection tokens = new TokenCollection();

            var sourceReader = new SourceReader(text);

            var ch = sourceReader.Read();
            while (ch != null)
            {
                switch (ch.Value)
                {
                    case '#':
                        tokens.AddToken<HashToken>("#");
                        break;
                    case '>':
                        tokens.AddToken<GreaterThanToken>(">");
                        break;
                    case '*':
                        tokens.AddToken<StarToken>("*");
                        break;
                    case '.':
                        tokens.AddToken<DotToken>(".");
                        break;
                    case '[':
                        tokens.AddToken<LeftMiddleBracketToken>("[");
                        break;
                    case ']':
                        tokens.AddToken<RightMiddleBracketToken>("]");
                        break;
                    case '(':
                        tokens.AddToken<LeftSmallBracktetToken>("(");
                        break;
                    case ')':
                        tokens.AddToken<RightSmallBracketToken>(")");
                        break;
                    case '!':
                        tokens.AddToken<ExcalmatoryToken>("!");
                        break;
                    case '\n':
                        tokens.AddToken<NewLineToken>("\n");
                        break;
                    case '\r':
                        break;
                    default:
                        tokens.AddToken<PlainToken>(ch.ToString());
                        break;
                }

                ch = sourceReader.Read();
            }

            return tokens;
        }
    }
}
