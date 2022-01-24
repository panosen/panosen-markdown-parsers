// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Panosen.Markdown.Inlines
{
    /// <summary>
    /// Represents a span containing emoji symbol.
    /// </summary>
    public partial class EmojiInline : MarkdownInline
    {
        /// <summary>
        /// Emoji
        /// </summary>
        public override MarkdownInlineType Type => MarkdownInlineType.Emoji;

        /// <inheritdoc/>
        public string Text { get; set; }
    }
}