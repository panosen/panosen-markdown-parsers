// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Microsoft.Toolkit.Parsers.Core;
using Panosen.Markdown.Inlines;
using Panosen.Markdown.Parsers.Helpers;

namespace Panosen.Markdown.Parsers.Inlines
{
    /// <summary>
    /// Represents an embedded image.
    /// </summary>
    public class ImageInlineParser
    {
        internal static void AddTripChars(List<InlineTripCharHelper> tripCharHelpers)
        {
            tripCharHelpers.Add(new InlineTripCharHelper() { FirstChar = '!', Method = InlineParseMethod.Image });
        }

        /// <summary>
        /// Attempts to parse an image e.g. "![Toolkit logo](https://raw.githubusercontent.com/windows-toolkit/WindowsCommunityToolkit/master/Microsoft.Toolkit.Uwp.SampleApp/Assets/ToolkitLogo.png)".
        /// </summary>
        /// <param name="markdown"> The markdown text. </param>
        /// <param name="start"> The location to start parsing. </param>
        /// <param name="end"> The location to stop parsing. </param>
        /// <returns> A parsed markdown image, or <c>null</c> if this is not a markdown image. </returns>
        internal static InlineParseResult Parse(string markdown, int start, int end)
        {
            // Expect a '!' character.
            if (start >= end || markdown[start] != '!')
            {
                return null;
            }

            int pos = start + 1;

            // Then a '[' character
            if (pos >= end || markdown[pos] != '[')
            {
                return null;
            }

            pos++;

            // Find the ']' character
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

            // Extract the alt.
            string tooltip = markdown.Substring(start + 2, pos - (start + 2));

            // Expect the '(' character.
            pos++;

            string reference = string.Empty;
            string url = string.Empty;
            int imageWidth = 0;
            int imageHeight = 0;

            if (pos < end && markdown[pos] == '[')
            {
                int refStart = pos;

                // Find the reference ']' character
                while (pos < end)
                {
                    if (markdown[pos] == ']')
                    {
                        break;
                    }

                    pos++;
                }

                reference = markdown.Substring(refStart + 1, pos - refStart - 1);
            }
            else if (pos < end && markdown[pos] == '(')
            {
                while (pos < end && ParseHelpers.IsMarkdownWhiteSpace(markdown[pos]))
                {
                    pos++;
                }

                // Extract the URL.
                int urlStart = pos;
                while (pos < end && markdown[pos] != ')')
                {
                    pos++;
                }

                var imageDimensionsPos = markdown.IndexOf(" =", urlStart, pos - urlStart, StringComparison.Ordinal);

                url = imageDimensionsPos > 0
                    ? TextRunInlineParser.ResolveEscapeSequences(markdown, urlStart + 1, imageDimensionsPos)
                    : TextRunInlineParser.ResolveEscapeSequences(markdown, urlStart + 1, pos);

                if (imageDimensionsPos > 0)
                {
                    // trying to find 'x' which separates image width and height
                    var dimensionsSeparatorPos = markdown.IndexOf("x", imageDimensionsPos + 2, pos - imageDimensionsPos - 1, StringComparison.Ordinal);

                    // didn't find separator, trying to parse value as imageWidth
                    if (dimensionsSeparatorPos == -1)
                    {
                        var imageWidthStr = markdown.Substring(imageDimensionsPos + 2, pos - imageDimensionsPos - 2);

                        int.TryParse(imageWidthStr, out imageWidth);
                    }
                    else
                    {
                        var dimensions = markdown.Substring(imageDimensionsPos + 2, pos - imageDimensionsPos - 2).Split('x');

                        // got width and height
                        if (dimensions.Length == 2)
                        {
                            int.TryParse(dimensions[0], out imageWidth);
                            int.TryParse(dimensions[1], out imageHeight);
                        }
                    }
                }
            }

            if (pos == end)
            {
                return null;
            }

            // We found something!
            var result = new ImageInline
            {
                Tooltip = tooltip,
                RenderUrl = url,
                ReferenceId = reference,
                Url = url,
                Text = markdown.Substring(start, pos + 1 - start),
                ImageWidth = imageWidth,
                ImageHeight = imageHeight
            };
            return new InlineParseResult(result, start, pos + 1);
        }
    }
}