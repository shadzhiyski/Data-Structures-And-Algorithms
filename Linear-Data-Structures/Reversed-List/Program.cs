namespace Reversed_List
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var rl = new ReversedList<int>();
            rl.Add(4);
            rl.Add(14);
            rl.Add(8);
            rl.Add(3);
            rl.Add(18);
            rl.Add(23);
            rl.Add(67);
            rl.Add(91);
            rl.Remove(2);
            rl.Remove(2);
            Console.WriteLine(string.Join(" ", rl));
        }
    }
}
