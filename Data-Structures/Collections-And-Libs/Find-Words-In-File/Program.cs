using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_Words_In_File
{
    class Program
    {
        static void Main(string[] args)
        {
            var str = "Hello, I am studying C# at SoftUni.\nC# is my favorite language.\nI like C# and Java.\nat at at";
            var wordsAsString = "C# at";
            var words = wordsAsString.Split(
                new string[] { " " },
                int.MaxValue,
                StringSplitOptions.RemoveEmptyEntries);
            IDictionary<string, int> wordCounts = new Dictionary<string, int>();
            foreach (var word in words)
            {
                wordCounts.Add(word, 0);
            }

            var textWords = str.Split(
                new string[] { " ", "\f", "\n", "\r", "\t", "\v", "!", ".", "," },
                int.MaxValue,
                StringSplitOptions.RemoveEmptyEntries);
            //var punctuation = str.Where(Char.IsPunctuation).Distinct().ToArray();
            //IEnumerable<string> textWords = str.Split().Select(x => x.Trim(punctuation));
            foreach (var word in textWords)
            {
                if (wordCounts.ContainsKey(word))
                {
                    wordCounts[word]++;
                }
            }

            foreach (var wordCount in wordCounts)
            {
                Console.WriteLine(wordCount);
            }
        }
    }
}
