using System;
namespace Linked_Stack
{
    public class LinkedStack<T>
    {
        private Node<T> topNode;
        private int count = 0;

        public int Count 
        {
            get { return count; }
            private set { count = value; }
        }

        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
            
            return topNode.Value;
        }

        public void Push(T element)
        {
            var node = new Node<T>(element, topNode);
            topNode = node;
            Count++;
        }

        public T Pop()
        {
            if (Count == 0) 
            {
                throw new InvalidOperationException();
            }

            var element = topNode.Value;
            topNode = topNode.NextNode;
            Count--;

            return element;
        }

        public T[] ToArray()
        {
            var size = Count;
            int i = 0;
            var elems = new T[size];
            var element = topNode;

            while (element != null) 
            {
                elems[i++] = element.Value;
                element = element.NextNode;
            }

            return elems;
        }

        private class Node<T>
        {
            private T value;
            private Node<T> nextNode;

            public Node(T value, Node<T> nextNode = null) 
            {
                this.Value = value;
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
        }
    }
}
