// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Panosen.Markdown.Inlines
{
    /// <summary>
    /// Represents a type of hyperlink where the text and the target URL cannot be controlled
    /// independently.
    /// </summary>
    public class HyperlinkInline : MarkdownInline
    {
        /// <summary>
        /// RawHyperlink
        /// </summary>
        public override MarkdownInlineType Type => MarkdownInlineType.RawHyperlink;

        /// <summary>
        /// Gets or sets the text to display.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the URL to link to.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets this type of hyperlink does not have a tooltip.
        /// </summary>
        public string Tooltip { get; set; }

        /// <summary>
        /// Gets or sets the type of hyperlink.
        /// </summary>
        public HyperlinkType LinkType { get; set; }
    }
}