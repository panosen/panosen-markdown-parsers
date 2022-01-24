// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace Panosen.Markdown.Blocks
{
    /// <summary>
    /// Yaml Header. use for blog.
    /// e.g.
    /// ---
    /// title: something
    /// tag: something
    /// ---
    /// </summary>
    public class YamlHeaderBlock : MarkdownBlock
    {
        /// <summary>
        /// YamlHeader
        /// </summary>
        public override MarkdownBlockType Type => MarkdownBlockType.YamlHeader;

        /// <summary>
        /// Gets or sets yaml header properties
        /// </summary>
        public Dictionary<string, string> Children { get; set; }
    }
}
