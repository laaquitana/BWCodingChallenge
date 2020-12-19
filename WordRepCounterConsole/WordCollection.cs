﻿using System;
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

        public void LoadArticleTokens(string filepath)
        {
            string text = File.ReadAllText(filepath);
            ArticleTokens.AddRange(text.Split(' ', StringSplitOptions.RemoveEmptyEntries));
        }

        public void LoadWordsTokens(string filepath)
        {
            string text = File.ReadAllText(filepath);
            WordsTokens.AddRange(text.Split(new [] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
        }

        public void GenerateTokenizedSentences()
        {
            if (ArticleTokens.Count > 0 && WordsTokens.Count > 0)
            {
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

                // End of paragraph has no .
                /*if (sentenceTokens.Count > 0)
                {
                    TokenizedSentences.Add(new TokenizedSentence(counter, sentenceTokens));
                }*/
            }
        }

        public void GenerateWordsDetailsList()
        {
            if (TokenizedSentences.Count > 0)
            {
                int counter = 0;
                foreach (var token in WordsTokens)
                {
                    WordDetails wordDetails = new WordDetails(token);
                    foreach (var sentence in TokenizedSentences)
                    {
                        foreach (var word in sentence.SentenceTokens)
                        {
                            string temp;
                            
                            // last word in the sentence
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
            }
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

        public void SaveWordCollectionNewFormat(string filepath)
        {
            throw new NotImplementedException();
        }
    }
}