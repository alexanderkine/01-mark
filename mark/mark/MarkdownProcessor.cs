using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace mark
{
    public class MarkdownProcessor : IProcessor
    {
        //private StringBuilder convertedText;
        public string ConvertFromMarkdownToHtml(string markdownText)
        {
            if (string.IsNullOrEmpty(markdownText)) return string.Empty;
            var paragraphs = Regex.Split(markdownText, @"\n\s*\n")
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(s => string.Format("<p>{0}</p>", s));
            return string.Join(string.Empty, paragraphs);
        }
    }
}
