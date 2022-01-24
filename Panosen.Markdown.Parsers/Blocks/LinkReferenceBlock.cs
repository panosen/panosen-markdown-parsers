// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.Toolkit.Parsers.Core;
using Panosen.Markdown.Blocks;
using Panosen.Markdown.Parser.Inlines;

namespace Panosen.Markdown.Parser.Blocks
{
    /// <summary>
    /// Represents the target of a reference ([ref][]).
    /// </summary>
    public class LinkReferenceBlockParser
    {
        /// <summary>
        /// Attempts to parse a reference e.g. "[example]: http://www.reddit.com 'title'".
        /// </summary>
        /// <param name="markdown"> The markdown text. </param>
        /// <param name="start"> The location to start parsing. </param>
        /// <param name="end"> The location to stop parsing. </param>
        /// <returns> A parsed markdown link, or <c>null</c> if this is not a markdown link. </returns>
        internal static LinkReferenceBlock Parse(string markdown, int start, int end)
        {
            // Expect a '[' character.
            if (start >= end || markdown[start] != '[')
            {
                return null;
            }

            // Find the ']' character
            int pos = start + 1;
            while (pos < end)
            {
                if (markdown[pos] == ']')
                {
                    break;
                }

                pos++;
            }

            if (pos == end)
            {
                return null;
            }

            // Extract the ID.
            string id = markdown.Substring(start + 1, pos - (start + 1));

            // Expect the ':' character.
            pos++;
            if (pos == end || markdown[pos] != ':')
            {
                return null;
            }

            // Skip whitespace
            pos++;
            while (pos < end && ParseHelpers.IsMarkdownWhiteSpace(markdown[pos]))
            {
                pos++;
            }

            if (pos == end)
            {
                return null;
            }

            // Extract the URL.
            int urlStart = pos;
            while (pos < end && !ParseHelpers.IsMarkdownWhiteSpace(markdown[pos]))
            {
                pos++;
            }

            string url = TextRunInlineParser.ResolveEscapeSequences(markdown, urlStart, pos);

            // Ignore leading '<' and trailing '>'.
            url = url.TrimStart('<').TrimEnd('>');

            // Skip whitespace.
            pos++;
            while (pos < end && ParseHelpers.IsMarkdownWhiteSpace(markdown[pos]))
            {
                pos++;
            }

            string tooltip = null;
            if (pos < end)
            {
                // Extract the tooltip.
                char tooltipEndChar;
                switch (markdown[pos])
                {
                    case '(':
                        tooltipEndChar = ')';
                        break;

                    case '"':
                    case '\'':
                        tooltipEndChar = markdown[pos];
                        break;

                    default:
                        return null;
                }

                pos++;
                int tooltipStart = pos;
                while (pos < end && markdown[pos] != tooltipEndChar)
                {
                    pos++;
                }

                if (pos == end)
                {
                    return null;    // No end character.
                }

                tooltip = markdown.Substring(tooltipStart, pos - tooltipStart);

                // Check there isn't any trailing text.
                pos++;
                while (pos < end && ParseHelpers.IsMarkdownWhiteSpace(markdown[pos]))
                {
                    pos++;
                }

                if (pos < end)
                {
                    return null;
                }
            }

            // We found something!
            var result = new LinkReferenceBlock();
            result.Id = id;
            result.Url = url;
            result.Tooltip = tooltip;
            return result;
        }
    }
}