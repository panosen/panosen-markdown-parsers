// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Panosen.Markdown.Blocks
{
    /// <summary>
    /// Represents a block of text that is displayed as a single paragraph.
    /// </summary>
    public class ParagraphBlock : MarkdownBlock
    {
        /// <summary>
        /// Paragraph
        /// </summary>
        public override MarkdownBlockType Type => MarkdownBlockType.Paragraph;

        /// <summary>
        /// Gets or sets the contents of the block.
        /// </summary>
        public IList<MarkdownInline> Inlines { get; set; }
    }
}