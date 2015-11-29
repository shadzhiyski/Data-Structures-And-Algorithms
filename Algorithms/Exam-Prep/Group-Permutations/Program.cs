namespace Group_Permutations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class Program
    {
        private static int[] occurencies;
        private static Dictionary<char, int> letterOccurencies = new Dictionary<char, int>();
        private static StringBuilder result = new StringBuilder();
        private static char[] letters;

        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            for (int i = 0; i < input.Length; i++)
            {
                if(!letterOccurencies.ContainsKey(input[i]))
                {
                    letterOccurencies.Add(input[i], 0);
                }

                letterOccurencies[input[i]]++;
            }

            int n = letterOccurencies.Keys.Count;
            int[] indices = Enumerable.Range(0, n).ToArray();

            letters = input.ToCharArray().Distinct().ToArray();
            Permute(indices);
            Console.Write(result.ToString());
        }

        private static void Permute(int[] array, int startIndex = 0)
        {
            if (startIndex >= array.Length - 1)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    int letterIndex = array[i];
                    char letter = letters[letterIndex];
                    result.Append(letter, letterOccurencies[letter]);
                }

                result.AppendLine();
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
