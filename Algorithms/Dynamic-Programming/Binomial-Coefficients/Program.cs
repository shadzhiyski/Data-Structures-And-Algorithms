namespace Binomial_Coefficients
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        private const int MAX = 100;
        private static ulong[,] binomCoeff = new ulong[MAX, MAX];

        private static ulong Binom(int n, int k)
        {
            if (k > n) { return 0; }
            else if (k == 0 || k == n) { return 1; }
            else if (binomCoeff[n, k] == 0) 
            {
                binomCoeff[n, k] = Binom(n - 1, k - 1) + Binom(n - 1, k);
            }

            return binomCoeff[n, k];
        }

        static void Main(string[] args)
        {
            Console.WriteLine(Binom(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine())));
        }
    }
}
