namespace Panosen.Markdown.Blocks
{
    /// <summary>
    /// Represents a heading.
    /// <seealso href="https://spec.commonmark.org/0.29/#atx-headings">Single-Line Header CommonMark Spec</seealso>
    /// <seealso href="https://spec.commonmark.org/0.29/#setext-headings">Two-Line Header CommonMark Spec</seealso>
    /// </summary>
    public class HeaderBlock : MarkdownInlineBlock
    {
        /// <summary>
        /// Header
        /// </summary>
        public override MarkdownBlockType Type => MarkdownBlockType.Header;

        /// <summary>
        /// header level (1-6).
        /// </summary>
        public int HeaderLevel { get; set; }
    }

    /// <summary>
    /// HeaderBlockExtension
    /// </summary>
    public static class HeaderBlockExtension
    {
        /// <summary>
        /// SetHeaderLevel
        /// </summary>
        /// <returns></returns>
        public static HeaderBlock SetHeaderLevel(this HeaderBlock headerBlock, int headerLevel)
        {
            headerBlock.HeaderLevel = headerLevel;

            return headerBlock;
        }
    }
}