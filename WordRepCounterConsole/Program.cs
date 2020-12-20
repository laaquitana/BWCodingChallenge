using System;
using System.IO;

namespace WordRepCounterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Hello User! *****" + Environment.NewLine);
            
            Console.Write("Please enter the full path of input file Article.txt: ");
            string articlePath = Console.ReadLine().Trim(new char[] { '"', ' ' }); 
            if (!File.Exists(articlePath))
            {
                Console.WriteLine($"ERROR: {articlePath} does not exist");
                return;
            }
            
            Console.Write("Please enter the full path of input file Words.txt: ");
            string wordsPath = Console.ReadLine().Trim(new char[] { '"', ' ' });
            if (!File.Exists(wordsPath))
            {
                Console.WriteLine($"ERROR: {wordsPath} does not exist");
                return;
            }

            Console.Write("Please enter the output directory: ");
            string outputDir = Console.ReadLine().Trim(new char[] { '"', ' ' });
            if (!Directory.Exists(outputDir))
            {
                Console.WriteLine($"ERROR: {outputDir} directory does not exist");
                return;
            }
            string outputPath = outputDir + @"\Output.txt";
            
            WordCollection wordCollection = new WordCollection();
            string message;

            if (!(wordCollection.LoadArticleTokens(articlePath, out message) && wordCollection.LoadWordsTokens(wordsPath, out message)))
            {
                Console.WriteLine($"ERROR: {message}");
                return;
            }

            if (!wordCollection.GenerateTokenizedSentences(out message))
            {
                Console.WriteLine($"ERROR: {message}");
                return;
            }

            if (!wordCollection.GenerateWordsDetailsList(out message))
            {
                Console.WriteLine($"ERROR: {message}");
                return;
            }

            if (!wordCollection.SaveWordDetailsListNewFormat(outputPath, out message))
            {
                Console.WriteLine($"ERROR: {message}");
                return;
            }

            Console.WriteLine(Environment.NewLine + $"Success! Please see {outputPath}");
        }
    }
}
