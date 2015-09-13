using System;

namespace Advanced_Tree_Structures
{
    class Program
    {
        static void Main(string[] args)
        {
            var intervals = new IntervalTree<int>();
            intervals.Add(0, 3);
            intervals.Add(1, 3);
            intervals.Add(2, 8);
            intervals.Add(1, 15);
            intervals.Add(11, 23);
            intervals.Add(0, 3);
            intervals.Add(11, 14);
            intervals.Add(11, 17);
            intervals.Add(0, 4);
            intervals.Add(8, 11);
            intervals.Remove(1, 15);
            var intersects = intervals.OverlappingIntervalsOf(1, 2);
            intervals.Traverse();
            foreach (var intersect in intersects)
            {
                Console.WriteLine("{0} - {1}", intersect.Start, intersect.End);
            }
        }
    }
}
