using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Panosen.Markdown.Parser.MSTest;
using Panosen.Markdown.Parser2;
using System.IO;

namespace Panosen.Markdown.Parsers.MSTest
{
    [TestClass]
    public class MarkdownTokenizerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var markdown = UnitTest1.PrepareMarkdown();

            var markdownTokenizer = new MarkdownTokenizer();

            var tokenCollection = markdownTokenizer.Analyze(markdown);

            File.WriteAllText(@"F:\MarkdownTokenizer.json", JsonConvert.SerializeObject(tokenCollection, Formatting.Indented));
        }
    }
}
