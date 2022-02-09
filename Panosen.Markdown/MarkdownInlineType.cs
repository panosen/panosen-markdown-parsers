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
        Comment = 201,

        /// <summary>
        /// A text run
        /// </summary>
        TextRun = 202,

        /// <summary>
        /// A bold run
        /// </summary>
        Bold = 203,

        /// <summary>
        /// An italic run
        /// </summary>
        Italic = 204,

        /// <summary>
        /// A link in markdown syntax
        /// </summary>
        MarkdownLink = 205,

        /// <summary>
        /// A raw hyper link
        /// </summary>
        RawHyperlink = 206,

        /// <summary>
        /// A raw subreddit link
        /// </summary>
        RawSubreddit = 207,

        /// <summary>
        /// A strike through run
        /// </summary>
        Strikethrough = 208,

        /// <summary>
        /// A superscript run
        /// </summary>
        Superscript = 209,

        /// <summary>
        /// A subscript run
        /// </summary>
        Subscript = 210,

        /// <summary>
        /// A code run
        /// </summary>
        Code = 211,

        /// <summary>
        /// An image
        /// </summary>
        Image = 212,

        /// <summary>
        /// Emoji
        /// </summary>
        Emoji = 213,

        /// <summary>
        /// Link Reference
        /// </summary>
        LinkReference = 214
    }
}