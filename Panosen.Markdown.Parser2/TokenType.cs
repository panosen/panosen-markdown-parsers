using Panosen.Compling;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panosen.Markdown.Parser2
{
    public class Symbols
    {
        /// <summary>
        /// 数字1
        /// </summary>
        public Symbol Number1 => new Symbol { Type = SymbolType.Terminal, Value = "1" };

        /// <summary>
        /// 数字2
        /// </summary>
        public Symbol Number2 => new Symbol { Type = SymbolType.Terminal, Value = "2" };

        /// <summary>
        /// #
        /// </summary>
        public Symbol Hash => new Symbol { Type = SymbolType.Terminal, Value = "#" };

        /// <summary>
        /// &gt;
        /// </summary>
        public Symbol GreaterThan => new Symbol { Type = SymbolType.Terminal, Value = ">" };

        /// <summary>
        /// 点
        /// </summary>
        public Symbol Dot => new Symbol { Type = SymbolType.Terminal, Value = "." };

        /// <summary>
        /// 左 中括号
        /// </summary>
        public Symbol LeftMiddleBracket => new Symbol { Type = SymbolType.Terminal, Value = "[" };

        /// <summary>
        /// 右中括号
        /// </summary>
        public Symbol RightMiddleBracket => new Symbol { Type = SymbolType.Terminal, Value = "]" };

        /// <summary>
        /// 左小括号
        /// </summary>
        public Symbol LeftSmallBracktet => new Symbol { Type = SymbolType.Terminal, Value = "(" };

        /// <summary>
        /// 右小括号
        /// </summary>
        public Symbol RightSmallBracket => new Symbol { Type = SymbolType.Terminal, Value = ")" };

        /// <summary>
        /// 感叹号
        /// </summary>
        public Symbol Excalmatory => new Symbol { Type = SymbolType.Terminal, Value = "!" };

        /// <summary>
        /// 星号
        /// </summary>
        public Symbol Star => new Symbol { Type = SymbolType.Terminal, Value = "*" };

        /// <summary>
        /// 文本
        /// </summary>
        public Symbol Plain => new Symbol { Type = SymbolType.Terminal, Value = "text" };

        /// <summary>
        /// 换行符 '\n'
        /// </summary>
        public Symbol NextLine => new Symbol { Type = SymbolType.Terminal, Value = "\n" };
    }
}
