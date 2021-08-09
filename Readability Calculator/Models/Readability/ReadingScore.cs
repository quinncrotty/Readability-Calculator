using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Readability_Calculator.Models.Readability;

namespace ReadabilityCalculator.Models.Readability
{
    public class ReadingScore
    {
        private string text;
        public int words { get; private set; }

        public int sentences { get; private set; }

        public int syllables { get; private set; }
        public double Calculate(string inputText)
        {
            text = inputText;
            SyllableDictionary syllableDictionary = new SyllableDictionary();

            foreach (Match m in Regex.Matches(text, @"\b[^\s]+\b"))
            {
                if(syllableDictionary.GetSyllableCount(m.Value) == -1)
                {
                    // Counts Syllables in a Word 
                    syllables += Regex.Matches(m.Value, @"[aeiouy]+?\w*?[^e]").Count;
                }
                else
                {
                    syllables += syllableDictionary.GetSyllableCount(m.Value);
                }

            }
            // Counts Words in String
            words = Regex.Matches(text, @"\b[^\s]+\b").Count;

            // Counts Sentences in String
            sentences = Regex.Matches(text, @"\s+[^.!?]*[.!?]").Count;

            

           
            // Flesch Reading Ease Formula
            double score = Math.Round(206.835 - (1.015 * (Convert.ToDouble(words) / Convert.ToDouble(sentences))) - (84.6 * (Convert.ToDouble(syllables) / Convert.ToDouble(words))), 2);

            return score;
        }
    }
}
