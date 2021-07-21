// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace Panosen.Markdown.Inlines
{
    /// <summary>
    /// Represents an embedded image.
    /// </summary>
    public class ImageInline : MarkdownInline
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageInline"/> class.
        /// </summary>
        public ImageInline()
            : base(MarkdownInlineType.Image)
        {
        }

        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the image Render URL.
        /// </summary>
        public string RenderUrl { get; set; }

        /// <summary>
        /// Gets or sets a text to display on hover.
        /// </summary>
        public string Tooltip { get; set; }

        /// <inheritdoc/>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ID of a reference, if this is a reference-style link.
        /// </summary>
        public string ReferenceId { get; set; }

        /// <summary>
        /// Gets image width
        /// If value is greater than 0, ImageStretch is set to UniformToFill
        /// If both ImageWidth and ImageHeight are greater than 0, ImageStretch is set to Fill
        /// </summary>
        public int ImageWidth { get; set; }

        /// <summary>
        /// Gets image height
        /// If value is greater than 0, ImageStretch is set to UniformToFill
        /// If both ImageWidth and ImageHeight are greater than 0, ImageStretch is set to Fill
        /// </summary>
        public int ImageHeight { get; set; }

        /// <summary>
        /// Converts the object into it's textual representation.
        /// </summary>
        /// <returns> The textual representation of this object. </returns>
        public override string ToString()
        {
            if (ImageWidth > 0 && ImageHeight > 0)
            {
                return string.Format("![{0}]: {1} (Width: {2}, Height: {3})", Tooltip, Url, ImageWidth, ImageHeight);
            }

            if (ImageWidth > 0)
            {
                return string.Format("![{0}]: {1} (Width: {2})", Tooltip, Url, ImageWidth);
            }

            if (ImageHeight > 0)
            {
                return string.Format("![{0}]: {1} (Height: {2})", Tooltip, Url, ImageHeight);
            }

            return string.Format("![{0}]: {1}", Tooltip, Url);
        }
    }
}