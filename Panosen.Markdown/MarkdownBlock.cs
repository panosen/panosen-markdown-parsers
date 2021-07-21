// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Panosen.Markdown
{
    /// <summary>
    /// A Block Element is an element that is a container for other structures.
    /// </summary>
    public abstract class MarkdownBlock : MarkdownElement
    {
        /// <summary>
        /// Gets or sets tells us what type this element is.
        /// </summary>
        public MarkdownBlockType Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownBlock"/> class.
        /// </summary>
        public MarkdownBlock(MarkdownBlockType type)
        {
            Type = type;
        }
    }
}