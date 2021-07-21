// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using Panosen.Markdown.Inlines;
using Panosen.Markdown.Parsers.Helpers;

namespace Panosen.Markdown.Parsers.Inlines
{
    /// <summary>
    /// Represents a span containing code, or other text that is to be displayed using a
    /// fixed-width font.
    /// </summary>
    public class CodeInlineParser
    {
        /// <summary>
        /// Returns the chars that if found means we might have a match.
        /// </summary>
        internal static void AddTripChars(List<InlineTripCharHelper> tripCharHelpers)
        {
            tripCharHelpers.Add(new InlineTripCharHelper() { FirstChar = '`', Method = InlineParseMethod.Code });
        }

        /// <summary>
        /// Attempts to parse an inline code span.
        /// </summary>
        /// <param name="markdown"> The markdown text. </param>
        /// <param name="start"> The location to start parsing. </param>
        /// <param name="maxEnd"> The location to stop parsing. </param>
        /// <returns> A parsed inline code span, or <c>null</c> if this is not an inline code span. </returns>
        internal static InlineParseResult Parse(string markdown, int start, int maxEnd)
        {
            // Check the first char.
            if (start == maxEnd || markdown[start] != '`')
            {
                return null;
            }

            // There is an alternate syntax that starts and ends with two backticks.
            // e.g. ``sdf`sdf`` would be "sdf`sdf".
            int innerStart = start + 1;
            int innerEnd, end;
            if (innerStart < maxEnd && markdown[innerStart] == '`')
            {
                // Alternate double back-tick syntax.
                innerStart++;

                // Find the end of the span.
                innerEnd = Common.IndexOf(markdown, "``", innerStart, maxEnd);
                if (innerEnd == -1)
                {
                    return null;
                }

                end = innerEnd + 2;
            }
            else
            {
                // Standard single backtick syntax.

                // Find the end of the span.
                innerEnd = Common.IndexOf(markdown, '`', innerStart, maxEnd);
                if (innerEnd == -1)
                {
                    return null;
                }

                end = innerEnd + 1;
            }

            // The span must contain at least one character.
            if (innerStart == innerEnd)
            {
                return null;
            }

            // We found something!
            var result = new CodeInline();
            result.Text = markdown.Substring(innerStart, innerEnd - innerStart).Trim(' ', '\t', '\r', '\n');
            return new InlineParseResult(result, start, end);
        }
    }
}