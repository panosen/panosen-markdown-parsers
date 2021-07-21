// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Panosen.Markdown.Inlines
{
    /// <summary>
    /// Represents a span containing superscript text.
    /// ^(sample)
    /// </summary>
    public class SuperscriptTextInline : MarkdownInline
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuperscriptTextInline"/> class.
        /// </summary>
        public SuperscriptTextInline()
            : base(MarkdownInlineType.Superscript)
        {
        }

        /// <summary>
        /// Gets or sets the contents of the inline.
        /// </summary>
        public IList<MarkdownInline> Inlines { get; set; }
    }
}