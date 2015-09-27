namespace Nested_Loops_To_Recursion
{
    using System;

    class Program
    {
        static int n = 3;
        static int counter = 0;

        static void Main(string[] args)
        {
            CombinatorialUtils cUtils = new CombinatorialUtils(Print);
            Console.WriteLine("Exponential loop (variations):");
            cUtils.LoopExponentially(n, n);

            Console.WriteLine();
            Console.WriteLine("Combinations with repetitions:");
            cUtils.MakeCombinations(n, n - 1, true);

            Console.WriteLine();
            Console.WriteLine("Combinations without repetitions:");
            cUtils.MakeCombinations(n, n - 1, false);
        }

        private static void Print(string value)
        {
            Console.WriteLine("{0}", value);
            counter++;
        }
    }
}
