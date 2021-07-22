// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace Panosen.Markdown.Blocks
{
    /// <summary>
    /// Represents a heading.
    /// <seealso href="https://spec.commonmark.org/0.29/#atx-headings">Single-Line Header CommonMark Spec</seealso>
    /// <seealso href="https://spec.commonmark.org/0.29/#setext-headings">Two-Line Header CommonMark Spec</seealso>
    /// </summary>
    public class HeaderBlock : MarkdownBlock
    {
        /// <summary>
        /// Header
        /// </summary>
        public override MarkdownBlockType Type => MarkdownBlockType.Header;

        private int _headerLevel;

        /// <summary>
        /// Gets or sets the header level (1-6).  1 is the most important header, 6 is the least important.
        /// </summary>
        public int HeaderLevel
        {
            get
            {
                return _headerLevel;
            }

            set
            {
                if (value < 1 || value > 6)
                {
                    throw new ArgumentOutOfRangeException("HeaderLevel", "The header level must be between 1 and 6 (inclusive).");
                }

                _headerLevel = value;
            }
        }

        /// <summary>
        /// Gets or sets the contents of the block.
        /// </summary>
        public List<MarkdownInline> Inlines { get; set; }

    }
}