namespace Play_With_Trees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class PlayWithTrees
    {
        static IDictionary<int, Tree<int>> nodeByValue = new Dictionary<int, Tree<int>>();

        static Tree<int> GetTreeNodeByValue(int value)
        {
            if (!nodeByValue.ContainsKey(value))
            {
                nodeByValue[value] = new Tree<int>(value);
            }

            return nodeByValue[value];
        }

        static Tree<int> FindRootNode()
        {
            var rootNode = nodeByValue.Values.FirstOrDefault(n => n.Parent == null);
            return rootNode;
        }

        static IEnumerable<Tree<int>> FindMiddleNodes()
        {
            var middleNodes = nodeByValue.Values
                .Where(n => n.Children.Count > 0 && n.Parent != null);
            return middleNodes;
        }

        static IEnumerable<Tree<int>> FindLeafNodes()
        {
            var leafNodes = nodeByValue.Values
                .Where(n => n.Children.Count == 0);
            return leafNodes;
        }

        static IEnumerable<Tree<int>> FindSubtreesWithSum(int sum)
        {
            var subtreeRoots = new List<Tree<int>>();
            foreach (var node in nodeByValue)
            {
                if (node.Value.Children.Sum(n => n.Value) + node.Value.Value == sum)
                {
                    subtreeRoots.Add(node.Value);
                }
            }

            return subtreeRoots;
        }

        static IEnumerable<IEnumerable<int>> FindAllPathsWithGivenSum(int sum)
        {
            var paths = new List<List<int>>();
            var nodes = nodeByValue.Values;
            foreach (var node in nodes)
            {
                var path = new List<int>();
                var parent = node.Parent;
                int pathSum = node.Value;
                path.Add(node.Value);
                while (parent != null)
                {
                    pathSum += parent.Value;
                    if (pathSum > sum) { break; }
                    path.Add(parent.Value);
                    parent = parent.Parent;
                }

                if (pathSum == sum)
                {
                    path.Reverse();
                    paths.Add(path);
                }
            }

            return paths;
        }

        static IEnumerable<int> FindLongestPath()
        {
            var leafs = FindLeafNodes();
            Tree<int> startLeaf = null;
            int longestLength = 1;
            foreach (var leaf in leafs)
            {
                var parent = leaf.Parent;
                int count = 1;
                while (parent != null)
	            {
                    count++;
                    parent = parent.Parent;
	            }

                if (count > longestLength) 
                { 
                    longestLength = count;
                    startLeaf = leaf;
                }
            }

            int[] path = new int[longestLength--];
            if (startLeaf == null)
            {
                path[longestLength] = nodeByValue.FirstOrDefault().Value.Value;
                return path;
            }

            var pathParent = startLeaf.Parent;
            path[longestLength--] = startLeaf.Value;
            while (pathParent != null)
            {
                path[longestLength--] = pathParent.Value;
                pathParent = pathParent.Parent;
            }

            return path;
        }

        static void Main(string[] args)
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

            int pathNum = int.Parse(Console.ReadLine());
            int subtreeNum = int.Parse(Console.ReadLine());

            var leafNodes = FindLeafNodes();
            var middleNodes = FindMiddleNodes();
            var subtreeRoots = FindSubtreesWithSum(subtreeNum);
            var longestPath = FindLongestPath();
            var pathsBySum = FindAllPathsWithGivenSum(pathNum);

            Console.WriteLine("Root node: {0}", 
                nodeByValue.FirstOrDefault().Value.Value);
            Console.WriteLine();

            Console.WriteLine("Leaf nodes: {0}", 
                string.Join(", ", leafNodes.Select(n => n.Value).ToList()));
            Console.WriteLine();

            Console.WriteLine("Middle nodes: {0}",
                string.Join(", ", middleNodes.Select(n => n.Value).ToList()));
            Console.WriteLine();

            Console.WriteLine("Longest path:\n{0} (length = {1})", 
                string.Join(" -> ", longestPath),
                longestPath.Count());
            Console.WriteLine();

            Console.WriteLine("Paths of sum {0}:\n{1}", 
                pathNum,
                string.Join("\n", 
                    pathsBySum.Select(p => string.Join(" -> ", p.ToList()))
                )
            );
            Console.WriteLine();

            Console.WriteLine("Subtrees of sum {0}:\n{1}",
                subtreeNum,
                string.Join("\n", subtreeRoots
                .Select(n =>
                    n.Value
                    + (n.Children.Count == 0 ? "" : " + ")
                    + string.Join(" + ", n.Children.Select(c => c.Value)))
                .ToList()
                )
            );
        }
    }
}
