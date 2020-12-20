using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WordRepCounterConsole
{
    public class WordCollection
    {
        public List<string> ArticleTokens { get; private set; }
        public List<string> WordsTokens { get; private set; }
        public List<TokenizedSentence> TokenizedSentences { get; private set; }
        public Dictionary<string, WordDetails> WordDetailsList { get; private set; }
        public Dictionary<int,char> PrefixDictionary { get; private set; }

        public WordCollection()
        {
            ArticleTokens = new List<string>();
            WordsTokens = new List<string>();
            TokenizedSentences = new List<TokenizedSentence>();
            WordDetailsList = new Dictionary<string, WordDetails>();
            SetPrefixDictionary();
        }

        public bool LoadArticleTokens(string filepath, out string message)
        {
            // check if exist
            if (!File.Exists(filepath))
            {
                message = $"{filepath} does not exist.";
                return false;
            }
            // check if empty
            if (new FileInfo(filepath).Length == 0)
            {
                message = $"{filepath} is empty.";
                return false;
            }

            string text = File.ReadAllText(filepath);
            ArticleTokens.AddRange(text.Split(' ', StringSplitOptions.RemoveEmptyEntries));

            message = $"Successfully parsed {filepath}";
            return true;
        }
        
        public bool LoadWordsTokens(string filepath, out string message)
        {
            // check if exist
            if (!File.Exists(filepath))
            {
                message = $"{filepath} does not exist.";
                return false;
            }
            // check if empty
            if (new FileInfo(filepath).Length == 0)
            {
                message = $"{filepath} is empty.";
                return false;
            }

            string text = File.ReadAllText(filepath);
            WordsTokens.AddRange(text.Split(new [] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));

            message = $"Successfully parsed {filepath}";
            return true;
        }

        public bool GenerateTokenizedSentences(out string message)
        {
            if (ArticleTokens.Count == 0)
            {
                message = $"{ArticleTokens} is empty. Please make sure to load an input text file containing a paragraph first before calling this method.";
                return false;
            }

            if (WordsTokens.Count == 0)
            {
                message = $"{WordsTokens} is empty. Please make sure to load an input text file containing a list of words first before calling this method.";
                return false;
            }

            List<string> sentenceTokens = new List<string>();
            int counter = 1;

            foreach (var token in ArticleTokens)
            {
                sentenceTokens.Add(token);
                if (token.EndsWith('.'))
                {
                    if (!WordsTokens.Contains(token.ToLower()))
                    {
                        string[] temp = sentenceTokens.ToArray();
                        TokenizedSentences.Add(new TokenizedSentence(counter, temp.ToList()));
                        sentenceTokens.Clear();
                        counter++;
                    }
                }
            }

            message = "Successfully generated tokenized sentences.";
            return true;
        }

        public bool GenerateWordsDetailsList(out string message)
        {
            if (TokenizedSentences.Count == 0)
            {
                message = $"{TokenizedSentences} is empty. Please make sure to generate tokenized sentences after loading input files.";
                return false;
            }

            int counter = 0;
            foreach (var token in WordsTokens)
            {
                WordDetails wordDetails = new WordDetails(token);
                foreach (var sentence in TokenizedSentences)
                {
                    foreach (var word in sentence.SentenceTokens)
                    {
                        string temp;

                        // last word in the sentence has a trailing '.'
                        if (word.EndsWith('.') && word == sentence.SentenceTokens.Last())
                        {
                            temp = word.Trim('.').ToLower();
                        }
                        else
                        {
                            temp = word.Trim(',').ToLower();
                        }

                        if (token == temp)
                        {
                            wordDetails.SentenceNumberList.Add(sentence.SequenceNumber);
                        }
                    }
                }
                WordDetailsList.Add(GeneratePrefix(counter), wordDetails);
                counter++;
            }

            message = "Successfully generated word details list.";
            return true;
        }

        private void SetPrefixDictionary()
        {
            PrefixDictionary = new Dictionary<int, char>();
            for (int i = 0; i < 26; i++)
            {
                // 97 is 'a' 
                PrefixDictionary.Add(i+1, Char.ConvertFromUtf32(97 + i).ToCharArray()[0]);
            }
        }

        public string GeneratePrefix(int num)
        {
            int letterPosition = (num % 26) + 1;
            int letterRepetition = (num / 26) + 1;

            PrefixDictionary.TryGetValue(letterPosition, out char letter);

            string prefix = new string(letter, letterRepetition);
            
            return prefix;
        }

        public bool SaveWordDetailsListNewFormat(string filepath, out string message)
        {
            foreach (var wordDetails in WordDetailsList)
            {
                string line = $" {wordDetails.Key}. {wordDetails.Value.ToString()}" + Environment.NewLine;
                File.AppendAllText(filepath, line);
            }

            message = "Successfully saved word details list in new format to an output file.";
            return true;
        }
    }
}
