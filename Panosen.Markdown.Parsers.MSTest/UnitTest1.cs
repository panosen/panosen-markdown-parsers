using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Panosen.Markdown.Parser;
using Panosen.Markdown.Parsers.MSTest;
using Panosen.Markdown.Parsers.Render;
using System.IO;
using System.Text;

namespace Panosen.Markdown.Parser.MSTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var markdown = PrepareMarkdown();

            var fromParsers = new MarkdownDocumentParser().Parse(markdown);
            //var fromParsers2 = new Panosen.Markdown.Parser2.MarkdownDocumentParser();

            File.WriteAllText("f:\\Panosen.Markdown.Parser.json", JsonConvert.SerializeObject(fromParsers, Formatting.Indented));
            //File.WriteAllText("f:\\Panosen.Markdown.Parser2.json", JsonConvert.SerializeObject(fromParsers2, Formatting.Indented));

            var sampleMarkdownRenderer = new MarkdownRenderer();

            var temp = sampleMarkdownRenderer.Transform(fromParsers);

            File.WriteAllText(@"F:\MarkdownRenderer.html", temp);
        }

        public static string PrepareMarkdown()
        {
            return @"
1.标题
# 一级标题![mahua](mahua-logo.jpg)
## 二级标题
### 三级标题
#### 四级标题
##### 五级标题
###### 六级标题

2.无序列表

* 苹果
* 香蕉
* 橘子

3.有序列表

1. 苹果
2. 香蕉
3. 橘子

4.引用
>这里是引用






![mahua](mahua-logo.jpg)
####MaHua是什么?
一个在线编辑markdown文档的编辑器

向Mac下优秀的markdown编辑器mou致敬

##MaHua有哪些功能？

* 方便的`导入导出`功能
    *  直接把一个markdown的文本文件拖放到当前这个页面就可以了
    *  导出为一个html格式的文件，样式一点也不会丢失
* 编辑和预览`同步滚动`，所见即所得（右上角设置）
* `VIM快捷键`支持，方便vim党们快速的操作 （右上角设置）
* 强大的`自定义CSS`功能，方便定制自己的展示
* 有数量也有质量的`主题`,编辑器和预览区域
* 完美兼容`Github`的markdown语法
* 预览区域`代码高亮`
* 所有选项自动记忆

##有问题反馈
在使用中有任何问题，欢迎反馈给我，可以用以下联系方式跟我交流

* 邮件(dev.hubo#gmail.com, 把#换成@)
* QQ: 287759234
* weibo: [@草依山](http://weibo.com/ihubo)
* twitter: [@ihubo](http://twitter.com/ihubo)

##捐助开发者
在兴趣的驱动下,写一个`免费`的东西，有欣喜，也还有汗水，希望你喜欢我的作品，同时也能支持一下。
当然，有钱捧个钱场（右上角的爱心标志，支持支付宝和PayPal捐助），没钱捧个人场，谢谢各位。

##感激
感谢以下的项目,排名不分先后

* [mou](http://mouapp.com/) 
* [ace](http://ace.ajax.org/)
* [jquery](http://jquery.com)

##关于作者

```javascript
  var ihubo = {
    nickName  : ""草依山\"",
    site: ""http://jser.me""
  }
```


1.标题
# 一级标题
## 二级标题
### 三级标题
#### 四级标题
##### 五级标题
###### 六级标题

2.无序列表

* 苹果
* 香蕉
* 橘子

3.有序列表

1. 苹果
2. 香蕉
3. 橘子

4.引用
>这里是引用

5.图片
![Mou icon](http://mouapp.com/mou_128.png)

6.链接
[百度](http://www.baidu.com)

7.粗体
**粗体**

8.斜体
*斜体*

9.表格

| Tables        | Are           | Cool  |
| ------------- |:-------------:| -----:|
| col 3 is      | right-aligned | $1600 |
| col 2 is      | centered      |   $12 |
| zebra stripes | are neat      |    $1 |

10.代码
`Hello World`

11.分割线
***


P => PP|P|A|B|C|D|E

A → abBcdBe
B → BB|B|a|b|c|d|e|f|g
C → bBcdBe
D → ffBff
E → fBf";
        }
    }
}
