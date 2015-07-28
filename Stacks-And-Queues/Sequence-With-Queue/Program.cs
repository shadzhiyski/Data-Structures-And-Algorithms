namespace Sequence_With_Queue
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            var q = new Queue<int>();
            var result = new Queue<int>();
            var numberString = Console.ReadLine();
            var startNumber = int.Parse(numberString.Trim());

            q.Enqueue(startNumber);
            result.Enqueue(startNumber);
            while (result.Count < 50)
            {
                var number = q.Dequeue();
                q.Enqueue(number + 1);
                q.Enqueue(2 * number + 1);
                q.Enqueue(number + 2);

                result.Enqueue(number + 1);
                result.Enqueue(2 * number + 1);
                result.Enqueue(number + 2);
            }

            for (int i = 0; i < 50; i++)
            {
                Console.Write("{0} ", result.Dequeue());
            }

            Console.WriteLine();
        }
    }
}
