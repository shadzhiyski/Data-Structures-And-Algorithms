namespace Distance_Between_Vertices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static int count = 0;
        static Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
        static List<Tuple<int, int>> distancesToFind = new List<Tuple<int, int>>(); 

        static void Main(string[] args)
        {
            graph = ReadGraph();
            distancesToFind = ReadDistancesToFind();
            FindDistances(distancesToFind);
        }

        private static void FindDistances(List<Tuple<int, int>> distancesToFind)
        {
            foreach (var distance in distancesToFind)
            {
                TraverseBFS(distance.Item1, distance.Item2);
            }
        }

        private static List<Tuple<int, int>> ReadDistancesToFind()
        {
            Console.WriteLine("Distances to find:");

            var distances = new List<Tuple<int, int>>();
            
            string inputRow = null;
            while (!string.IsNullOrEmpty(inputRow = Console.ReadLine()))
            {
                string[] inputParams = inputRow.Split('-');

                int vertex1 = int.Parse(inputParams[0]);
                int vertex2 = int.Parse(inputParams[1]);
                distances.Add(new Tuple<int, int>(vertex1, vertex2));
            }

            return distances;
        }

        private static Dictionary<int, List<int>> ReadGraph()
        {
            Console.WriteLine("Graph:");

            var graph = new Dictionary<int, List<int>>();
            string inputRow = null;
            while (!string.IsNullOrEmpty(inputRow = Console.ReadLine()))
            {
                string[] inputParams = inputRow
                    .Split(new string[] { " -> ", " ->" }, StringSplitOptions.RemoveEmptyEntries);
                
                int vertex = int.Parse(inputParams[0]);
                graph.Add(vertex, new List<int>());

                if (inputParams.Length > 1)
                {
                    string[] childNode = inputParams[1]
                        .Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < childNode.Length; i++)
                    {
                        graph[vertex].Add(int.Parse(childNode[i]));
                    }
                }
            }
            
            return graph;
        }

        public static void TraverseBFS(int node, int destNode)
        {
            count = 0;
            var nodes = new Queue<int>();
            var visited = new HashSet<int>();
            var countChildsInCurrWave = 1;
            var countChildsInNextWave = 0;

            // Enqueue the start node to the queue
            visited.Add(node);
            nodes.Enqueue(node);

            // Breadth-First Search (BFS)
            while (nodes.Count != 0)
            {
                int currentNode = nodes.Dequeue();

                if (currentNode == destNode)
                {
                    Console.WriteLine("{{{0}, {1}}} -> {2}", node, currentNode, count);
                    return;
                }

                foreach (var childNode in graph[currentNode])
                {
                    if (!visited.Contains(childNode))
                    {
                        countChildsInNextWave++;
                        nodes.Enqueue(childNode);
                        visited.Add(childNode);
                    }
                }

                if (--countChildsInCurrWave == 0)
                {
                    countChildsInCurrWave = countChildsInNextWave;
                    countChildsInNextWave = 0;

                    // the count of waves (the depth) is the length between the two nodes
                    count++;
                }
            }

            // if no connection
            Console.WriteLine(@"({0}, {1}) -> {2}", node, destNode, -1);
        }
    }
}
