namespace Salaries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static List<int>[] graph;
        static long[] employeeSalary;

        static void Main(string[] args)
        {
            List<int> managerIndices;
            graph = ReadGraph(out managerIndices);
            var salariesCount = CountSalaries(ref graph, managerIndices);
            Console.WriteLine("{0}", salariesCount);
        }

        public static long CountSalaries(ref List<int>[] graph, List<int> managerIndices)
        {
            var visited = new bool[graph.Length];
            long salariesCount = 0;
            for (int i = 0; i < managerIndices.Count; i++)
            {
                CountSalariesDFS(ref visited, managerIndices[i]);
            }
            for (int i = 0; i < employeeSalary.Length; i++)
            {
                salariesCount += employeeSalary[i];
            }

            return salariesCount;
        }

        private static long CountSalariesDFS(ref bool[] visited, int node)
        {
            long salary = 0;
            if (!visited[node]) 
            {
                visited[node] = true;
                for (int i = 0; i < graph[node].Count; i++)
                {
                    salary += CountSalariesDFS(ref visited, graph[node][i]);
                }

                employeeSalary[node] = salary == 0 ? 1 : salary;
                return employeeSalary[node];
            }

            return employeeSalary[node];
        }

        private static List<int>[] ReadGraph(out List<int> managerIndices)
        {
            var size = int.Parse(Console.ReadLine());
            var graph = new List<int>[size];
            employeeSalary = new long[size];
            var nodes = new HashSet<int>(Enumerable.Range(0, size));
            for (int i = 0; i < size; i++)
            {
                var row = Console.ReadLine();
                graph[i] = new List<int>();
                for (int j = 0; j < size; j++)
                {
                    if (row[j] == 'Y')
                    {
                        graph[i].Add(j);
                        nodes.Remove(j);
                    }
                }
            }

            managerIndices = nodes.ToList();
            return graph;
        }
    }
}
