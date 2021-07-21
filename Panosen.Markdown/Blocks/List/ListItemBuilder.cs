// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text;

namespace Panosen.Markdown.Blocks
{
    public class ListItemBuilder : MarkdownBlock
    {
        public override MarkdownBlockType Type => MarkdownBlockType.ListItemBuilder;

        public StringBuilder Builder { get; } = new StringBuilder();
    }
}