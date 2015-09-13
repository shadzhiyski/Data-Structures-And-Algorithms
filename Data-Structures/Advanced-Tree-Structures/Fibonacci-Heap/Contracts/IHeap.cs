using System;

namespace Fibonacci_Heap.Contracts
{
    public interface IHeap<P, V> where P : IComparable<P>
    {
        void Insert(P priority, V value);

        V FindMin();

        V DeleteMin();

        void DecreaseKey(P priority, P newPriority);
    }
}
