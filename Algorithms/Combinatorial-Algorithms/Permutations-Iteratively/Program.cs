using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permutations_Iteratively
{
    class Program
    {
        static int countOfPermutations = 0;
        static int[] a;

        static void Main(string[] args)
        {
            int n = 6;
            int[] array = Enumerable.Range(0, n + 1).ToArray();
            a = Enumerable.Range(1, n).Reverse().ToArray();

            Permute(array);

            Console.WriteLine(countOfPermutations);
        }

        static void Permute(int[] p)
        {
            int n = p.Length - 1, i = 1, j = 0;
            while (i < n)
            {
                countOfPermutations++;
                PrintBackwards(a);

                p[i]--;
                if (i % 2 == 1)
                {
                    j = p[i];
                }
                else
                {
                    j = 0;
                }

                Swap(j, i, ref a);
                i = 1;
                while (p[i] == 0)
                {
                    p[i] = i;
                    i++;
                }
            }

            countOfPermutations++;
            PrintBackwards(a);
        }

        static void PrintBackwards(int[] array)
        {
            for (int i = array.Length - 1; i >= 0; i--)
            {
                Console.Write("{0} ", array[i]);
            }
            Console.WriteLine();
        }

        private static void Swap(int index1, int index2, ref int[] array)
        {
            if (index1 != index2)
            {
                int temp = array[index1];
                array[index1] = array[index2];
                array[index2] = temp;
            }
        }
    }
}
