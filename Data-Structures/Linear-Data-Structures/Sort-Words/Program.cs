namespace Sort_Words
{
    using System;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter words:");
            string inputString = Console.ReadLine();
            var words = inputString.Split(' ')
                .OrderBy(w => w)
                .ToList();

            Console.WriteLine(string.Join(" ", words));
        }
    }
}
