using System;
using System.Collections.Generic;
using System.Text;

namespace WordRepCounterConsole
{
    public class WordDetails
    {
        public string Word { get; private set; }
        public List<int> SentenceNumberList { get; set; }
        public WordDetails(string word)
        {
            Word = word;
            SentenceNumberList = new List<int>();
        }
        public override string ToString()
        {
            return $"{Word} {{{SentenceNumberList.Count}:{String.Join(',',SentenceNumberList)}}}";
        }
    }
}
