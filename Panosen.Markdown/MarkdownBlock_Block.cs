using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Markdown
{
    /// <summary>
    /// MarkdownBlock with Blocks
    /// </summary>
    public abstract class MarkdownBlockBlock : MarkdownBlock
    {

        /// <summary>
        /// Gets or sets the contents of the block.
        /// </summary>
        public List<MarkdownBlock> Blocks { get; set; }
    }

    /// <summary>
    /// MarkdownBlockBlockExtension
    /// </summary>
    public static class MarkdownBlockBlockExtension
    {
        /// <summary>
        /// AddBlock
        /// </summary>
        public static MarkdownBlockBlock AddBlock(this MarkdownBlockBlock markdownBlockBlock, MarkdownBlock markdownBlock)
        {
            if (markdownBlockBlock.Blocks == null)
            {
                markdownBlockBlock.Blocks = new List<MarkdownBlock>();
            }

            markdownBlockBlock.Blocks.Add(markdownBlock);

            return markdownBlockBlock;
        }

        /// <summary>
        /// AddBlock
        /// </summary>
        public static TMarkdownBlock AddBlock<TMarkdownBlock>(this MarkdownBlockBlock markdownBlockBlock)
            where TMarkdownBlock : MarkdownBlock, new()
        {
            if (markdownBlockBlock.Blocks == null)
            {
                markdownBlockBlock.Blocks = new List<MarkdownBlock>();
            }

            TMarkdownBlock markdownBlock = new TMarkdownBlock();

            markdownBlockBlock.Blocks.Add(markdownBlock);

            return markdownBlock;
        }
        /// <summary>
        /// AddBlock
        /// </summary>
        public static MarkdownBlockBlock AddBlocks(this MarkdownBlockBlock markdownBlockBlock, List<MarkdownBlock> markdownBlocks)
        {
            if (markdownBlocks == null)
            {
                return markdownBlockBlock;
            }

            if (markdownBlockBlock.Blocks == null)
            {
                markdownBlockBlock.Blocks = new List<MarkdownBlock>();
            }

            markdownBlockBlock.Blocks.AddRange(markdownBlocks);

            return markdownBlockBlock;
        }
    }
}
