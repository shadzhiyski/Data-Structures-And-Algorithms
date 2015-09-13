namespace Find_Root
{
    using System;
    using System.Collections.Generic;

    public static class FindRoot
    {
        public static IDictionary<int, Tree<int>> NodeByValue = new Dictionary<int, Tree<int>>();

        static Tree<int> GetTreeNodeByValue(int value)
        {
            if (!NodeByValue.ContainsKey(value))
            {
                NodeByValue[value] = new Tree<int>(value);
            }

            return NodeByValue[value];
        }

        public static void ParseConsoleInput()
        {
            int nodesCount = int.Parse(Console.ReadLine());
            for (int i = 1; i < nodesCount; i++)
            {
                string[] edge = Console.ReadLine().Split(' ');
                int parentValue = int.Parse(edge[0]);
                Tree<int> parentNode = GetTreeNodeByValue(parentValue);
                int childValue = int.Parse(edge[1]);
                Tree<int> childNode = GetTreeNodeByValue(childValue);
                parentNode.Children.Add(childNode);
                childNode.Parent = parentNode;
            }
        }

        public static IList<int> FindRoots()
        {
            IList<int> roots = new List<int>();
            foreach (var node in NodeByValue)
            {
                if (node.Value.Parent == null)
                {
                    roots.Add(node.Value.Value);
                }
            }

            return roots;
        }

        static void SeekRoots()
        {
            var roots = FindRoots();

            if (roots.Count > 0)
            {
                foreach (var root in roots)
                {
                    Console.WriteLine(root);
                }
            }
            else
            {
                Console.WriteLine("No roots.");
            }
        }

        public static void Main()
        {
            ParseConsoleInput();

            SeekRoots();
        }
    }
}
