namespace Subsets_Of_String_Array
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var array = new[] { "test", "rock", "fun", "str4" };
            int k = 3;

            Combinations(ref array, k);
        }

        static void Combinations<T>(ref T[] array, int k, int startIndex = 0, int index = 0)
        {
            if (index >= k)
            {
                Print(ref array, k);
            }
            else
            {
                for (int i = startIndex; i < array.Length; i++)
                {
                    Swap(ref array, index, i);
                    Combinations(ref array, k, i + 1, index + 1);
                    Swap(ref array, index, i);
                }
            }
        }

        private static void Swap<T>(ref T[] array, int index1, int index2)
        {
            T temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }

        private static void Print<T>(ref T[] array, int k)
        {
            for (int i = 0; i < k; i++)
            {
                Console.Write("{0} ", array[i].ToString());
            }
            Console.WriteLine();
        }
    }
}
