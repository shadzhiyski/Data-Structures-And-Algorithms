namespace Permutations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        private static int countOfPermutations = 0;

        static void Main(string[] args)
        {
            int n = 6;
            int[] array = Enumerable.Range(1, n).ToArray();

            Permute(array);

            Console.WriteLine(countOfPermutations);
        }

        private static void Permute(int[] array, int startIndex = 0)
        {
            if (startIndex >= array.Length - 1)
            {
                Console.WriteLine(string.Join(" ", array));
                countOfPermutations++;
            }
            else
            {
                for (int i = startIndex; i < array.Length; i++)
                {
                    Swap(i, startIndex, ref array);
                    Permute(array, startIndex + 1);
                    Swap(startIndex, i, ref array);
                }
            }
        }

        private static void Swap(int i, int startIndex, ref int[] array)
        {
            if (i != startIndex)
            {
                int temp = array[i];
                array[i] = array[startIndex];
                array[startIndex] = temp;
            }
        }
    }
}
