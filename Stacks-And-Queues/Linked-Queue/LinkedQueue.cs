namespace Linked_Queue
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LinkedQueue<T>
    {
        private Node<T> firstNode;
        private Node<T> lastNode;
        private int count = 0;

        public int Count 
        {
            get { return count; }
            private set { count = value; } 
        }

        public void Enqueue(T element)
        {
            if (Count > 0)
            {
                lastNode.NextNode = new Node<T>(element, lastNode);
                lastNode = lastNode.NextNode;
            }
            else
            {
                firstNode = lastNode = new Node<T>(element);
            }

            Count++;
        }

        public T Dequeue()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            var currNode = firstNode;
            firstNode = currNode.NextNode;
            currNode.PrevNode = null;
            currNode.NextNode = null;
            Count--;

            return currNode.Value;
        }

        public T[] ToArray()
        {
            var size = Count;
            var elems = new T[size];
            for (int i = 0; i < size; i++)
            {
                var element = Dequeue();
                elems[i] = element;
            }

            return elems;
        }

        private class Node<T>
        {
            private T value;
            private Node<T> nextNode;
            private Node<T> prevNode;

            public Node(T value, Node<T> prevNode = null, Node<T> nextNode = null)
            {
                this.Value = value;
                this.PrevNode = prevNode;
                this.NextNode = nextNode;
            }

            public T Value
            {
                get { return value; }
                set { this.value = value; }
            }

            public Node<T> NextNode
            {
                get { return nextNode; }
                set { nextNode = value; }
            }

            public Node<T> PrevNode
            {
                get { return prevNode; }
                set { prevNode = value; }
            }
        }
    }
}
