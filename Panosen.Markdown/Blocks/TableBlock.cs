// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace Panosen.Markdown.Blocks
{
    /// <summary>
    /// Represents a block which contains tabular data.
    /// </summary>
    public class TableBlock : MarkdownBlock
    {
        /// <summary>
        /// Table
        /// </summary>
        public override MarkdownBlockType Type => MarkdownBlockType.Table;

        /// <summary>
        /// Gets or sets the table rows.
        /// </summary>
        public IList<TableRow> Rows { get; set; }

        /// <summary>
        /// Gets or sets describes the columns in the table.  Rows can have more or less cells than the number
        /// of columns.  Rows with fewer cells should be padded with empty cells.  For rows with
        /// more cells, the extra cells should be hidden.
        /// </summary>
        public IList<TableColumnDefinition> ColumnDefinitions { get; set; }
    }

    /// <summary>
    /// Describes a column in the markdown table.
    /// </summary>
    public class TableColumnDefinition
    {
        /// <summary>
        /// Gets or sets the alignment of content in a table column.
        /// </summary>
        public ColumnAlignment Alignment { get; set; }
    }

    /// <summary>
    /// Represents a single row in the table.
    /// </summary>
    public class TableRow
    {
        /// <summary>
        /// Gets or sets the table cells.
        /// </summary>
        public IList<TableCell> Cells { get; set; }
    }

    /// <summary>
    /// Represents a cell in the table.
    /// </summary>
    public class TableCell
    {
        /// <summary>
        /// Gets or sets the cell contents.
        /// </summary>
        public IList<MarkdownInline> Inlines { get; set; }
    }
}