// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Panosen.Markdown.Blocks;

namespace Panosen.Markdown
{
    /// <summary>
    /// Represents a Markdown Document.
    /// </summary>
    public class MarkdownDocument : MarkdownBlock
    {
        /// <summary>
        /// Root
        /// </summary>
        public override MarkdownBlockType Type => MarkdownBlockType.Root;

        /// <summary>
        /// References
        /// </summary>
        public Dictionary<string, LinkReferenceBlock> References { get; set; }

        /// <summary>
        /// Gets or sets the list of block elements.
        /// </summary>
        public IList<MarkdownBlock> Blocks { get; set; }
    }
}