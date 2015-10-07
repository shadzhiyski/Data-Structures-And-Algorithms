namespace Combinations_Iteratively
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static int steps = 0;
        static void Main(string[] args)
        {
            int n = 4, k = 3;
            int[] array = Enumerable.Range(1, n).ToArray();
            Console.WriteLine("Combinations iteratively:");
            Combinations(array, k);
            Console.WriteLine(steps);
        }

        public static void Combinations<T>(T[] array, int k)
        {
            int n = array.Length;
            int[] com = new int[n];
            for (int i = 0; i < k; i++) { com[i] = i; }
            while (com[k - 1] < n) {
                for (int i = 0; i < k; i++)
                {
                    Console.Write("{0} ", array[com[i]]);
                }
                Console.WriteLine();
                steps++;
                int t = k - 1;
                while (t != 0 && com[t] == n - k + t) t--;
                com[t]++;
                for (int i = t + 1; i < k; i++) com[i] = com[i - 1] + 1;
            }
        }

        private static void Swap<T>(int index1, int index2, ref T[] array)
        {
            if (index1 != index2)
            {
                T temp = array[index1];
                array[index1] = array[index2];
                array[index2] = temp;
            }
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
