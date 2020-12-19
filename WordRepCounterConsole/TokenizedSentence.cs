using System;
using System.Collections.Generic;
using System.Text;

namespace WordRepCounterConsole
{
    public class TokenizedSentence
    {
        public List<string> SentenceTokens { get; private set; }
        public int SequenceNumber { get; private set; }
        public TokenizedSentence(int sequenceNum, List<string> tokens)
        {
            SequenceNumber = sequenceNum;
            SentenceTokens = tokens;
        }
        public override string ToString()
        {
            return String.Join(' ', SentenceTokens);
        }
    }
}
