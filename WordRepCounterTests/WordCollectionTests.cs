﻿using NUnit.Framework;
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
        private string _articleFilePath;
        private string _wordsFilePath;
        private string _projectDirectory;
        private WordCollection _wordCollection;
        private List<string> _expectedArticleOutput;
        private List<string> _expectedWordsOutput;

        [SetUp]
        public void Setup()
        {
            _projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            _articleFilePath = _projectDirectory + @"\test_files\test_01\Input\Article.txt";
            _wordsFilePath = _projectDirectory + @"\test_files\test_01\Input\Words.txt";
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
        public void WordCollection_LoadArticleTokens_Output()
        {
            _wordCollection.LoadArticleTokens(_articleFilePath);
            Assert.IsTrue(_wordCollection.ArticleTokens.SequenceEqual(_expectedArticleOutput));
        }

        [Test]
        public void WordCollection_LoadWordsTokens_Output()
        {
            _wordCollection.LoadWordsTokens(_wordsFilePath);
            Assert.IsTrue(_wordCollection.WordsTokens.SequenceEqual(_expectedWordsOutput));
        }

        [Test]
        public void WordCollection_GenerateTokenizedSentences_Output()
        {
            string expectedSentence = "This is what I learned from Mr. Jones about a paragraph.";
            _wordCollection.LoadArticleTokens(_articleFilePath);
            _wordCollection.LoadWordsTokens(_wordsFilePath);
            _wordCollection.GenerateTokenizedSentences();
            Assert.AreEqual(1, _wordCollection.TokenizedSentences.Count);
            Assert.AreEqual(expectedSentence, _wordCollection.TokenizedSentences.First().ToString());
        }
    }
}