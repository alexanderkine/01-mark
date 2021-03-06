﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

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
            var paragraphs = Regex.Split(MarkdownText, @"\r*\n\s*\r*\n")
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(s => string.Format("<p>{0}</p>", s));
            var text = string.Join(string.Empty, paragraphs);
            return ConvertTextWithUnderscores(text);   
        }       

        private string ConvertTextWithUnderscores(string text)
        {
            var builder = new StringBuilder();
            var match = EmLocation.Match(text);
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
            return ConvertTextWithDoubleUnderscores(builder.ToString());
        }

        private string ConvertTextWithDoubleUnderscores(string text)
        {
            var builder = new StringBuilder();
            var match = StrongLocation.Match(text);
            if (!match.Success)
                return text;
            var lastMatchIndex = 0;
            while (match.Success)
            {
                builder.Append(text.Substring(lastMatchIndex, match.Index - lastMatchIndex));
                lastMatchIndex = match.Index + match.Length;
                builder.Append(IsLetter(text[match.Index - 1]) ? "</strong>" : "<strong>");
                match = match.NextMatch();
            }
            builder.Append(text.Substring(lastMatchIndex, text.Length - lastMatchIndex));
            for (var i=0;i<builder.Length-1;i++)
                if (builder[i] == '\\' && builder[i + 1] != '\\')
                    builder.Remove(i, 1);
            return builder.ToString();
        }

        private readonly static Regex LetterRegex = new Regex(@"([^\W_]|\\)");
        private static readonly Regex StrongLocation = new Regex(@"(?<![\\_\w])__(?![\W_\\])|(?<=[^\W_])__(?![_\w])");
        private static readonly Regex EmLocation = new Regex(@"(?<![\w_\\])_(?![\W_\\])|(?<=[^\W_])_(?![\w_])");
        private static readonly Regex EmLocationBegin = new Regex(@"(?<![\w_\\])_(?![\W_\\])");
        private static readonly Regex EmLocationEnd = new Regex(@"(?<=[^\W_])_(?![_\w])");
        private static readonly Regex StrongLocationBegin = new Regex(@"(?<![\w_\\])__(?![\W_\\])");
        private static readonly Regex StrongLocationEnd = new Regex(@"(?<=[^\W_])__(?![_\w])");
        private static bool IsLetter(char sign)
        {
            var signString = sign.ToString();
            return LetterRegex.Match(signString).ToString() == signString;
        }
    }
}
