namespace Reverse_Numbers_With_Stack
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            var nums = new Stack<int>();
            var numbersAsString = Console.ReadLine();
            var numbersAsStrings = numbersAsString.Split(' ');

            foreach (var numberAsString in numbersAsStrings)
            {
                nums.Push(int.Parse(numberAsString.Trim()));
            }

            while (nums.Count > 0)
            {
                Console.Write("{0} ", nums.Pop());
            }

            Console.WriteLine();
        }
    }
}
