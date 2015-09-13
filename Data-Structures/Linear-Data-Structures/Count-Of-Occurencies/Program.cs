namespace Count_Of_Occurencies
{
    using Remove_Odd_Occurencies;
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void PrintOccurencies(List<int> numbers)
        {
            int maxNumber = NumbersUtils.FindMax(numbers);
            int minNumber = NumbersUtils.FindMin(numbers);
            int[] occurencies = NumbersUtils.FindOccurenciesInRange(numbers, minNumber, maxNumber);
            int len = occurencies.Length;

            for (int i = 0; i < len; i++)
            {
                Console.WriteLine(string.Format("{0} -> {1} times", i + minNumber, occurencies[i]));
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter the numbers:");
            string numbersInputString = Console.ReadLine();
            string[] numberString = numbersInputString.Split(' ');
            int len = numberString.Length;
            List<int> numbers = new List<int>(len);

            for (int i = 0; i < len; i++)
            {
                numbers.Add(int.Parse(numberString[i]));
            }

            PrintOccurencies(numbers);
        }
    }
}
