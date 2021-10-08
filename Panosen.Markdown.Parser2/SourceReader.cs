using System;
using System.Collections.Generic;
using System.Text;

namespace Panosen.Markdown.Parser2
{
    public class SourceReader
    {
        private readonly string text;

        private int index;
        private int row = 1;
        private int col = 1;

        public SourceReader(string text)
        {
            this.index = 0;
            this.text = text;
        }

        public char? Read()
        {
            if (string.IsNullOrEmpty(this.text))
            {
                return null;
            }

            if (index >= this.text.Length)
            {
                return null;
            }

            var ch = this.text[this.index];
            this.index++;
            return ch;
        }

        public char? ViewOne()
        {
            if (string.IsNullOrEmpty(this.text))
            {
                return null;
            }

            if (index >= this.text.Length)
            {
                return null;
            }

            return this.text[this.index];
        }

        public int Row => this.row;

        public int Col => this.col;
    }
}
