// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace Panosen.Markdown.Inlines
{
    /// <summary>
    /// Represents a span containing subscript text.
    /// </summary>
    public class SubscriptTextInline : MarkdownInline
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptTextInline"/> class.
        /// &lt;sub&gt;sample&lt;/sub&gt;
        /// </summary>
        public SubscriptTextInline()
            : base(MarkdownInlineType.Subscript)
        {
        }

        /// <summary>
        /// Gets or sets the contents of the inline.
        /// </summary>
        public IList<MarkdownInline> Inlines { get; set; }
    }
}
