namespace Panosen.Markdown.Blocks
{
    /// <summary>
    /// Represents a block of text that is displayed as a single paragraph.
    /// </summary>
    public class ParagraphBlock : MarkdownInlineBlock
    {
        /// <summary>
        /// Paragraph
        /// </summary>
        public override MarkdownBlockType Type => MarkdownBlockType.Paragraph;
    }
}