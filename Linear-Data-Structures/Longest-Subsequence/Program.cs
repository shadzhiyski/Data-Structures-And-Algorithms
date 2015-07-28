namespace Longest_Subsequence
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        public static List<int> LongestSubseqence(List<int> numbers)
        {
            int maxCount = 1,
                currCount = 1,
                endIndex = 0,
                len = numbers.Count;

            for (int i = 1; i <= len; i++)
            {
                if (i < len && numbers[i] == numbers[i - 1]) { currCount++; }
                else 
                { 
                    if (maxCount < currCount)
                    {
                        maxCount = currCount;
                        endIndex = i - 1;
                    }

                    currCount = 1;
                }
            }

            List<int> maxSequence = new List<int>();
            for (int i = 0; i < maxCount; i++)
            {
                maxSequence.Add(numbers[endIndex - i]);
            }

            return maxSequence;
        } 
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the numbers:");
            string numbersInputString = Console.ReadLine();
            string[] numberString = numbersInputString.Split(' ');
            int len = numberString.Length;
            List<int> numbers = new List<int>(len);

            for (int i = 0; i < len; i++)
            {
                numbers.Add(int.Parse(numberString[i]));
            }

            var longestSequence = LongestSubseqence(numbers);

            Console.WriteLine(string.Join(" ", longestSequence));
        }
    }
}
