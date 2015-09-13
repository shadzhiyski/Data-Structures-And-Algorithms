namespace OrderedSetExample
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class OrderedSet<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        private Node root;

        public int Count { get; private set; }

        public void Add(T element)
        {
            Node currNode = root;
            if (root == null)
            {
                root = new Node(element, null);
            }
            else
            {
                while (currNode != null)
                {
                    if (currNode.Value.CompareTo(element) > 0)
                    {
                        if (currNode.Left == null)
                        {
                            currNode.Left = new Node(element, currNode); break;
                        }
                        currNode = currNode.Left;
                    }
                    else if (currNode.Value.CompareTo(element) <= 0)
                    {
                        if (currNode.Right == null)
                        {
                            currNode.Right = new Node(element, currNode); break;
                        }
                        currNode = currNode.Right;
                    }
                }
            }

            Count++;
        }

        public bool Contains(T element)
        {
            Node currNode = root;
            while (currNode != null)
            {
                if (currNode.Value.CompareTo(element) > 0)
                {
                    currNode = currNode.Left;
                }
                else if (currNode.Value.CompareTo(element) < 0)
                {
                    currNode = currNode.Right;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public void Remove(T element)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            var visited = new MyDictionary<Node, T>();
            var nodes = new Stack<Node>();
            if (root != null)
            {
                nodes.Push(root);
                while (nodes.Count > 0)
                {
                    var currentNode = nodes.Peek();
                    if (currentNode != null)
                    {
                        if (!visited.ContainsKey(currentNode))
                        {
                            nodes.Push(currentNode.Left);
                        }
                        else
                        {
                            yield return currentNode.Value;
                            nodes.Pop();
                            nodes.Push(currentNode.Right);
                        }
                    }
                    else
                    {
                        nodes.Pop();
                        if (nodes.Count > 0)
                        {
                            visited.Add(nodes.Peek(), nodes.Peek().Value);
                        }
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class Node
        {
            public Node(T value, Node parent)
            {
                Value = value;
                Parent = parent;
            }

            public T Value { get; set; }

            public Node Parent { get; set; }

            public Node Left { get; set; }

            public Node Right { get; set; }
        }
    }
}
