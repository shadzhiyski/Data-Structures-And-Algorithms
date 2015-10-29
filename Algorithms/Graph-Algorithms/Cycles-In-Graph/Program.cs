namespace Cycles_In_Graph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        private static Dictionary<string, List<string>> graph;
        static HashSet<string> visited;

        static void Main(string[] args)
        {
            graph = ReadGraph();
            if (!HasCycles(graph)) 
            {
                Console.WriteLine("Acyclic: Yes");
            }
            else
            {
                Console.WriteLine("Acyclic: No");
            }
        }

        private static Dictionary<string, List<string>> ReadGraph()
        {
            var graph = new Dictionary<string, List<string>>();
            string inputRow = null;
            while (!string.IsNullOrEmpty(inputRow = Console.ReadLine()))
            {
                string[] inputParam = inputRow
                    .Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);

                if (!graph.ContainsKey(inputParam[0]))
                {
                    graph.Add(inputParam[0], new List<string>());
                }
                if (!graph.ContainsKey(inputParam[1]))
                {
                    graph.Add(inputParam[1], new List<string>());
                }

                graph[inputParam[0]].Add(inputParam[1]);
                graph[inputParam[1]].Add(inputParam[0]);
            }

            return graph;
        }

        static bool HasCycles(Dictionary<string, List<string>> graph)
        {
            try
            {
                visited = new HashSet<string>();
                DFS(graph.Keys.FirstOrDefault());  

                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }

        static void DFS(string node, string prevNode = null)
        {
            if (!visited.Contains(node))
            {
                visited.Add(node);
                for (int i = 0; i < graph[node].Count; i++)
                {
                    if (!graph[node][i].Equals(prevNode))
                    {
                        DFS(graph[node][i], node);
                    }
                }
            }
            else
            {
                throw new Exception("A cycle detected in graph");
            }
        }
    }
}
