// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Panosen.Markdown.Blocks
{
    /// <summary>
    /// Represents a horizontal line.
    /// ---
    /// </summary>
    public class HorizontalRuleBlock : MarkdownBlock
    {
        /// <summary>
        /// HorizontalRule
        /// </summary>
        public override MarkdownBlockType Type => MarkdownBlockType.HorizontalRule;
    }
}