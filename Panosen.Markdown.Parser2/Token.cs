using System;
using System.Collections.Generic;
using System.Text;

namespace Panosen.Markdown.Parser2
{
    public abstract class Token
    {
        public abstract TokenType TokenType { get; }

        public string Text { get; set; }
    }

    /// <summary>
    /// 数字
    /// </summary>
    public class NumberToken : Token
    {
        public override TokenType TokenType => TokenType.Number;
    }

    /// <summary>
    /// 点
    /// </summary>
    public class HashToken : Token
    {
        public override TokenType TokenType => TokenType.Hash;
    }

    /// <summary>
    /// 大于
    /// </summary>
    public class GreaterThanToken : Token
    {
        public override TokenType TokenType => TokenType.GreaterThan;
    }

    /// <summary>
    /// 点
    /// </summary>
    public class DotToken : Token
    {
        public override TokenType TokenType => TokenType.Dot;
    }

    /// <summary>
    /// 左中括号
    /// </summary>
    public class LeftMiddleBracketToken : Token
    {
        public override TokenType TokenType => TokenType.LeftMiddleBracket;
    }

    /// <summary>
    /// 右中括号
    /// </summary>
    public class RightMiddleBracketToken : Token
    {
        public override TokenType TokenType => TokenType.RightMiddleBracket;
    }

    /// <summary>
    /// 左小括号
    /// </summary>
    public class LeftSmallBracktetToken : Token
    {
        public override TokenType TokenType => TokenType.LeftSmallBracktet;
    }

    /// <summary>
    /// 右小括号
    /// </summary>
    public class RightSmallBracketToken : Token
    {
        public override TokenType TokenType => TokenType.RightSmallBracket;
    }

    /// <summary>
    /// 感叹号
    /// </summary>
    public class ExcalmatoryToken : Token
    {
        public override TokenType TokenType => TokenType.Excalmatory;
    }

    /// <summary>
    /// 星号
    /// </summary>
    public class StarToken : Token
    {
        public override TokenType TokenType => TokenType.Star;
    }

    /// <summary>
    /// 文本
    /// </summary>
    public class PlainToken : Token
    {
        public override TokenType TokenType => TokenType.Plain;
    }

    /// <summary>
    /// 换行 '\n'
    /// </summary>
    public class NewLineToken : Token
    {
        public override TokenType TokenType => TokenType.NextLine;
    }

    /// <summary>
    /// Token Collection
    /// </summary>
    public class TokenCollection
    {
        public List<Token> Tokens { get; set; }
    }

    /// <summary>
    /// TokenCollectionExtension
    /// </summary>
    public static class TokenCollectionExtension
    {
        public static TToken AddToken<TToken>(this TokenCollection tokenCollection, string text)
            where TToken : Token, new()
        {
            if (tokenCollection.Tokens == null)
            {
                tokenCollection.Tokens = new List<Token>();
            }

            TToken token = new TToken();
            token.Text = text;

            tokenCollection.Tokens.Add(token);

            return token;
        }

        public static TokenCollection AddToken(this TokenCollection tokenCollection, Token token)
        {
            if (tokenCollection.Tokens == null)
            {
                tokenCollection.Tokens = new List<Token>();
            }

            tokenCollection.Tokens.Add(token);

            return tokenCollection;
        }
    }
}
