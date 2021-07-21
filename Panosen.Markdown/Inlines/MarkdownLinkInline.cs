// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace Panosen.Markdown.Inlines
{
    /// <summary>
    /// Represents a type of hyperlink where the text can be different from the target URL.
    /// </summary>
    public class MarkdownLinkInline : MarkdownInline
    {
        /// <summary>
        /// MarkdownLink
        /// </summary>
        public override MarkdownInlineType Type => MarkdownInlineType.MarkdownLink;

        /// <summary>
        /// Gets or sets the contents of the inline.
        /// </summary>
        public IList<MarkdownInline> Inlines { get; set; }

        /// <summary>
        /// Gets or sets the link URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets a tooltip to display on hover.
        /// </summary>
        public string Tooltip { get; set; }

        /// <summary>
        /// Gets or sets the ID of a reference, if this is a reference-style link.
        /// </summary>
        public string ReferenceId { get; set; }
    }
}