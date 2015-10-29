namespace Areas_In_Matrix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static char[,] graph;

        public static void Main()
        {
            graph = ReadGraph();
            int countAreas;
            var areasCounts = FindNumberOfAreas(out countAreas);

            Console.WriteLine("Areas: {0}", countAreas);
            foreach (var areaCount in areasCounts)
            {
                Console.WriteLine("Letter {0} -> {1}", areaCount.Key, areaCount.Value);
            }
        }

        private static char[,] ReadGraph()
        {
            var rowsCount = int.Parse(Console.ReadLine());
            var colsCount = int.Parse(Console.ReadLine());
            var graph = new char[rowsCount, colsCount];
            for (int i = 0; i < rowsCount; i++)
            {
                var row = Console.ReadLine();
                for (int j = 0; j < colsCount; j++)
                {
                    graph[i, j] = row[j];
                }
            }

            return graph;
        }

        public static Dictionary<char, int> FindNumberOfAreas(out int countAreas)
        {
            countAreas = 0;
            var visited = new bool[graph.GetLength(0), graph.GetLength(1)];
            var areasCounts = new Dictionary<char, int>();
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = 0; j < graph.GetLength(1); j++)
                {
                    if (!visited[i, j])
                    {
                        TraverseArea(ref visited, graph[i, j], i, j);
                        if (!areasCounts.ContainsKey(graph[i, j]))
                        {
                            areasCounts.Add(graph[i, j], 0);
                        }

                        countAreas++;
                        areasCounts[graph[i, j]]++;
                    }
                }
            }

            return areasCounts;
        }

        private static void TraverseArea(ref bool[,] visited, char area, int rowIndex, int colIndex)
        {
            if (InRange(rowIndex, colIndex))
            {
                if (!visited[rowIndex, colIndex] && graph[rowIndex, colIndex].Equals(area))
                {
                    visited[rowIndex, colIndex] = true;
                    TraverseArea(ref visited, area, rowIndex + 1, colIndex);
                    TraverseArea(ref visited, area, rowIndex, colIndex + 1);
                    TraverseArea(ref visited, area, rowIndex - 1, colIndex);
                    TraverseArea(ref visited, area, rowIndex, colIndex - 1);
                }
            }
        }

        private static bool InRange(int i, int j)
        {
            if ((i >= 0)
                && (j >= 0)
                && (i < graph.GetLength(0))
                && (j < graph.GetLength(1)))
            { return true; }

            return false;
        }
    }
}
