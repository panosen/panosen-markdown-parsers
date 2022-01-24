// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Panosen.Markdown.Blocks
{
    /// <summary>
    /// Represents the target of a reference ([ref][]).
    /// </summary>
    public class LinkReferenceBlock : MarkdownBlock
    {
        /// <summary>
        /// LinkReference
        /// </summary>
        public override MarkdownBlockType Type => MarkdownBlockType.LinkReference;

        /// <summary>
        /// Gets or sets the reference ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the link URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets a tooltip to display on hover.
        /// </summary>
        public string Tooltip { get; set; }
    }
}