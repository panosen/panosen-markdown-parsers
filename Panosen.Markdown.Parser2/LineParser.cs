using Panosen.Markdown.Blocks;
using Panosen.Markdown.Inlines;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panosen.Markdown.Parser2
{
    public class LineParser
    {

        public static MarkdownInlineCollection ProcessLine(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                return null;
            }

            var tokenList = Tokenizer.GetTokenList(line);
            if (tokenList == null || tokenList.Count <= 0)
            {
                return null;
            }

            MarkdownInlineCollection markdownInlineCollection = new MarkdownInlineCollection();

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
                                    markdownInlineCollection.AddInline<TextRunInline>().SetText(tokenList[index].Text);
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
                                markdownInlineCollection.AddInline<TextRunInline>().SetText("!");
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
                                    markdownInlineCollection.AddInline<TextRunInline>().SetText(tempTokenList[i].Text);
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
                                    markdownInlineCollection.AddInline<TextRunInline>().SetText(tempTokenList[i].Text);
                                }
                                tempTokenList.Clear();
                                imageTextBuilder.Clear();
                                markdownInlineCollection.AddInline<TextRunInline>().SetText(")");
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
                                var imageInline = markdownInlineCollection.AddInline<ImageInline>();
                                imageInline.Text = imageTextBuilder.ToString();
                                imageInline.Url = imageUrlBuilder.ToString();
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
                                markdownInlineCollection.AddInline<TextRunInline>().SetText("[");
                                markdownInlineCollection.AddInline<TextRunInline>().SetText("]");
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
                                    markdownInlineCollection.AddInline<TextRunInline>().SetText(tempTokenList[index].Text);
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
                                markdownInlineCollection.AddInline<TextRunInline>().SetText(linkTextBuilder.ToString());
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
                                var hyperlinkInline = markdownInlineCollection.AddInline<HyperlinkInline>();
                                hyperlinkInline.Text = linkTextBuilder.ToString();
                                hyperlinkInline.Url = linkUrlBuilder.ToString();
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

            return markdownInlineCollection;
        }

    }
}
