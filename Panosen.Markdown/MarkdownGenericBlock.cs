using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Markdown
{
    /// <summary>
    /// MarkdownBlock with Inlines
    /// </summary>
    public abstract class MarkdownGenericBlock<TT> : MarkdownBlock
    {

        /// <summary>
        /// Gets or sets the contents of the block.
        /// </summary>
        public List<TT> Inlines { get; set; }
    }

    /// <summary>
    /// MarkdownBlock with Inlines
    /// </summary>
    public abstract class MarkdownInlineBlock : MarkdownBlock
    {

        /// <summary>
        /// Gets or sets the contents of the block.
        /// </summary>
        public List<MarkdownInline> Inlines { get; set; }
    }

    /// <summary>
    /// MarkdownInlineBlockExtension
    /// </summary>
    public static class MarkdownInlineBlockExtension
    {
        /// <summary>
        /// AddInline
        /// </summary>
        public static MarkdownInlineBlock AddInline(this MarkdownInlineBlock markdownInlineBlock, MarkdownInline markdownInline)
        {
            if (markdownInlineBlock.Inlines == null)
            {
                markdownInlineBlock.Inlines = new List<MarkdownInline>();
            }

            markdownInlineBlock.Inlines.Add(markdownInline);

            return markdownInlineBlock;
        }

        /// <summary>
        /// AddInline
        /// </summary>
        public static TMarkdownInline AddInline<TMarkdownInline>(this MarkdownInlineBlock markdownInlineBlock)
            where TMarkdownInline : MarkdownInline, new()
        {
            if (markdownInlineBlock.Inlines == null)
            {
                markdownInlineBlock.Inlines = new List<MarkdownInline>();
            }

            TMarkdownInline markdownInline = new TMarkdownInline();

            markdownInlineBlock.Inlines.Add(markdownInline);

            return markdownInline;
        }
        /// <summary>
        /// AddInline
        /// </summary>
        public static MarkdownInlineBlock AddInlines(this MarkdownInlineBlock markdownInlineBlock, List<MarkdownInline> markdownInlines)
        {
            if (markdownInlines == null)
            {
                return markdownInlineBlock;
            }

            if (markdownInlineBlock.Inlines == null)
            {
                markdownInlineBlock.Inlines = new List<MarkdownInline>();
            }

            markdownInlineBlock.Inlines.AddRange(markdownInlines);

            return markdownInlineBlock;
        }
    }
}
