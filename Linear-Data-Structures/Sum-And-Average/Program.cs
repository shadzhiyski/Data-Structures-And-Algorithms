namespace Sum_And_Average
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the numbers:");
            string numbersInputString = Console.ReadLine();
            string[] numberString = numbersInputString.Split(' ');
            int sum = 0, len = numberString.Length;
            double avg = 0;
            List<int> numbers = new List<int>(len);
            
            for (int i = 0; i < len; i++)
            {
                int number = int.Parse(numberString[i]);
                numbers.Add(number);
                sum += number;
            }

            avg = (double)sum / len;
            Console.WriteLine(string.Format("Sum={0}; Average={1}", sum, avg));
        }
    }
}
