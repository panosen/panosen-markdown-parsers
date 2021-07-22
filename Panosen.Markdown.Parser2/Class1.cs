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
                                .AddInlines(ProcessLine(line.Substring(1)));
                        }
                        break;
                    case '*':
                        {
                            ProcessStar(markdownDocument, line);
                        }
                        break;
                    default:
                        {
                            markdownDocument.AddBlock<ParagraphBlock>().AddInlines(ProcessLine(line));
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

                var inlines = ProcessLine(line.Substring(index + 2));

                //ParagraphBlock paragraphBlock = new ParagraphBlock();
                //paragraphBlock.Inlines = inlines;
                //ol.Items.Add(paragraphBlock);
            }
            else
            {
                ParagraphBlock paragraphBlock = new ParagraphBlock();
                paragraphBlock.Inlines = ProcessLine(line);
                markdownDocument.AddBlock(paragraphBlock);
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

                var inlines = ProcessLine(line.Substring(index + 2));

                //ParagraphBlock paragraphBlock = new ParagraphBlock();
                //paragraphBlock.Inlines = inlines;
                //ol.Items.Add(paragraphBlock);
            }
            else
            {
                ParagraphBlock paragraphBlock = new ParagraphBlock();
                paragraphBlock.Inlines = ProcessLine(line);
                markdownDocument.AddBlock(paragraphBlock);
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
                    .AddInlines(ProcessLine(line));
            }
        }

        #region 分析一个句子
        private static List<MarkdownInline> ProcessLine(string line)
        {
            List<MarkdownInline> markdownInlineList = new List<MarkdownInline>();
            if (string.IsNullOrEmpty(line))
            {
                return markdownInlineList;
            }

            var tokenList = GetTokenList(line);
            if (tokenList == null || tokenList.Count <= 0)
            {
                return markdownInlineList;
            }

            Stack<Status> statusStack = new Stack<Status>();
            List<Token> tempTokenList = new List<Token>();

            StringBuilder imageTextBuilder = new StringBuilder();
            StringBuilder imageUrlBuilder = new StringBuilder();
            StringBuilder linkTextBuilder = new StringBuilder();
            StringBuilder linkUrlBuilder = new StringBuilder();

            StringBuilder boldBuilder = new StringBuilder();
            StringBuilder italicBuilder = new StringBuilder();

            statusStack.Push(Status.Empty);

            var index = 0;
            while (index < tokenList.Count)
            {
                switch (statusStack.Peek())
                {
                    case Status.Empty:
                        {
                            switch (tokenList[index].TokenType)
                            {
                                case TokenType.LeftMiddleBracket:
                                    {
                                        statusStack.Push(Status.Link1);
                                        tempTokenList.Add(tokenList[index]);
                                    }
                                    break;
                                case TokenType.Excalmatory:
                                    {
                                        statusStack.Push(Status.Image1);
                                        tempTokenList.Add(tokenList[index]);
                                    }
                                    break;
                                case TokenType.Plain:
                                case TokenType.RightMiddleBracket:
                                case TokenType.LeftSmallBracktet:
                                case TokenType.RightSmallBracket:
                                default:
                                    markdownInlineList.Add(new TextRunInline { Text = tokenList[index].Text });
                                    break;
                            }
                        }
                        break;
                    case Status.Image1:
                        {
                            if (tokenList[index].TokenType == TokenType.LeftMiddleBracket)
                            {
                                statusStack.Pop();
                                statusStack.Push(Status.Image2);
                                tempTokenList.Add(tokenList[index]);
                            }
                            else
                            {
                                statusStack.Pop();
                                tempTokenList.RemoveAt(0);
                                markdownInlineList.Add(new TextRunInline { Text = "!" });
                            }
                        }
                        break;
                    case Status.Image2:
                        {
                            if (tokenList[index].TokenType == TokenType.RightMiddleBracket)
                            {
                                statusStack.Pop();
                                statusStack.Push(Status.Image4);
                                tempTokenList.Add(tokenList[index]);
                            }
                            else
                            {
                                statusStack.Pop();
                                statusStack.Push(Status.Image3);
                                tempTokenList.Add(tokenList[index]);
                                imageTextBuilder.Append(tokenList[index].Text);
                            }
                        }
                        break;
                    case Status.Image3:
                        {
                            if (tokenList[index].TokenType == TokenType.RightMiddleBracket)
                            {
                                statusStack.Pop();
                                statusStack.Push(Status.Image4);
                                tempTokenList.Add(tokenList[index]);
                            }
                            else
                            {
                                tempTokenList.Add(tokenList[index]);
                                imageTextBuilder.Append(tokenList[index].Text);
                            }
                        }
                        break;
                    case Status.Image4:
                        {
                            if (tokenList[index].TokenType == TokenType.LeftSmallBracktet)
                            {
                                statusStack.Pop();
                                statusStack.Push(Status.Image5);
                                tempTokenList.Add(tokenList[index]);
                            }
                            else
                            {
                                statusStack.Pop();
                                for (int i = 0; i < tempTokenList.Count; i++)
                                {
                                    markdownInlineList.Add(new TextRunInline { Text = tempTokenList[i].Text });
                                }
                                tempTokenList.Clear();
                                imageTextBuilder.Clear();
                                index--;//将index-1，重新再判断状态
                            }
                        }
                        break;
                    case Status.Image5:
                        {
                            if (tokenList[index].TokenType == TokenType.RightSmallBracket)
                            {
                                statusStack.Pop();
                                for (int i = 0; i < tempTokenList.Count; i++)
                                {
                                    markdownInlineList.Add(new TextRunInline { Text = tempTokenList[i].Text });
                                }
                                tempTokenList.Clear();
                                imageTextBuilder.Clear();
                                markdownInlineList.Add(new TextRunInline { Text = ")" });
                            }
                            else
                            {
                                statusStack.Pop();
                                statusStack.Push(Status.Image6);
                                tempTokenList.Add(tokenList[index]);
                                imageUrlBuilder.Append(tokenList[index].Text);
                            }
                        }
                        break;
                    case Status.Image6:
                        {
                            if (tokenList[index].TokenType == TokenType.RightSmallBracket)
                            {
                                statusStack.Pop();
                                markdownInlineList.Add(new ImageInline { Text = imageTextBuilder.ToString(), Url = imageUrlBuilder.ToString() });
                                imageTextBuilder.Clear();
                                imageUrlBuilder.Clear();
                            }
                            else
                            {
                                tempTokenList.Add(tokenList[index]);
                                imageUrlBuilder.Append(tokenList[index].Text);
                            }
                        }
                        break;
                    case Status.Link1:
                        {
                            if (tokenList[index].TokenType == TokenType.RightMiddleBracket)
                            {
                                statusStack.Pop();
                                markdownInlineList.Add(new TextRunInline { Text = "[" });
                                markdownInlineList.Add(new TextRunInline { Text = "]" });
                            }
                            else
                            {
                                statusStack.Pop();
                                statusStack.Push(Status.Link2);
                                tempTokenList.Add(tokenList[index]);
                                linkTextBuilder.Append(tokenList[index].Text);
                            }
                        }
                        break;
                    case Status.Link2:
                        if (tokenList[index].TokenType == TokenType.RightMiddleBracket)
                        {
                            statusStack.Pop();
                            statusStack.Push(Status.Link3);
                            tempTokenList.Add(tokenList[index]);
                        }
                        else
                        {
                            tempTokenList.Add(tokenList[index]);
                            linkTextBuilder.Append(tokenList[index].Text);
                        }
                        break;
                    case Status.Link3:
                        {
                            if (tokenList[index].TokenType == TokenType.LeftSmallBracktet)
                            {
                                statusStack.Pop();
                                statusStack.Push(Status.Link4);
                                tempTokenList.Add(tokenList[index]);
                            }
                            else
                            {
                                for (int i = 0; i < tempTokenList.Count; i++)
                                {
                                    markdownInlineList.Add(new TextRunInline { Text = tempTokenList[index].Text });
                                }
                                linkTextBuilder.Clear();
                                tempTokenList.Clear();
                                index--;//将index-1，重新再判断状态
                            }
                        }
                        break;
                    case Status.Link4:
                        {
                            if (tokenList[index].TokenType == TokenType.RightSmallBracket)
                            {
                                markdownInlineList.Add(new TextRunInline { Text = linkTextBuilder.ToString() });
                                tempTokenList.Clear();
                                linkTextBuilder.Clear();
                            }
                            else
                            {
                                statusStack.Pop();
                                statusStack.Push(Status.Link5);
                                tempTokenList.Add(tokenList[index]);
                                linkUrlBuilder.Append(tokenList[index].Text);
                            }
                        }
                        break;
                    case Status.Link5:
                        {
                            if (tokenList[index].TokenType == TokenType.RightSmallBracket)
                            {
                                markdownInlineList.Add(new HyperlinkInline { Text = linkTextBuilder.ToString(), Url = linkUrlBuilder.ToString() });
                            }
                            else
                            {
                                tempTokenList.Add(tokenList[index]);
                                linkUrlBuilder.Append(tokenList[index].Text);
                            }
                        }
                        break;
                    case Status.Star1:
                        {
                            if (tokenList[index].TokenType == TokenType.Star)
                            {
                                statusStack.Push(Status.Star2);
                            }
                            else
                            {
                                tempTokenList.Add(tokenList[index]);
                                italicBuilder.Append(tokenList[index].Text);
                            }
                        }
                        break;
                    case Status.Star2:
                        {
                            if (tokenList[index].TokenType == TokenType.Star)
                            {
                                statusStack.Push(Status.Star3);
                            }
                            else
                            {

                            }
                        }
                        break;
                    case Status.Star3:
                        {
                            if (tokenList[index].TokenType == TokenType.Star)
                            {
                                statusStack.Push(Status.Star3);
                            }
                            else
                            {

                            }
                        }
                        break;
                    case Status.Star4:
                        {
                            if (tokenList[index].TokenType == TokenType.Star)
                            {
                                statusStack.Push(Status.Star3);
                            }
                            else
                            {

                            }
                        }
                        break;
                    default:
                        break;
                }

                index++;
            }

            return markdownInlineList;
        }

        internal enum Status
        {
            Empty,

            /// <summary>
            /// 感叹号"!"
            /// </summary>
            Image1,
            /// <summary>
            /// 左中括号"["
            /// </summary>
            Image2,
            /// <summary>
            /// 中括号内文字
            /// </summary>
            Image3,
            /// <summary>
            /// 右中括号"]"
            /// </summary>
            Image4,
            /// <summary>
            /// 左小括号"("
            /// </summary>
            Image5,
            /// <summary>
            /// 小括号内文字
            /// </summary>
            Image6,

            /// <summary>
            /// 左中括号"["
            /// </summary>
            Link1,
            /// <summary>
            /// 中括号内文字
            /// </summary>
            Link2,
            /// <summary>
            /// 右中括号"]"
            /// </summary>
            Link3,
            /// <summary>
            /// 左小括号"("
            /// </summary>
            Link4,
            /// <summary>
            /// 小括号内文字
            /// </summary>
            Link5,

            /// <summary>
            /// 粗体第一个*
            /// </summary>
            Star1,
            /// <summary>
            /// 粗体第二个*
            /// </summary>
            Star2,
            /// <summary>
            /// 粗体第三个*
            /// </summary>
            Star3,
            /// <summary>
            /// 粗体第四个*
            /// </summary>
            Star4
        }

        internal enum TokenType
        {
            /// <summary>
            /// 左 中括号
            /// </summary>
            LeftMiddleBracket,

            /// <summary>
            /// 右中括号
            /// </summary>
            RightMiddleBracket,

            /// <summary>
            /// 左小括号
            /// </summary>
            LeftSmallBracktet,

            /// <summary>
            /// 右小括号
            /// </summary>
            RightSmallBracket,

            /// <summary>
            /// 感叹号
            /// </summary>
            Excalmatory,

            /// <summary>
            /// 星号
            /// </summary>
            Star,

            /// <summary>
            /// 文本
            /// </summary>
            Plain
        }

        internal struct Token
        {
            public TokenType TokenType { get; set; }

            public string Text { get; set; }
        }

        private static List<Token> GetTokenList(string text)
        {
            List<Token> tokens = new List<Token>();
            if (string.IsNullOrEmpty(text))
            {
                return tokens;
            }

            StringBuilder current = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                switch (text[i])
                {
                    case '!':
                        ProcessToken(tokens, current);
                        tokens.Add(new Token { Text = text[i].ToString(), TokenType = TokenType.Excalmatory });

                        break;
                    case '[':
                        ProcessToken(tokens, current);
                        tokens.Add(new Token { Text = text[i].ToString(), TokenType = TokenType.LeftMiddleBracket });

                        break;
                    case ']':
                        ProcessToken(tokens, current);
                        tokens.Add(new Token { Text = text[i].ToString(), TokenType = TokenType.RightMiddleBracket });

                        break;
                    case '(':
                        ProcessToken(tokens, current);
                        tokens.Add(new Token { Text = text[i].ToString(), TokenType = TokenType.LeftSmallBracktet });

                        break;
                    case ')':
                        ProcessToken(tokens, current);
                        tokens.Add(new Token { Text = text[i].ToString(), TokenType = TokenType.RightSmallBracket });

                        break;
                    case '*':
                        ProcessToken(tokens, current);
                        tokens.Add(new Token { Text = text[i].ToString(), TokenType = TokenType.Star });

                        break;
                    default:
                        current.Append(text[i]);

                        break;
                }
            }
            if (current.Length > 0)
            {
                tokens.Add(new Token { Text = current.ToString(), TokenType = TokenType.Plain });
            }

            return tokens;
        }

        private static void ProcessToken(List<Token> tokens, StringBuilder current)
        {
            if (current.Length == 0)
            {
                return;
            }

            tokens.Add(new Token { Text = current.ToString(), TokenType = TokenType.Plain });
            current.Clear();
        }
        #endregion
    }
}
