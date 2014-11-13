using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Web;

namespace mark
{
    class Program
    {
        private static void Main(string[] args)
        {
            var file = "";
            if (args.Length == 0)
            {
                Console.Write("Enter input file name: ");
                file = Console.ReadLine();
            }
            else
                file = args[0];
            var fileName = Path.GetFileNameWithoutExtension(file);
            if (!File.Exists(file))
            {
                Console.WriteLine("File doesn't exist");
                return;
            }
            var input = ParseSpecialSymbols(File.ReadAllText(file));
            using (var writer = new StreamWriter(fileName + ".html"))
            {
                var processor = new MarkdownProcessor(input);
                writer.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">" + processor.ConvertFromMarkdownToHtml());
            }
        }

        public static string ParseSpecialSymbols(string text)
        {
            return HttpUtility.HtmlEncode(text);
        }
    }
}
