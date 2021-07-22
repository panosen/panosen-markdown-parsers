using Panosen.Markdown.Blocks;
using Panosen.Markdown.Inlines;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panosen.Markdown.Parser2
{
    public class Class1
    {
        public static MarkdownDocument Process(string content)
        {
            MarkdownDocument markdownDocument = new MarkdownDocument();

            var lines = content.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                if (line[0] >= '0' && line[0] <= '9')
                {
                    ProcessNumber(markdownDocument, line);
                    continue;
                }
                switch (line[0])
                {
                    case '#':
                        {
                            ProcessHeader(markdownDocument, line);
                        }
                        break;
                    case '>':
                        {
                            markdownDocument
                                .AddBlock<QuoteBlock>()
                                .AddBlock<ParagraphBlock>()
                                .AddInlines(LineParser.ProcessLine(line.Substring(1)));
                        }
                        break;
                    case '*':
                        {
                            ProcessStar(markdownDocument, line);
                        }
                        break;
                    default:
                        {
                            markdownDocument.AddBlock<ParagraphBlock>().AddInlines(LineParser.ProcessLine(line));
                        }

                        break;
                }
            }

            return markdownDocument;
        }

        private static void ProcessNumber(MarkdownDocument markdownDocument, string line)
        {
            StringBuilder builder = new StringBuilder();
            int index = 0;
            while (index < line.Length && char.IsNumber(line[index]))
            {
                builder.Append(line[index]);

                index++;
            }
            if (index < line.Length && line[index] == '.' && index + 1 < line.Length && line[index + 1] == ' ')
            {
                var ol = new ListBlock();
                ol.Style = ListStyle.Numbered;
                ol.Items = new List<ListItemBlock>();
                markdownDocument.AddBlock(ol);

                var inlines = LineParser.ProcessLine(line.Substring(index + 2));

                //ParagraphBlock paragraphBlock = new ParagraphBlock();
                //paragraphBlock.Inlines = inlines;
                //ol.Items.Add(paragraphBlock);
            }
            else
            {
                markdownDocument.AddBlock<ParagraphBlock>().AddInlines(LineParser.ProcessLine(line));
            }
        }

        private static void ProcessStar(MarkdownDocument markdownDocument, string line)
        {
            StringBuilder builder = new StringBuilder();
            int index = 0;
            while (index < line.Length && char.IsNumber(line[index]))
            {
                builder.Append(line[index]);

                index++;
            }
            if (index < line.Length && line[index] == '.' && index + 1 < line.Length && line[index + 1] == ' ')
            {
                var ul = new ListBlock();
                ul.Style = ListStyle.Numbered;
                ul.Items = new List<ListItemBlock>();
                markdownDocument.AddBlock(ul);

                var inlines = LineParser.ProcessLine(line.Substring(index + 2));

                //ParagraphBlock paragraphBlock = new ParagraphBlock();
                //paragraphBlock.Inlines = inlines;
                //ol.Items.Add(paragraphBlock);
            }
            else
            {
                markdownDocument.AddBlock<ParagraphBlock>().AddInlines(LineParser.ProcessLine(line));
            }
        }

        private static void ProcessHeader(MarkdownDocument markdownDocument, string line)
        {
            int level = 1;
            while (level < Math.Min(line.Length, 6) && line[level] == '#')
            {
                level++;
            }
            if (level < line.Length && line[level] == ' ')
            {
                markdownDocument.AddBlock<HeaderBlock>()
                    .SetHeaderLevel(level)
                    .AddInline<TextRunInline>()
                    .SetText(line.Substring(level));
            }
            else
            {
                markdownDocument
                    .AddBlock<ParagraphBlock>()
                    .AddInlines(LineParser.ProcessLine(line));
            }
        }

        
    }
}
