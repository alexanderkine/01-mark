using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace mark
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = "\n    \nTesting is good!\n\n\n";
            var processor = new MarkdownProcessor(text);
            var actualResult = processor.ConvertFromMarkdownToHtml();
        }
    }
}
