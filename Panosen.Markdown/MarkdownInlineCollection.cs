using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Markdown
{
    /// <summary>
    /// MarkdownInlineCollection
    /// </summary>
    public class MarkdownInlineCollection
    {
        /// <summary>
        /// Inlines
        /// </summary>
        public List<MarkdownInline> Inlines { get; set; }
    }

    /// <summary>
    /// MarkdownInlineCollectionExtension
    /// </summary>
    public static class MarkdownInlineCollectionExtension
    {
        /// <summary>
        /// AddInline
        /// </summary>
        public static MarkdownInlineCollection AddInline(this MarkdownInlineCollection markdownInlineCollection, MarkdownInline markdownInline)
        {
            if (markdownInlineCollection.Inlines == null)
            {
                markdownInlineCollection.Inlines = new List<MarkdownInline>();
            }

            markdownInlineCollection.Inlines.Add(markdownInline);

            return markdownInlineCollection;
        }

        /// <summary>
        /// AddInline
        /// </summary>
        public static TMarkdownInline AddInline<TMarkdownInline>(this MarkdownInlineCollection markdownInlineCollection)
            where TMarkdownInline : MarkdownInline, new()
        {
            if (markdownInlineCollection.Inlines == null)
            {
                markdownInlineCollection.Inlines = new List<MarkdownInline>();
            }

            TMarkdownInline markdownInline = new TMarkdownInline();

            markdownInlineCollection.Inlines.Add(markdownInline);

            return markdownInline;
        }
        /// <summary>
        /// AddInline
        /// </summary>
        public static MarkdownInlineCollection AddInlines(this MarkdownInlineCollection markdownInlineCollection, List<MarkdownInline> markdownInlines)
        {
            if (markdownInlines == null)
            {
                return markdownInlineCollection;
            }

            if (markdownInlineCollection.Inlines == null)
            {
                markdownInlineCollection.Inlines = new List<MarkdownInline>();
            }

            markdownInlineCollection.Inlines.AddRange(markdownInlines);

            return markdownInlineCollection;
        }
    }
}
