// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Panosen.Markdown.Inlines
{
    /// <summary>
    /// Represents a span containing strikethrough text.
    /// </summary>
    public class StrikethroughTextInline : MarkdownInline
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StrikethroughTextInline"/> class.
        /// </summary>
        public StrikethroughTextInline()
            : base(MarkdownInlineType.Strikethrough)
        {
        }

        /// <summary>
        /// Gets or sets The contents of the inline.
        /// </summary>
        public IList<MarkdownInline> Inlines { get; set; }

        /// <summary>
        /// Converts the object into it's textual representation.
        /// </summary>
        /// <returns> The textual representation of this object. </returns>
        public override string ToString()
        {
            if (Inlines == null)
            {
                return base.ToString();
            }

            return "~~" + string.Join(string.Empty, Inlines) + "~~";
        }
    }
}