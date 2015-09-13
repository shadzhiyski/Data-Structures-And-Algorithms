namespace Round_Dance
{
    using Find_Root;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class RoundDance
    {
        static IList<int>[] graph;
        static int maxLength = 0;
        static int[] numberOfLinks;
        static bool[] visited;
        static int minVal = 0;
        static int maxVal = 0;
        static int startVertex = 0;

        static void LinkVertices(int vertex1, int vertex2)
        {
            if(graph[vertex1] == null) 
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
            for (int i = 0; i < graph.Length; i++ )
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

        static void FindLongestPathLengthOf(int vertex, int prevVertex = -1, int dist = 0)
        {
            if (++dist > maxLength) { maxLength = dist; }
            for (int refVertex = 0; refVertex < graph[vertex].Count; refVertex++)
			{
                if (graph[vertex][refVertex] != prevVertex) 
                { 
                    FindLongestPathLengthOf(graph[vertex][refVertex], vertex, dist);
                }
			}
        }

        static void SetDistanceFromVertex(int vertex, int startDistance = 1)
        {
            numberOfLinks[vertex] = startDistance;
            visited[vertex] = true;
            if (graph[vertex] != null)
            {
                foreach (var linkedVertex in graph[vertex])
                {
                    if (!visited[linkedVertex])
                    {
                        numberOfLinks[linkedVertex] = startDistance + 1; 
                        SetDistanceFromVertex(
                            linkedVertex, startDistance + 1);
                    }
                }
            }
        }

        //static void Main(string[] args)
        //{
        //    ParseConsoleInputAndInitGraph();

        //    Console.WriteLine();
        //    PrintGraph();
        //    FindLongestPathLengthOf(startVertex);

        //    Console.WriteLine();
        //    Console.WriteLine("Max round dance from {0}: {1}", 
        //        (startVertex + minVal), 
        //        maxLength);
        //}
    }
}
