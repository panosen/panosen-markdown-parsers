// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Panosen.Markdown.Inlines
{
    /// <summary>
    /// Represents a span containing plain text.
    /// </summary>
    public class TextRunInline : MarkdownInline
    {
        /// <summary>
        /// TextRun
        /// </summary>
        public override MarkdownInlineType Type => MarkdownInlineType.TextRun;

        /// <summary>
        /// Gets or sets the text for this run.
        /// </summary>
        public string Text { get; set; }
    }

    /// <summary>
    /// TextRunInlineExtension
    /// </summary>
    public static class TextRunInlineExtension
    {
        /// <summary>
        /// SetText
        /// </summary>
        public static TextRunInline SetText(this TextRunInline textRunInline, string text)
        {
            textRunInline.Text = text;

            return textRunInline;
        }
    }
}