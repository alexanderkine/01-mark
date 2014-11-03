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
        private readonly MarkdownProcessor processor = new MarkdownProcessor();
        [Test]
        public void convert_empty_text()
        {
            var text = "";
            var result = processor.ConvertFromMarkdownToHtml(text);
            Assert.AreEqual(result, string.Empty);
        }

        [Test]
        public void convert_the_paragraph()
        {
            var text = "\n    \nTesting is good!";
            var result = processor.ConvertFromMarkdownToHtml(text);
            Assert.AreEqual(result,"<p>Testing is good!</p>");
        }
    }
}
