namespace Longest_Zigzag_Subsequence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var seq = new int[] { 24, 5, 31, 3, 3, 342, 51, 114, 52, 55, 56, 58 };//8, 3, 5, 7, 0, 9, 10, 20, 20, 20, 12, 19, 11 };
            Console.WriteLine(string.Join(" ", LongestZigzagSubsequence(seq)));
        }

        private static T[] LongestZigzagSubsequence<T>(T[] seq)
            where T : IComparable<T>
        {
            int[] len = new int[seq.Length];
            int[] prev = new int[seq.Length];
            int maxLen = 0;
            int lastIndex = -1;

            for (int x = 0; x < seq.Length; x++)
            {
                len[x] = 1;
                prev[x] = -1;
                for (int i = 0; i < x; i++)
                {
                    if (((i % 2 == 0 && seq[i].CompareTo(seq[x]) < 0) || (i % 2 == 1 && seq[i].CompareTo(seq[x]) > 0))
                        || ((i % 2 == 1 && seq[i].CompareTo(seq[x]) < 0) || (i % 2 == 0 && seq[i].CompareTo(seq[x]) > 0))
                        && len[i] >= len[x])
                    {
                        len[x] = len[i] + 1;
                        prev[x] = i;
                    }
                }

                if (len[x] > maxLen)
                {
                    maxLen = len[x];
                    lastIndex = x;
                }
            }

            var longestSeq = new LinkedList<T>();
            while (lastIndex != -1)
            {
                longestSeq.AddFirst(seq[lastIndex]);
                lastIndex = prev[lastIndex];
            }

            return longestSeq.ToArray();
        }
    }
}
