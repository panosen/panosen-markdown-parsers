// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Panosen.Markdown
{
    /// <summary>
    /// Determines the type of Inline the Inline Element is.
    /// </summary>
    public enum MarkdownInlineType
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// A comment
        /// </summary>
        Comment = 1,

        /// <summary>
        /// A text run
        /// </summary>
        TextRun = 2,

        /// <summary>
        /// A bold run
        /// </summary>
        Bold = 3,

        /// <summary>
        /// An italic run
        /// </summary>
        Italic = 4,

        /// <summary>
        /// A link in markdown syntax
        /// </summary>
        MarkdownLink = 5,

        /// <summary>
        /// A raw hyper link
        /// </summary>
        RawHyperlink = 6,

        /// <summary>
        /// A raw subreddit link
        /// </summary>
        RawSubreddit = 7,

        /// <summary>
        /// A strike through run
        /// </summary>
        Strikethrough = 8,

        /// <summary>
        /// A superscript run
        /// </summary>
        Superscript = 9,

        /// <summary>
        /// A subscript run
        /// </summary>
        Subscript = 10,

        /// <summary>
        /// A code run
        /// </summary>
        Code = 11,

        /// <summary>
        /// An image
        /// </summary>
        Image = 12,

        /// <summary>
        /// Emoji
        /// </summary>
        Emoji = 13,

        /// <summary>
        /// Link Reference
        /// </summary>
        LinkReference = 14
    }
}