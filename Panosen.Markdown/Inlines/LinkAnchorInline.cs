// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Xml.Linq;

namespace Panosen.Markdown.Inlines
{
    /// <summary>
    /// Represents a span that contains a reference for links to point to.
    /// </summary>
    public class LinkAnchorInline : MarkdownInline
    {
        /// <summary>
        /// LinkAnchorInline
        /// </summary>
        public LinkAnchorInline()
            : base(MarkdownInlineType.LinkReference)
        {
        }

        /// <summary>
        /// Gets or sets the Name of this Link Reference.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the raw Link Reference.
        /// </summary>
        public string Raw { get; set; }
    }
}