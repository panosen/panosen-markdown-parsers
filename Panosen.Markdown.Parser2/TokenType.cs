using System;
using System.Collections.Generic;
using System.Text;

namespace Panosen.Markdown.Parser2
{
    public enum TokenType
    {
        /// <summary>
        /// 数字
        /// </summary>
        Number,

        /// <summary>
        /// #
        /// </summary>
        Hash,

        /// <summary>
        /// &gt;
        /// </summary>
        GreaterThan,

        /// <summary>
        /// 点
        /// </summary>
        Dot,

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
        Plain,

        /// <summary>
        /// 换行符 '\n'
        /// </summary>
        NextLine
    }
}
