// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Panosen.Markdown.Inlines
{
    /// <summary>
    /// Represents a span containing code, or other text that is to be displayed using a
    /// fixed-width font.
    /// </summary>
    public class CodeInline : MarkdownInline
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CodeInline"/> class.
        /// </summary>
        public CodeInline()
            : base(MarkdownInlineType.Code)
        {
        }

        /// <summary>
        /// Gets or sets the text to display as code.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Converts the object into it's textual representation.
        /// </summary>
        /// <returns> The textual representation of this object. </returns>
        public override string ToString()
        {
            if (Text == null)
            {
                return base.ToString();
            }

            return "`" + Text + "`";
        }
    }
}