using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace mark
{
    public class MarkdownProcessor : IProcessor
    {
        private string MarkdownText;

        public MarkdownProcessor(string markdownText)
        {
            this.MarkdownText = markdownText;
        }
        public string ConvertFromMarkdownToHtml()
        {
            if (string.IsNullOrEmpty(MarkdownText)) return string.Empty;
            var paragraphs = Regex.Split(MarkdownText, @"\r?\n\s*\r?")
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(s => string.Format("<p>{0}</p>", s));
            var text = string.Join(string.Empty, paragraphs);
            return ConvertTextWithUnderscores(text);   
        }

        private string ConvertTextWithUnderscores(string text)
        {
            var markingUnderscoreRegex = new Regex(@"((?<![\w_])_(?![\W_])|(?<=[^\W_]|\\)_(?![\w_]))");
            var builder = new StringBuilder();
            var match = markingUnderscoreRegex.Match(text);
            if (!match.Success)
                return text;
            var lastMatchIndex = 0;
            while (match.Success)
            {
                builder.Append(text.Substring(lastMatchIndex, match.Index - lastMatchIndex));
                lastMatchIndex = match.Index + match.Length;
                builder.Append(IsLetter(text[match.Index - 1]) ? "</em>" : "<em>");
                match = match.NextMatch();
            }
            builder.Append(text.Substring(lastMatchIndex, text.Length - lastMatchIndex));
            return builder.ToString();
        }

        private readonly static Regex LetterRegex = new Regex(@"([^\W_]|\\)");

        private static bool IsLetter(char sign)
        {
            var signString = sign.ToString();
            return LetterRegex.Match(signString).ToString() == signString;
        }
    }
}
