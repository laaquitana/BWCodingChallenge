using NUnit.Framework;
using System.Collections.Generic;
using WordRepCounterConsole;

namespace WordRepCounterTests
{
    public class WordDetailsTests
    {
        private WordDetails _wordDetails;
        readonly List<int> _repetitionList = new List<int>();

        [SetUp]
        public void Setup()
        {
            _wordDetails = new WordDetails("sample");

            _repetitionList.Add(1);
            _repetitionList.Add(2);
            _repetitionList.Add(2);
            _repetitionList.Add(5);

            _wordDetails.SentenceNumberList = _repetitionList;
        }

        [Test]
        public void WordDetails_ToString_OutputFormat()
        {
            // Expected format is "<Word> {<listTotalCount>: <listItem1>, <listItem2>, etc.}"
            string expectedOutput = "sample {4:1,2,2,5}";
            Assert.AreEqual(expectedOutput, _wordDetails.ToString());
        }
    }
}