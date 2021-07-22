using System;
using System.Collections.Generic;
using System.Text;

namespace Panosen.Markdown.Parser2
{
    public enum Status
    {
        Empty,

        /// <summary>
        /// 感叹号"!"
        /// </summary>
        Image1,
        /// <summary>
        /// 左中括号"["
        /// </summary>
        Image2,
        /// <summary>
        /// 中括号内文字
        /// </summary>
        Image3,
        /// <summary>
        /// 右中括号"]"
        /// </summary>
        Image4,
        /// <summary>
        /// 左小括号"("
        /// </summary>
        Image5,
        /// <summary>
        /// 小括号内文字
        /// </summary>
        Image6,

        /// <summary>
        /// 左中括号"["
        /// </summary>
        Link1,
        /// <summary>
        /// 中括号内文字
        /// </summary>
        Link2,
        /// <summary>
        /// 右中括号"]"
        /// </summary>
        Link3,
        /// <summary>
        /// 左小括号"("
        /// </summary>
        Link4,
        /// <summary>
        /// 小括号内文字
        /// </summary>
        Link5,

        /// <summary>
        /// 粗体第一个*
        /// </summary>
        Star1,
        /// <summary>
        /// 粗体第二个*
        /// </summary>
        Star2,
        /// <summary>
        /// 粗体第三个*
        /// </summary>
        Star3,
        /// <summary>
        /// 粗体第四个*
        /// </summary>
        Star4
    }
}
