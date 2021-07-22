namespace Panosen.Markdown.Blocks
{
    /// <summary>
    /// QuoteBlock
    /// </summary>
    public class QuoteBlock : MarkdownBlockBlock
    {
        /// <summary>
        /// Quote
        /// </summary>
        public override MarkdownBlockType Type => MarkdownBlockType.Quote;
    }
}