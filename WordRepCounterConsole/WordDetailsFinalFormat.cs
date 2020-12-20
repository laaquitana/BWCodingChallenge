using System;
using System.Collections.Generic;
using System.Text;

namespace WordRepCounterConsole
{
    public class WordDetailsFinalFormat
    {
        public string Prefix { get; private set; }
        public WordDetails WordDetailsInfo { get; set; }
        
        public WordDetailsFinalFormat(string prefix, WordDetails wordDetails)
        {
            Prefix = prefix;
            WordDetailsInfo = wordDetails;
        }

        public override string ToString()
        {
            return $" {Prefix}. {WordDetailsInfo.ToString()}";
        }
    }
}
