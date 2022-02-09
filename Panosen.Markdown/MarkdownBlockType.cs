// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Panosen.Markdown
{
    /// <summary>
    /// Determines the type of Block the Block element is.
    /// </summary>
    public enum MarkdownBlockType
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// The root element
        /// </summary>
        Root = 101,

        /// <summary>
        /// A paragraph element.
        /// </summary>
        Paragraph = 102,

        /// <summary>
        /// A quote block
        /// </summary>
        Quote = 103,

        /// <summary>
        /// A code block
        /// </summary>
        Code = 104,

        /// <summary>
        /// A header block
        /// </summary>
        Header = 105,

        /// <summary>
        /// A list block
        /// </summary>
        List = 106,

        /// <summary>
        /// A list item block
        /// </summary>
        ListItemBuilder = 107,

        /// <summary>
        /// a horizontal rule block
        /// </summary>
        HorizontalRule = 108,

        /// <summary>
        /// A table block
        /// </summary>
        Table = 109,

        /// <summary>
        /// A link block
        /// </summary>
        LinkReference = 110,

        /// <summary>
        /// A Yaml header block
        /// </summary>
        YamlHeader = 111
    }
}