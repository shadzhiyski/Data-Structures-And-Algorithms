namespace Linked_List
{
    using System.Collections;
    using System.Collections.Generic;

    public class LinkedList<T> : IEnumerable<T>
    {
        private int count = 0;
        private Node<T> first;
        private Node<T> last;

        public int Count 
        {
            get { return count; }
            private set { count = value; }
        }

        public void Add(T value)
        {
            var node = new Node<T>(value);
            if (last != null)
            {
                last.Next = node;
                last = node;
            }
            else
            {
                first = last = node;
            }

            count++;
        }

        public bool Remove(int index)
        {
            if (index < 0 || index >= count) { return false; }
            
            var node = first;
            for (int i = 0; i < index - 1; i++) { node = node.Next; }
            if (node.Next.Next != null) 
            {
                node.Next = node.Next.Next; 
            }
            else 
            { 
                node.Next = null; 
            }

            count--;

            return true;
        }

        public int FirstIndexOf(T value)
        {
            var node = first;
            int index = 0;
            while (node != null) 
            {
                if (node.Value.Equals(value)) { return index; }
                node = node.Next;
                index++;
            }

            return -1;
        }

        public int LastIndexOf(T value)
        {
            var node = first;
            int index = -1, curr = 0;
            while (node != null)
            {
                if (node.Value.Equals(value)) { index = curr; }
                node = node.Next;
                curr++;
            }

            return index;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var node = first;
            if (count > 0)
            {
                while (node != null)
                {
                    yield return node.Value;
                    node = node.Next;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class Node<T> 
        {
            private T value;
            private Node<T> next;

            public Node(T value)
            {
                this.Value = value;
            }

            public T Value 
            {
                get { return this.value; }
                set { this.value = value; }
            }

            public Node<T> Next 
            {
                get { return next; }
                set { next = value; }
            }
        }
    }
}
