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
        private readonly IProcessor processor = new MarkdownProcessor();
        
        [Test]
        public void convert_empty_text()
        {
            Test("","");
        }

        [Test]
        public void convert_the_paragraph()
        {
            var text = "\n    \nTesting is good!";
            var expectedResult = "<p>Testing is good!</p>";
            Test(text,expectedResult);
        }

        [Test]
        public void convert_the_paragraphs()
        {           
            var text = "Haha!\n    \nTesting is good!\n   \nFor everybody programm!\nIt's true";
            var expectedResult = "<p>Haha!</p><p>Testing is good!</p><p>For everybody programm!\nIt's true</p>";
            Test(text,expectedResult);
        }

        private void Test(string text, string result)
        {
            var actualResult = processor.ConvertFromMarkdownToHtml(text);
            Assert.AreEqual(actualResult,result);
        }
    }
}
