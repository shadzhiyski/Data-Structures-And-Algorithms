using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binary_Heap
{
    class MyComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return x - y;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var pq = new PriorityQueue<int, int>(new MyComparer());
            pq.Enqueue(15, 15);
            pq.Enqueue(18, 18);
            pq.Enqueue(5, 5);
            pq.Enqueue(23, 23);
            pq.Enqueue(19, 19);
            pq.Enqueue(3, 3);
            pq.Enqueue(1, 1);

            while (pq.Count > 0)
            {
                Console.WriteLine(pq.Dequeue());
            }
        }
    }
}
