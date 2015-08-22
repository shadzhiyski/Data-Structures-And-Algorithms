namespace Round_Dance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Unit
    {
        public int Node;

        public int Dist;
    }

    public class RoundeDanceSolvation
    {
        static IList<int>[] graph;
        static bool[] visited;
        static Unit furthestEnd = new Unit { Node = 0, Dist = 0 };
        static IList<int> units = new List<int>(); 
        static int[] numberOfLinks;
        static int maxLength = 0;
        static int minVal = 0;
        static int maxVal = 0;
        static int startVertex = 0;

        static void LinkVertices(int vertex1, int vertex2)
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

        public static void ParseConsoleInputAndInitGraph()
        {
            int nodesCount = int.Parse(Console.ReadLine());
            startVertex = int.Parse(Console.ReadLine());
            Queue<int> vertices = new Queue<int>(nodesCount);
            Queue<int> referredVertices = new Queue<int>(nodesCount);

            for (int i = 0; i < nodesCount; i++)
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

            for (int i = 0; i < nodesCount; i++)
            {
                var vertex = vertices.Dequeue();
                var refVertex = referredVertices.Dequeue();
                LinkVertices(vertex - minVal, refVertex - minVal);
            }

            numberOfLinks = new int[maxVal - minVal + 1];
            visited = new bool[maxVal - minVal + 1];
        }

        static void PrintGraph()
        {
            for (int i = 0; i < graph.Length; i++)
            {
                if (graph[i] != null)
                {
                    Console.Write("{0}:", i + 1);
                    for (int j = 0; j < graph[i].Count; j++)
                    {
                        Console.Write(" {0}", graph[i][j] + 1);
                    }
                    Console.WriteLine(";");
                }
            }
        }

        static void FindMaxLengthFrom(int vertex)
        {
            furthestEnd = new Unit() { Node = vertex, Dist = 0 };
            while (graph[furthestEnd.Node].Count != 1)
            {
                FindFurthestVertexAndUnits(furthestEnd.Node);
                maxLength += furthestEnd.Dist;
                furthestEnd.Dist = 0;
            }
        }

        static void FindLength(int vertex)
        {
            int distance = 0;
            FindFurthestVertexAndUnits(vertex);
            maxLength = furthestEnd.Dist;

            while (units.Count > 0)
            {
                foreach (var unit in units)
                {
                    FindFurthestVertexAndUnits(unit);    
                }
            }
        }

        static void FindFurthestVertexAndUnits(int vertex, int dist = 0)
        {
            if (graph[vertex].Count != 2 && dist != 0)
            {
                if (dist > furthestEnd.Dist)
                {
                    furthestEnd.Node = vertex;
                    furthestEnd.Dist = dist;
                }

                if (graph[vertex].Count > 2)
                {
                    units.Add(vertex);
                }

                return;
            }
            visited[vertex] = true;
            dist++;
            for (int linkedVert = 0; linkedVert < graph[vertex].Count; linkedVert++)
            {
                if (!visited[graph[vertex][linkedVert]])
                {
                    FindFurthestVertexAndUnits(graph[vertex][linkedVert], dist);
                }
            }
        }

        static void Main(string[] args)
        {
            ParseConsoleInputAndInitGraph();
            FindMaxLengthFrom(startVertex);
            PrintGraph();
            Console.WriteLine();
            Console.WriteLine(maxLength);
        }
    }
}
