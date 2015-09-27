namespace Tower_Of_Hanoi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    class Program
    {
        static int numberOfElements = 8;
        static int countSteps = 0;

        static void Main(string[] args)
        {
            Stack<int> source = new Stack<int>(Enumerable.Range(1, numberOfElements));
            Stack<int> destination = new Stack<int>();
            Stack<int> spare = new Stack<int>();

            MoveDisks(numberOfElements / 2, source, destination, spare);

            Console.WriteLine(countSteps);
            Console.WriteLine();
            while (source.Count > 0)
            {
                Console.WriteLine(source.Pop());
            }

            Console.WriteLine();
            while (destination.Count > 0)
            {
                Console.WriteLine(destination.Pop());
            }

            Console.WriteLine();
            while (spare.Count > 0)
            {
                Console.WriteLine(spare.Pop());
            }
        }

        static void MoveDisks(int bottomDisk, Stack<int> source, Stack<int> dest, Stack<int> spare)
        {
            countSteps++;
            if (bottomDisk == 1)
            {
                dest.Push(source.Pop());
            }
            else
            {
                MoveDisks(bottomDisk - 1, source, spare, dest);
                spare.Push(source.Pop());
                dest.Push(spare.Pop());
                MoveDisks(bottomDisk - 1, spare, dest, source);
            }
        }
    }
}
