namespace Array_Based_Stack
{
    using System;

    public class ArrayStack<T>
    {
        private const int InitialCapacity = 16;
        private T[] elements;
        private int count = 0;

        public ArrayStack(int capacity = InitialCapacity)
        {
            elements = new T[capacity];
        }

        public int Count
        {
            get { return count; }
            private set { count = value; }
        }

        public void Push(T element)
        {
            if (Count == elements.Length) { Allocate(); }
            elements[Count++] = element;
        }

        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            return elements[Count - 1];
        }

        public T Pop()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            return elements[--Count];
        }

        private void Allocate()
        {
            int capacity = 2 * elements.Length;
            T[] newArr = new T[capacity];
            for (int i = 0; i < Count; i++) { newArr[i] = elements[i]; }
            elements = newArr;
        }

        public T[] ToArray()
        {
            var elems = new T[Count];
            for (int i = 0; i < Count; i++) { elems[i] = elements[Count - (i + 1)]; }

            return elems;
        }
    }
}
