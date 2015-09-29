namespace Round_Dance
{
    using System;
    using System.Collections.Generic;

    class LongestRoundDance
    {
        private static int stepsCount = 0;
        static int minVal = 0;
        static int maxVal = 0;
        static int startVertex = 0;

        private static List<int>[] graph;
        private static bool[] laysOnCurrentPath;
        private static List<int> longestPath = new List<int>();

        public static void FindLongestPathFrom(int index)
        {
            List<int> traversingPath = new List<int>();
            FindLongestPathFrom(index, ref traversingPath);
        }

        public static void FindLongestPathFrom(int index, ref List<int> path)
        {
            if (!LaysOnCurrentPath(index))
            {
                stepsCount++;
                laysOnCurrentPath[index] = true;
                path.Add(index);
                foreach (var vertex in graph[index])
                {
                    FindLongestPathFrom(vertex, ref path);
                }

                CheckPath(ref path);
                path.RemoveAt(path.Count - 1);
                laysOnCurrentPath[index] = false;
            }
        }

        private static void CheckPath(ref List<int> path)
        {
            if (longestPath.Count < path.Count)
            {
                longestPath.Clear();
                longestPath.AddRange(path.ToArray());
            }
        }

        private static bool LaysOnCurrentPath(int vertex)
        {
            if (laysOnCurrentPath[vertex]) { return true; }

            return false;
        }

        private static void PrintLongestPath()
        {
            Console.Write(longestPath[0] + 1);
            for (int vertex = 1; vertex < longestPath.Count; vertex++)
            {
                Console.Write("->{0}", longestPath[vertex] + 1);
            }
            Console.WriteLine();
        }


        private static void ParseConsoleInputAndInitGraph()
        {
            int numberOfFriendships = int.Parse(Console.ReadLine());
            startVertex = int.Parse(Console.ReadLine());
            Queue<int> vertices = new Queue<int>(numberOfFriendships);
            Queue<int> referredVertices = new Queue<int>(numberOfFriendships);

            for (int i = 0; i < numberOfFriendships; i++)
            {
                string[] edge = Console.ReadLine().Split(' ');
                int vertex = int.Parse(edge[0]);
                int refVertex = int.Parse(edge[1]);
                if (i == 0) { minVal = vertex < refVertex ? vertex : refVertex; }
                else
                {
                    if (vertex > maxVal) { maxVal = vertex; }
                    else if (vertex < minVal) { minVal = vertex; }
                    if (refVertex > maxVal) { maxVal = refVertex; }
                    else if (refVertex < minVal) { minVal = refVertex; }
                }

                vertices.Enqueue(vertex);
                referredVertices.Enqueue(refVertex);
            }

            startVertex = startVertex - minVal;
            graph = new List<int>[maxVal - minVal + 1];

            for (int i = 0; i < numberOfFriendships; i++)
            {
                var vertex = vertices.Dequeue();
                var refVertex = referredVertices.Dequeue();
                LinkVertices(vertex - minVal, refVertex - minVal);
            }
            laysOnCurrentPath = new bool[maxVal - minVal + 1];
        }

        private static void LinkVertices(int vertex1, int vertex2)
        {
            if (graph[vertex1] == null)
            {
                graph[vertex1] = new List<int>();
            }
            if (graph[vertex2] == null)
            {
                graph[vertex2] = new List<int>();
            }

            graph[vertex1].Add(vertex2);
            graph[vertex2].Add(vertex1);
        }

        public static void Main()
        {
            ParseConsoleInputAndInitGraph();

            FindLongestPathFrom(startVertex);

            Console.WriteLine();
            Console.WriteLine("Number of friends: {0}", longestPath.Count);
            PrintLongestPath();
            Console.WriteLine();
            Console.WriteLine("Steps: {0}", stepsCount);
        }
    }
}
