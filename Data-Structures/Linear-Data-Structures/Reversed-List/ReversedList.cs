namespace Reversed_List
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IEnumerable<T>
    {
        private int size = 4;
        private int count = 0;
        private T[] item;

        public ReversedList(int size)
        {
            this.size = size;
            item = new T[size];
        }

        public ReversedList()
        {
            item = new T[size];
        }

        public T this[int index]
        {
            get { return this.item[count - index - 1]; }
        }

        public int Count 
        {
            get { return count; }
            private set { count = value; }
        }

        public int Capacity 
        {
            get { return size; }
            private set { size = value; }
        }

        public void Add(T item)
        {
            if (count == size) { Allocate(); }
            this.item[count++] = item;
        }

        public bool Remove(int index)
        {
            if (index >= count || index < 0) { return false; }

            index = count - index - 1;
            T[] newArr = new T[index];
            T[] after = new T[size - index - 1];
            Array.Copy(item, 0, newArr, 0, index);
            Array.Copy(item, index + 1, after, 0, size - index - 1);
            item = new T[--size];
            newArr.CopyTo(item, 0);
            after.CopyTo(item, newArr.Length);
            count--;

            return true;
        }

        private void Allocate()
        {
            size = size * 2;
            T[] newArr = new T[size];
            Array.Copy(item, 0, newArr, 0, count);
            item = newArr;
        }

        public IEnumerator<T> GetEnumerator()
        {
            int index = count;
            if (count > 0)
            {
                while ((--index) >= 0)
                {
                    yield return item[index];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
