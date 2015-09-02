using System;
using System.Collections.Generic;
using System.Linq;

namespace Fibonacci_Heap
{
    class Program
    {
        static void Main(string[] args)
        {
            var heap = new FibonacciHeap<int, int>();
            heap.Insert(3, 3);
            heap.Insert(8, 8);
            heap.Insert(9, 9);
            heap.Insert(11, 11);

            heap.Insert(14, 14);
            heap.Insert(38, 38);
            heap.Insert(38, 49);
            heap.Insert(3, 56);
            heap.Insert(38, 18);
            heap.Insert(38, 17);
            heap.Insert(3, 4);

            //heap.Insert(3, 3);
            //heap.Insert(8, 8);
            //heap.Insert(9, 9);
            //heap.Insert(11, 11);
            
            //heap.Insert(14, 14);
            //heap.Insert(38, 38);
            //heap.Insert(49, 49);
            //heap.Insert(56, 56);
            //heap.Insert(18, 18);
            //heap.Insert(17, 17);
            //heap.Insert(4, 4);
            int len = heap.Count;
            for (int i = 0; i < len; i++)
            {
                var min = heap.DeleteMin();
                Console.WriteLine(min);
            }
        }
    }
}
