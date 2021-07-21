// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Panosen.Markdown.Blocks
{
    /// <summary>
    /// Represents a list, with each list item proceeded by either a number or a bullet.
    /// </summary>
    public class ListBlock : MarkdownBlock
    {
        /// <summary>
        /// List
        /// </summary>
        public override MarkdownBlockType Type => MarkdownBlockType.List;

        /// <summary>
        /// Gets or sets the list items.
        /// </summary>
        public List<ListItemBlock> Items { get; set; }

        /// <summary>
        /// Gets or sets the style of the list, either numbered or bulleted.
        /// </summary>
        public ListStyle Style { get; set; }
    }
}