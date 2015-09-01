using System;
using System.Collections;
using System.Collections.Generic;

public class DoublyLinkedList<T> : IEnumerable<T>, IList<T>
    where T : IComparable<T>
{
    private int size = 0;
    internal DoublyLinkedListNode<T> first;
    internal DoublyLinkedListNode<T> last;

    public int Count
    { 
        get { return this.size; }
        internal set { this.size = value; }
    }
	
    public void AddFirst(T element)
    {
        var elemNode = new DoublyLinkedListNode<T>(element);
        if (Count > 0)
        {
            elemNode.Next = first;
            first.Prev = elemNode;
            first = elemNode;
        }
        else 
        {
            first = last = elemNode;
        }

        Count++;
    }

    public void AddLast(T element)
    {
        var elemNode = new DoublyLinkedListNode<T>(element);
        if (Count > 0)
        {
            elemNode.Prev = last;
            last.Next = elemNode;
            last = elemNode;
        }
        else
        {
            first = last = elemNode;
        }

        Count++;
    }

    public T RemoveFirst()
    {
        if (Count == 0) { throw new InvalidOperationException(); }
        var value = first.Value;
        if (first.Next != null) 
        {
            first = first.Next;
            first.Prev = null;
        }
        else
        {
            first = last = null;
        }

        Count--;
        return value;
    }

    public T RemoveLast()
    {
        if (Count == 0) { throw new InvalidOperationException(); }
        var value = last.Value;
        if (last.Prev != null)
        {
            last = last.Prev;
            last.Next = null;
        }
        else
        {
            first = last = null;
        }

        Count--;
        return value;
    }

    public void ForEach(Action<T> action)
    {
        var currNode = first;
        if (Count != 0)
        {
            while (currNode != null)
            {
                action(currNode.Value);
                currNode = currNode.Next;
            }
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        var currNode = first;
        if (Count != 0)
        {
			int count = Count;
            while (--count >= 0)
            {
                yield return currNode.Value;
                currNode = currNode.Next;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public T[] ToArray()
    {
        var array = new T[size];
        int i = 0;
        var currNode = first;
        while (i <= Count)
        {
            array[i++] = currNode.Value;
            currNode = currNode.Next;
        }

        return array;
    }

    public int IndexOf(T item)
    {
        int index = 0;
        var curr = first;
        do
        {
            if (curr.Value.Equals(item)) { return index; }
        } while (++index <= Count);

        return -1;
    }

    public void Insert(int index, T item)
    {
        if (index < 0 || index > Count)
        {
            throw new IndexOutOfRangeException();
        }
        var node = new DoublyLinkedListNode<T>(item);
        if (index == Count) { AddLast(item); }
        else if (index == 0) { AddFirst(item); }
        else
        {
            int currIndex = 0;
            DoublyLinkedListNode<T> curr = null;
            if (Count / 2 > index)
            {
                curr = first;
                while (currIndex++ < index)
                {
                    curr = curr.Next;
                }

                curr.Prev.Next = node;
                node.Next = curr;
            }
            else
            {
                curr = last;
                while (currIndex++ < index)
                {
                    curr = curr.Prev;
                }

                curr.Next.Prev = node;
                node.Prev = curr;
            }
        }
    }

    public void RemoveAt(int index)
    {
        DoublyLinkedListNode<T> curr = null;
        int currIndex = 0;
        if (Count / 2 > index)
        {
            curr = first;
            while (currIndex++ < index)
            {
                curr = curr.Next;
            }

            curr.Prev.Next = curr.Next;
        }
        else
        {
            currIndex = Count;
            curr = last;
            while (--currIndex > index)
            {
                curr = curr.Prev;
            }

            curr.Next.Prev = curr.Prev;
        }
    }

    public T this[int index]
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            throw new NotImplementedException();
        }
    }

    public void Add(T item)
    {
        AddLast(item);
    }

    public void Clear()
    {
        while (first != null)
        {
            first.Prev = null;
            first = first.Next;
        }
        last = null;
        Count = 0;
    }

    public bool Contains(T item)
    {
        var curr = first;
        while (curr != null)
        {
            if (curr.Value.Equals(item)) { return true; }
        }

        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool IsReadOnly
    {
        get { return false; }
    }

    public bool Remove(T item)
    {
        var curr = first;
        while (curr != null)
        {
            if (curr.Value.Equals(item))
            {
                curr.Prev.Next = curr.Next;
                return true;
            }
        }

        return false;
    }
}

internal class DoublyLinkedListNode<T>
{
    private T value;
    private DoublyLinkedListNode<T> next;
    private DoublyLinkedListNode<T> prev;

    public T Value
    {
        get { return this.value; }
        private set { this.value = value; }
    }

    public DoublyLinkedListNode(T val)
    {
        this.value = val;
    }

    public DoublyLinkedListNode<T> Next
    {
        get { return this.next; }
        set { this.next = value; }
    }

    public DoublyLinkedListNode<T> Prev
    {
        get { return this.prev; }
        set { this.prev = value; }
    }
}

//class Example
//{
//    static void Main()
//    {
//        var list = new DoublyLinkedList<int>();

//        list.ForEach(Console.WriteLine);
//        Console.WriteLine("--------------------");

//        list.AddLast(5);
//        list.AddFirst(3);
//        list.AddFirst(2);
//        list.AddLast(10);
//        Console.WriteLine("Count = {0}", list.Count);

//        list.ForEach(Console.WriteLine);
//        Console.WriteLine("--------------------");

//        list.RemoveFirst();
//        list.RemoveLast();
//        list.RemoveFirst();

//        list.ForEach(Console.WriteLine);
//        Console.WriteLine("--------------------");

//        list.RemoveLast();

//        list.ForEach(Console.WriteLine);
//        Console.WriteLine("--------------------");
//    }
//}
