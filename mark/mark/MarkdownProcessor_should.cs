using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Runtime.InteropServices;

namespace mark
{
    [TestFixture]
    public class MarkdownProcessor_should
    {
        private IProcessor processor;
        
        [Test]
        public void convert_empty_text()
        {
            Test("","");
        }

        [Test]
        public void convert_the_paragraph()
        {
            var text = "\n    \nTesting is good!\n\n\n";
            var expectedResult = "<p>Testing is good!</p>\n";
            Test(text,expectedResult);
        }

        [Test]
        public void convert_the_paragraphs()
        {           
            var text = "Haha!\n    \nTesting is good!\n   \nFor everybody programm!\nIt's true";
            var expectedResult = "<p>Haha!</p><p>Testing is good!</p><p>For everybody programm!\nIt's true</p>";
            Test(text,expectedResult);
        }

        [Test]
        public void convert_text_surrounded_with_underscores()
        {
            var text = "_I can't stop_  \n \r\n _haha_ blabla_123_bla";
            var expectedResult = "<p><em>I can't stop</em>  </p><p> <em>haha</em> blabla_123_bla</p>";
            Test(text, expectedResult);
        }

        private void Test(string text, string result)
        {
            processor = new MarkdownProcessor(text);
            var actualResult = processor.ConvertFromMarkdownToHtml();
            Assert.AreEqual(actualResult,result);
        }
    }
}
