// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Panosen.Markdown.Blocks;

namespace Panosen.Markdown
{
    /// <summary>
    /// Represents a Markdown Document.
    /// </summary>
    public class MarkdownDocument : MarkdownBlock
    {
        /// <summary>
        /// Root
        /// </summary>
        public override MarkdownBlockType Type => MarkdownBlockType.Root;

        /// <summary>
        /// References
        /// </summary>
        public Dictionary<string, LinkReferenceBlock> References { get; set; }

        /// <summary>
        /// Gets or sets the list of block elements.
        /// </summary>
        public List<MarkdownBlock> Blocks { get; set; }
    }

    /// <summary>
    /// MarkdownDocumentExtension
    /// </summary>
    public static class MarkdownDocumentExtension
    {
        /// <summary>
        /// AddBlock
        /// </summary>
        public static MarkdownDocument AddBlock(this MarkdownDocument markdownDocument, MarkdownBlock markdownBlock)
        {
            if (markdownDocument.Blocks == null)
            {
                markdownDocument.Blocks = new List<MarkdownBlock>();
            }

            markdownDocument.Blocks.Add(markdownBlock);

            return markdownDocument;
        }

        /// <summary>
        /// AddBlock
        /// </summary>
        public static TMarkdownBlock AddBlock<TMarkdownBlock>(this MarkdownDocument markdownDocument)
            where TMarkdownBlock : MarkdownBlock, new()
        {
            if (markdownDocument.Blocks == null)
            {
                markdownDocument.Blocks = new List<MarkdownBlock>();
            }

            TMarkdownBlock markdownBlock = new TMarkdownBlock();

            markdownDocument.Blocks.Add(markdownBlock);

            return markdownBlock;
        }
    }

}