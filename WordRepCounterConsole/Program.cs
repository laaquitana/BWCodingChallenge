using System;
using System.IO;

namespace WordRepCounterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // Get Article.txt file path from user
            // Get Words.txt file path from user
            // Get output dir from user
            // hardcoded for now, will update later
            string projectDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\.."));
            string articlePath = projectDir + @"\WordRepCounterTests\test_files\test_01\Input\Article.txt";
            string wordsPath = projectDir + @"\WordRepCounterTests\test_files\test_01\Input\Words.txt";

            // Call WordCollection to load/generate/save output
            WordCollection wordCollection = new WordCollection();
            wordCollection.LoadArticleTokens(articlePath);
            wordCollection.LoadWordsTokens(wordsPath);
            wordCollection.GenerateTokenizedSentences();
            wordCollection.GenerateWordsDetailsList();
            
        }
    }
}
