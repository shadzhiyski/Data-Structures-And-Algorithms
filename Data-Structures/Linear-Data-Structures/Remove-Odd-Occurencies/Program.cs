namespace Remove_Odd_Occurencies
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void DeleteOddOccurencies(List<int> numbers)
        {
            int maxNumber = NumbersUtils.FindMax(numbers);
            int minNumber = NumbersUtils.FindMin(numbers);
            int[] occurencies = NumbersUtils.FindOccurenciesInRange(numbers, minNumber, maxNumber);

            int i = 0;
            while (i < numbers.Count)
            {
                if (occurencies[numbers[i] - minNumber] % 2 == 1)
                { 
                    numbers.Remove(numbers[i]); 
                    continue; 
                }
                
                i++;
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

            DeleteOddOccurencies(numbers);

            Console.WriteLine(string.Join(" ", numbers));
        }
    }
}
