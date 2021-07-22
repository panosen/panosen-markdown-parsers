using System;
using System.Collections.Generic;
using System.Text;

namespace Panosen.Markdown.Parser2
{
    public struct Token
    {
        public TokenType TokenType { get; set; }

        public string Text { get; set; }
    }
}
