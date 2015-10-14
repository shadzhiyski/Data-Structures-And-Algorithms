using System;
using System.Collections.Generic;

internal class BinaryHeap<T> where T : IComparable<T>
{
    private List<T> heap;

    public BinaryHeap()
    {
        this.heap = new List<T>();
    }

    public BinaryHeap(T[] elements)
    {
        this.heap = new List<T>(elements);
        for (int i = heap.Count / 2; i >= 0; i--)
        {
            HeapifyDown(i);
        }
    }

    public int Count
    {
        get
        {
            return this.heap.Count;
        }
    }

    public T ExtractMin()
    {
        var max = heap[0];
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);
        if (heap.Count > 0)
        {
            HeapifyDown(0);
        }

        return max;
    }

    public T PeekMin()
    {
        return heap[0];
    }

    public void Insert(T node)
    {
        heap.Add(node);
        HeapifyUp(heap.Count - 1);
    }

    private void HeapifyDown(int i)
    {
        var left = 2 * i + 1;
        var right = 2 * i + 2;
        var largest = i;
        if (left < heap.Count
            && heap[left].CompareTo(heap[largest]) < 0)
        {
            largest = left;
        }
        if(right < heap.Count
            && heap[right].CompareTo(heap[largest]) < 0)
        {
            largest = right;
        }
        if (largest != i)
        {
            T old = heap[i];
            heap[i] = heap[largest];
            heap[largest] = old;
            HeapifyDown(largest);
        }
    }

    private void HeapifyUp(int i)
    {
        var parent = (i - 1) / 2;
        while (i > 0 && heap[i].CompareTo(heap[parent]) < 0)
        {
            T old = heap[i];
            heap[i] = heap[parent];
            heap[parent] = old;

            i = parent;
            parent = (i - 1) / 2;
        }
    }
}
