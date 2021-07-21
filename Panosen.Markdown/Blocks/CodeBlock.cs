// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text;

namespace Panosen.Markdown.Parsers.Blocks
{
    /// <summary>
    /// Represents a block of text that is displayed in a fixed-width font.  Inline elements and
    /// escape sequences are ignored inside the code block.
    /// </summary>
    public class CodeBlock : MarkdownBlock
    {
        /// <summary>
        /// Code
        /// </summary>
        public override MarkdownBlockType Type => MarkdownBlockType.Code;

        /// <summary>
        /// Gets or sets the source code to display.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the Language specified in prefix, e.g. ```c# (GitHub Style Parsing).<para/>
        /// This does not guarantee that the Code Block has a language, or no language, some valid code might not have been prefixed, and this will still return null. <para/>
        /// To ensure all Code is Highlighted (If desired), you might have to determine the language from the provided string, such as looking for key words.
        /// </summary>
        public string CodeLanguage { get; set; }
    }
}