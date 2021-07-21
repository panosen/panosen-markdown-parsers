// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using Panosen.Markdown.Blocks;
using Panosen.Markdown.Parsers.Helpers;
using Panosen.Markdown.Parsers.Inlines;

namespace Panosen.Markdown.Parsers.Blocks
{
    /// <summary>
    /// Represents a block of text that is displayed as a single paragraph.
    /// </summary>
    public class ParagraphBlockParser
    {
        /// <summary>
        /// Parses paragraph text.
        /// </summary>
        /// <param name="markdown"> The markdown text. </param>
        /// <returns> A parsed paragraph. </returns>
        internal static ParagraphBlock Parse(string markdown)
        {
            var result = new ParagraphBlock();
            result.Inlines = Common.ParseInlineChildren(markdown, 0, markdown.Length);
            return result;
        }
    }
}