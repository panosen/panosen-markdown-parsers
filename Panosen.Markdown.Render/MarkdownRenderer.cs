// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Panosen.CodeDom;
using Panosen.CodeDom.Tag;
using Panosen.CodeDom.Tag.Core;
using Panosen.CodeDom.Tag.Html;
using Panosen.Markdown.Blocks;
using Panosen.Markdown.Helper;
using Panosen.Markdown.Inlines;

namespace Panosen.Markdown.Parsers.Render
{
    /// <summary>
    /// A base renderer for Rendering Markdown into Controls.
    /// </summary>
    public class MarkdownRenderer
    {
        public string Transform(MarkdownDocument document)
        {
            var components = new ComponentCollection();

            Render(document, components);

            var builder = new StringBuilder();

            new TagEngineCore().Generate(components.Components, new StringWriter(builder));

            return builder.ToString();
        }

        /// <summary>
        /// Renders all Content to the Provided Parent UI.
        /// </summary>
        private void Render(MarkdownDocument document, ComponentCollection components)
        {
            foreach (MarkdownBlock element in document.Blocks)
            {
                var component = RenderBlock(element);
                if (component == null)
                {
                    continue;
                }
                components.AddComponent(component);
            }
        }

        /// <summary>
        /// Called to render a block element.
        /// </summary>
        protected Component RenderBlock(MarkdownBlock block)
        {
            switch (block.Type)
            {

                case MarkdownBlockType.Root:

                case MarkdownBlockType.Paragraph:
                    return RenderParagraph((ParagraphBlock)block);

                case MarkdownBlockType.Quote:
                    return RenderQuote((QuoteBlock)block);

                case MarkdownBlockType.Code:
                    return RenderCode((CodeBlock)block);

                case MarkdownBlockType.Header:
                    return RenderHeader((HeaderBlock)block);

                case MarkdownBlockType.List:
                    return RenderListElement((ListBlock)block);

                case MarkdownBlockType.ListItemBuilder:
                    return null;

                case MarkdownBlockType.HorizontalRule:
                    return RenderHorizontalRule();

                case MarkdownBlockType.Table:
                    return RenderTable((TableBlock)block);

                case MarkdownBlockType.LinkReference:
                    return null;

                case MarkdownBlockType.YamlHeader:
                    return RenderYamlHeader((YamlHeaderBlock)block);

                default:
                case MarkdownBlockType.None:
                    return null;
            }
        }

        /// <summary>
        /// Renders all of the children for the given element.
        /// </summary>
        protected void RenderInlineChildren(IList<MarkdownInline> inlineElements, CodeWriter codeWriter)
        {
            foreach (MarkdownInline element in inlineElements)
            {
                switch (element.Type)
                {
                    case MarkdownInlineType.Comment:
                    case MarkdownInlineType.LinkReference:
                        break;

                    default:
                        RenderInline(element, codeWriter);
                        break;
                }
            }
        }

        /// <summary>
        /// Called to render an inline element.
        /// </summary>
        protected void RenderInline(MarkdownInline element, CodeWriter codeWriter)
        {
            switch (element.Type)
            {
                case MarkdownInlineType.TextRun:
                    RenderTextRun((TextRunInline)element, codeWriter);
                    break;

                case MarkdownInlineType.Italic:
                    RenderItalicRun((ItalicTextInline)element, codeWriter);
                    break;

                case MarkdownInlineType.Bold:
                    RenderBoldRun((BoldTextInline)element, codeWriter);
                    break;

                case MarkdownInlineType.MarkdownLink:
                    CheckRenderMarkdownLink((MarkdownLinkInline)element, codeWriter);
                    break;

                case MarkdownInlineType.RawHyperlink:
                    RenderHyperlink((HyperlinkInline)element, codeWriter);
                    break;

                case MarkdownInlineType.Strikethrough:
                    RenderStrikethroughRun((StrikethroughTextInline)element, codeWriter);
                    break;

                case MarkdownInlineType.Superscript:
                    RenderSuperscriptRun((SuperscriptTextInline)element, codeWriter);
                    break;

                case MarkdownInlineType.Subscript:
                    RenderSubscriptRun((SubscriptTextInline)element, codeWriter);
                    break;

                case MarkdownInlineType.Code:
                    RenderCodeRun((CodeInline)element, codeWriter);
                    break;

                case MarkdownInlineType.Image:
                    RenderImage((ImageInline)element, codeWriter);
                    break;

                case MarkdownInlineType.Emoji:
                    RenderEmoji((EmojiInline)element, codeWriter);
                    break;
            }
        }

        /// <summary>
        /// Verifies if the link is valid, before processing into a link, or plain text.
        /// </summary>
        protected void CheckRenderMarkdownLink(MarkdownLinkInline element, CodeWriter codeWriter)
        {
            // Avoid processing when link text is empty.
            if (element.Inlines.Count == 0)
            {
                return;
            }

            if (element.Url == null)
            {
                // The element couldn't be resolved, just render it as text.
                RenderInlineChildren(element.Inlines, codeWriter);
                return;
            }

            foreach (MarkdownInline inline in element.Inlines)
            {
                if (inline is ImageInline imageInline)
                {
                    imageInline.Url = element.Url;
                    RenderImage(imageInline, codeWriter);
                    return;
                }
            }

            RenderMarkdownLink(element, codeWriter);
        }

        #region Inlines

        /// <summary>
        /// Renders emoji element.
        /// </summary>
        protected virtual void RenderEmoji(EmojiInline element, CodeWriter codeWriter) { }

        /// <summary>
        /// Renders a text run element.
        /// </summary>
        protected virtual void RenderTextRun(TextRunInline textRun, CodeWriter codeWriter)
        {
            codeWriter.Write(textRun.Text);
        }

        /// <summary>
        /// Renders a bold run element.
        /// </summary>
        protected virtual void RenderBoldRun(BoldTextInline boldText, CodeWriter codeWriter)
        {
            codeWriter.Write(Marks.LESS_THAN).Write(HtmlTagNames.B).Write(Marks.GREATER_THAN);

            RenderInlineChildren(boldText.Inlines, codeWriter);

            codeWriter.Write(Marks.LESS_THAN).Write(Marks.SLASH).Write(HtmlTagNames.B).Write(Marks.GREATER_THAN);
        }

        /// <summary>
        /// Renders a link element
        /// </summary>
        protected virtual void RenderMarkdownLink(MarkdownLinkInline markdownLink, CodeWriter codeWriter)
        {
            codeWriter.Write(Marks.LESS_THAN).Write(HtmlTagNames.A);

            if (!string.IsNullOrEmpty(markdownLink.Url))
            {
                codeWriter.Write(Marks.WHITESPACE).Write("href").Write(Marks.EQUAL).Write(Marks.DOUBLE_QUOTATION).Write(markdownLink.Url).Write(Marks.DOUBLE_QUOTATION);
            }

            codeWriter.Write(Marks.GREATER_THAN);

            RenderInlineChildren(markdownLink.Inlines, codeWriter);

            codeWriter.Write(Marks.LESS_THAN).Write(Marks.SLASH).Write(HtmlTagNames.A).Write(Marks.GREATER_THAN);
        }

        /// <summary>
        /// Renders an image element.
        /// </summary>
        protected virtual void RenderImage(ImageInline image, CodeWriter codeWriter)
        {
            codeWriter.Write(Marks.LESS_THAN).Write(HtmlTagNames.Img);

            if (image.Url != null)
            {
                codeWriter.Write(Marks.WHITESPACE).Write("src").Write(Marks.EQUAL).Write(Marks.DOUBLE_QUOTATION).Write(image.Url).Write(Marks.DOUBLE_QUOTATION);
            }

            if (image.Text != null)
            {
                codeWriter.Write(Marks.WHITESPACE).Write("alt").Write(Marks.EQUAL).Write(Marks.DOUBLE_QUOTATION).Write(image.Text).Write(Marks.DOUBLE_QUOTATION);
            }

            codeWriter.Write(Marks.WHITESPACE).Write(Marks.SLASH).Write(Marks.GREATER_THAN);
        }

        /// <summary>
        /// Renders a raw link element.
        /// </summary>
        protected virtual void RenderHyperlink(HyperlinkInline hyperlink, CodeWriter codeWriter)
        {
            codeWriter.Write(Marks.LESS_THAN).Write(HtmlTagNames.A);

            if (!string.IsNullOrEmpty(hyperlink.Url))
            {
                codeWriter.Write(Marks.WHITESPACE).Write("href").Write(Marks.EQUAL).Write(Marks.DOUBLE_QUOTATION).Write(hyperlink.Url).Write(Marks.DOUBLE_QUOTATION);
            }

            codeWriter.Write(Marks.GREATER_THAN);

            codeWriter.Write(hyperlink.Text);

            codeWriter.Write(Marks.LESS_THAN).Write(Marks.SLASH).Write(HtmlTagNames.A).Write(Marks.GREATER_THAN);
        }

        /// <summary>
        /// Renders a text run element.
        /// </summary>
        protected virtual void RenderItalicRun(ItalicTextInline italicText, CodeWriter codeWriter)
        {
            codeWriter.Write(Marks.LESS_THAN).Write(HtmlTagNames.I).Write(Marks.GREATER_THAN);

            RenderInlineChildren(italicText.Inlines, codeWriter);

            codeWriter.Write(Marks.LESS_THAN).Write(Marks.SLASH).Write(HtmlTagNames.I).Write(Marks.GREATER_THAN);
        }

        /// <summary>
        /// Renders a strikethrough element.
        /// </summary>
        protected virtual void RenderStrikethroughRun(StrikethroughTextInline strikethroughText, CodeWriter codeWriter)
        {
            codeWriter.Write(Marks.LESS_THAN).Write(HtmlTagNames.Strong).Write(Marks.GREATER_THAN);

            RenderInlineChildren(strikethroughText.Inlines, codeWriter);

            codeWriter.Write(Marks.LESS_THAN).Write(Marks.SLASH).Write(HtmlTagNames.Strong).Write(Marks.GREATER_THAN);
        }

        /// <summary>
        /// Renders a superscript element.
        /// </summary>
        protected virtual void RenderSuperscriptRun(SuperscriptTextInline element, CodeWriter codeWriter) { }

        /// <summary>
        /// Renders a subscript element.
        /// </summary>
        protected virtual void RenderSubscriptRun(SubscriptTextInline element, CodeWriter codeWriter) { }

        /// <summary>
        /// Renders a code element
        /// </summary>
        protected virtual void RenderCodeRun(CodeInline code, CodeWriter codeWriter)
        {
            codeWriter.Write(Marks.LESS_THAN).Write(HtmlTagNames.Code).Write(Marks.GREATER_THAN);

            codeWriter.Write(code.Text);

            codeWriter.Write(Marks.LESS_THAN).Write(Marks.SLASH).Write(HtmlTagNames.Code).Write(Marks.GREATER_THAN);
        }

        #endregion

        #region Blocks

        /// <summary>
        /// Renders a paragraph element.
        /// </summary>
        protected virtual Component RenderParagraph(ParagraphBlock paragraph)
        {
            var component = new PComponent();

            var builder = new StringBuilder();

            RenderInlineChildren(paragraph.Inlines, new StringWriter(builder));

            component.SetContent(builder.ToString());

            return component;
        }

        /// <summary>
        /// Renders a yaml header element.
        /// </summary>
        protected virtual Component RenderYamlHeader(YamlHeaderBlock element)
        {
            return null;
        }

        /// <summary>
        /// Renders a header element.
        /// </summary>
        protected virtual Component RenderHeader(HeaderBlock header)
        {
            var component = new HComponent(header.HeaderLevel);

            var builder = new StringBuilder();

            RenderInlineChildren(header.Inlines, new StringWriter(builder));

            component.SetContent(builder.ToString());

            return component;
        }

        /// <summary>
        /// Renders a list element.
        /// </summary>
        protected virtual Component RenderListElement(ListBlock list)
        {
            HtmlComponent component;
            switch (list.Style)
            {
                case ListStyle.Bulleted:
                    component = new UlComponent();
                    break;
                case ListStyle.Numbered:
                    component = new OlComponent();
                    break;
                default:
                    return null;
            }

            foreach (var item in list.Items)
            {
                var li = component.AddChild<LiComponent>();

                foreach (var block in item.Blocks)
                {
                    var temp = RenderBlock(block);
                    if (temp == null)
                    {
                        continue;
                    }

                    li.AddChild(temp);
                }
            }

            return component;
        }

        /// <summary>
        /// Renders a horizontal rule element.
        /// </summary>
        protected virtual Component RenderHorizontalRule()
        {
            return new HrComponent();
        }

        /// <summary>
        /// Renders a quote element.
        /// </summary>
        protected virtual Component RenderQuote(QuoteBlock quote)
        {
            var component = new BlockquoteComponent();
            if (quote.Blocks == null || quote.Blocks.Count == 0)
            {
                return component;
            }

            foreach (var block in quote.Blocks)
            {
                var temp = RenderBlock(block);
                if (temp == null)
                {
                    continue;
                }

                component.AddChild(temp);
            }

            return component;
        }

        /// <summary>
        /// Renders a code element.
        /// </summary>
        protected virtual Component RenderCode(CodeBlock code)
        {
            var component = new PreComponent();

            var codeComponent = component.AddChild<CodeComponent>();

            codeComponent.SetContent(code.Text);

            return component;
        }

        /// <summary>
        /// Renders a table element.
        /// </summary>
        protected virtual Component RenderTable(TableBlock table)
        {
            var tableComponent = new TableComponent();

            var theadComponent = tableComponent.AddChild<TheadComponent>();
            {
                var row = table.Rows[0];

                if (row.Cells != null && row.Cells.Count > 0)
                {
                    var tr = theadComponent.AddChild<TrComponent>();
                    foreach (var cell in row.Cells)
                    {
                        var builder = new StringBuilder();
                        RenderInlineChildren(cell.Inlines, new StringWriter(builder));
                        tr.AddChild<ThComponent>().SetContent(builder.ToString());
                    }
                }
            }

            var tbodyComponent = tableComponent.AddChild<TbodyComponent>();
            for (int i = 1; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];

                if (row.Cells != null && row.Cells.Count > 0)
                {
                    var tr = tbodyComponent.AddChild<TrComponent>();
                    foreach (var cell in row.Cells)
                    {
                        var builder = new StringBuilder();
                        RenderInlineChildren(cell.Inlines, new StringWriter(builder));
                        tr.AddChild<TdComponent>().SetContent(builder.ToString());
                    }
                }
            }

            return tableComponent;
        }

        #endregion
    }
}