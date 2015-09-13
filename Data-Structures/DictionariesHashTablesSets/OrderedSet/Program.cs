namespace OrderedSetExample
{
    using System;
    
    class Program
    {
        static void Main(string[] args)
        {
            var set = new OrderedSet<int>();
            set.Add(17);
            set.Add(9);
            set.Add(12);
            set.Add(19);
            set.Add(6);
            set.Add(25);
            foreach (var item in set)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(set.Contains(26));
            Console.WriteLine(set.Contains(6));
            Console.WriteLine(set.Contains(9));
            Console.WriteLine(set.Contains(12));
            Console.WriteLine(set.Contains(17));
            Console.WriteLine(set.Contains(19));
            Console.WriteLine(set.Contains(25));
        }
    }
}
