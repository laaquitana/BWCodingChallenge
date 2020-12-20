using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WordRepCounterConsole;

namespace WordRepCounterTests
{
    [TestFixture]
    public class WordCollectionTests
    {
        private string _projectDirectory;

        private string _articleFilePath1;
        private string _wordsFilePath1;
        private string _outputDir1;
        private string _actualOutputFilePath1;
        private string _expectedOutputFilePath1;

        private string _articleFilePath2;
        private string _wordsFilePath2;
        private string _outputDir2;
        private string _actualOutputFilePath2;
        private string _expectedOutputFilePath2;

        private WordCollection _wordCollection;
        private List<string> _expectedArticleOutput;
        private List<string> _expectedWordsOutput;

        [SetUp]
        public void Setup()
        {
            _projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            
            _articleFilePath1 = _projectDirectory + @"\test_files\test_01\Input\Article.txt";
            _wordsFilePath1 = _projectDirectory + @"\test_files\test_01\Input\Words.txt";
            _outputDir1 = _projectDirectory + @"\test_files\test_01\Output";
            _actualOutputFilePath1 = _outputDir1 + @"\Output.txt";
            _expectedOutputFilePath1 = _outputDir1 + @"\expected-output.txt";

            _articleFilePath2 = _projectDirectory + @"\test_files\test_02\Input\Article.txt";
            _wordsFilePath2 = _projectDirectory + @"\test_files\test_02\Input\Words.txt";
            _outputDir2 = _projectDirectory + @"\test_files\test_02\Output";
            _actualOutputFilePath2 = _outputDir2 + @"\Output.txt";
            _expectedOutputFilePath2 = _outputDir2 + @"\expected-output.txt";

            _wordCollection = new WordCollection();

            // Set expected output from Article.txt
            _expectedArticleOutput = new List<string>();
            _expectedArticleOutput.Add("This");
            _expectedArticleOutput.Add("is");
            _expectedArticleOutput.Add("what");
            _expectedArticleOutput.Add("I");
            _expectedArticleOutput.Add("learned");
            _expectedArticleOutput.Add("from");
            _expectedArticleOutput.Add("Mr.");
            _expectedArticleOutput.Add("Jones");
            _expectedArticleOutput.Add("about");
            _expectedArticleOutput.Add("a");
            _expectedArticleOutput.Add("paragraph.");

            // Set expected output from Words.txt
            _expectedWordsOutput = new List<string>();
            _expectedWordsOutput.Add("this");
            _expectedWordsOutput.Add("is");
            _expectedWordsOutput.Add("what");
            _expectedWordsOutput.Add("i");
            _expectedWordsOutput.Add("learned");
            _expectedWordsOutput.Add("from");
            _expectedWordsOutput.Add("mr.");
            _expectedWordsOutput.Add("jones");
            _expectedWordsOutput.Add("about");
            _expectedWordsOutput.Add("a");
            _expectedWordsOutput.Add("paragraph");
        }

        [Test]
        public void WordCollection_LoadArticleTokens_OneSentence()
        {
            Assert.IsTrue(_wordCollection.LoadArticleTokens(_articleFilePath1, out string message));
            Assert.IsTrue(_wordCollection.ArticleTokens.SequenceEqual(_expectedArticleOutput));
            string expectedMessage = $"Successfully parsed {_articleFilePath1}";
            Assert.AreEqual(expectedMessage, message);
        }

        [Test]
        public void WordCollection_LoadWordsTokens_ForOneSentence()
        {
            Assert.IsTrue(_wordCollection.LoadWordsTokens(_wordsFilePath1, out string message));
            Assert.IsTrue(_wordCollection.WordsTokens.SequenceEqual(_expectedWordsOutput));
            string expectedMessage = $"Successfully parsed {_wordsFilePath1}";
            Assert.AreEqual(expectedMessage, message);
        }

        [Test]
        public void WordCollection_GenerateTokenizedSentences_OneSentence()
        {
            string expectedSentence = "This is what I learned from Mr. Jones about a paragraph.";
            Assert.IsTrue(_wordCollection.LoadArticleTokens(_articleFilePath1, out string message));
            string expectedMessage = $"Successfully parsed {_articleFilePath1}";
            Assert.AreEqual(expectedMessage, message);

            Assert.IsTrue(_wordCollection.LoadWordsTokens(_wordsFilePath1, out message));
            expectedMessage = $"Successfully parsed {_wordsFilePath1}";
            Assert.AreEqual(expectedMessage, message);

            Assert.IsTrue(_wordCollection.GenerateTokenizedSentences(out message));
            Assert.AreEqual(1, _wordCollection.TokenizedSentences.Count);
            Assert.AreEqual(expectedSentence, _wordCollection.TokenizedSentences.First().ToString());
            expectedMessage = "Successfully generated tokenized sentences.";
            Assert.AreEqual(expectedMessage, message);

            Assert.IsTrue(_wordCollection.GenerateWordsDetailsList(out message));
            Assert.AreEqual(11, _wordCollection.WordDetailsList.Count);
            Assert.AreEqual("a", _wordCollection.WordDetailsList.First().Prefix);
            Assert.AreEqual("this", _wordCollection.WordDetailsList.First().WordDetailsInfo.Word);
            expectedMessage = "Successfully generated word details list.";
            Assert.AreEqual(expectedMessage, message);

            // compare outputs
            Assert.IsTrue(_wordCollection.SaveWordDetailsListNewFormat(_actualOutputFilePath1, out message));
            expectedMessage = "Successfully saved word details list in new format to an output file.";
            Assert.AreEqual(expectedMessage, message);
            var expectedOutputContent = File.ReadAllText(_expectedOutputFilePath1);
            var actualOutputContent = File.ReadAllText(_actualOutputFilePath1);
            Assert.AreEqual(expectedOutputContent, actualOutputContent);
        }

        [Test]
        public void WordCollection_GenerateTokenizedSentences_MultipleSentences()
        {
            Assert.IsTrue(_wordCollection.LoadArticleTokens(_articleFilePath2, out string message));
            string expectedMessage = $"Successfully parsed {_articleFilePath2}";
            Assert.AreEqual(expectedMessage, message);

            Assert.IsTrue(_wordCollection.LoadWordsTokens(_wordsFilePath2, out message));
            expectedMessage = $"Successfully parsed {_wordsFilePath2}";
            Assert.AreEqual(expectedMessage, message);

            Assert.IsTrue(_wordCollection.GenerateTokenizedSentences(out message));
            Assert.AreEqual(12, _wordCollection.TokenizedSentences.Count);
            expectedMessage = "Successfully generated tokenized sentences.";
            Assert.AreEqual(expectedMessage, message);

            Assert.IsTrue(_wordCollection.GenerateWordsDetailsList(out message));
            Assert.AreEqual(102, _wordCollection.WordDetailsList.Count);
            Assert.AreEqual("xxxx", _wordCollection.WordDetailsList.Last().Prefix);
            Assert.AreEqual("smith", _wordCollection.WordDetailsList.Last().WordDetailsInfo.Word);
            expectedMessage = "Successfully generated word details list.";
            Assert.AreEqual(expectedMessage, message);

            // compare outputs
            Assert.IsTrue(_wordCollection.SaveWordDetailsListNewFormat(_actualOutputFilePath2, out message));
            expectedMessage = "Successfully saved word details list in new format to an output file.";
            Assert.AreEqual(expectedMessage, message);
            var expectedOutputContent = File.ReadAllText(_expectedOutputFilePath2);
            var actualOutputContent = File.ReadAllText(_actualOutputFilePath2);
            Assert.AreEqual(expectedOutputContent, actualOutputContent);
        }
    }
}
