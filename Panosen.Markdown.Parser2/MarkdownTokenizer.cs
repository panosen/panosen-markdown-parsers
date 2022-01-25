using Panosen.Compling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Markdown.Parser2
{
    public class MarkdownTokenizer : ITokenizer
    {
        public TokenCollection Analyze(string text)
        {
            TokenCollection tokenCollection = new TokenCollection();

            StringBuilder builder = new StringBuilder();

            var row = 0;
            var col = 0;

            var reader = new SourceReader(text);
            while (reader.ViewOne() != null)
            {
                if (builder.Length == 0)
                {
                    row = reader.Row;
                    col = reader.Col;
                }

                var value = reader.Read().Value.ToString();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    builder.Append(value);
                    continue;
                }

                if (builder.Length > 0)
                {
                    tokenCollection.AddToken(value: builder.ToString(), row: row, col: col);
                    builder.Clear();
                    row = 0;
                    col = 0;
                }
            }

            if (builder.Length > 0)
            {
                tokenCollection.AddToken(value: builder.ToString(), row: row, col: col);
            }

            return tokenCollection;
        }
    }
}
