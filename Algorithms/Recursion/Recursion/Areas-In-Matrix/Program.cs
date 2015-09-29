//#define DEBUG_MODE

namespace Areas_In_Matrix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    class Program
    {
        private static int countSteps = 0;

        private static int countSizeOfArea = 0;
        private static OrderedBag<AreaInfo> areas =
            new OrderedBag<AreaInfo>();
        private static Queue<Tuple<int, int>> cellsOfNotCrawledAreas =
            new Queue<Tuple<int, int>>();
        private static Queue<Tuple<int, int>> cellsOfNotCrawledWalls =
            new Queue<Tuple<int, int>>();
        private static string[,] matrix = new string[,]
		{
            {"1", " ", "*", "2", " ", " ", " ", " " },
            {" ", "*", "*", " ", " ", " ", " ", " " },
            {" ", "*", "*", "*", "*", "*", "*", " " },
            {"*", "*", "*", "*", "4", " ", "*", "*" },
            {"3", " ", "*", " ", " ", " ", " ", " " },
			{" ", " ", "*", "*", "*", "*", "*", "*" },
            {" ", " ", " ", " ", "*", "6", "*", "5" },
            {" ", " ", "*", " ", "*", " ", "*", " " }
        };
        private static bool[,] visited = new bool[matrix.GetLength(0), matrix.GetLength(1)];

        public static void CrawlMatrix(int row, int col)
        {
            cellsOfNotCrawledAreas.Enqueue(new Tuple<int, int>(row, col));
            while (true)
            {
                if (cellsOfNotCrawledAreas.Count > 0)
                {
                    var landCell = cellsOfNotCrawledAreas.Dequeue();
                    if (!IsVisited(landCell.Item1, landCell.Item2))
                    {
                        int indexOfArea = 0, startRow = 0, startCol = 0;

                        CrawlArea(
                            landCell.Item1, landCell.Item2, 
                            ref countSizeOfArea, 
                            ref indexOfArea,
                            ref startRow,
                            ref startCol);
                        areas.Add(
                            new AreaInfo 
                            { 
                                Index = indexOfArea, 
                                Size = countSizeOfArea,
                                StartRow = startRow,
                                StartCol = startCol
                            });
                        countSizeOfArea = 0;
                    }
                }
                else if (cellsOfNotCrawledWalls.Count > 0)
                {
                    var wallCell = cellsOfNotCrawledWalls.Dequeue();
                    if (!IsVisited(wallCell.Item1, wallCell.Item2))
                    {
                        CrawlWall(wallCell.Item1, wallCell.Item2);
                    }
                }
                else break;
            }
        }

        public static void CrawlArea(
            int row, int col, ref int countSizeOfArea, 
            ref int indexOfArea, 
            ref int startRow, 
            ref int startCol)
        {
            countSteps++;
            if (InRange(row, col) && !IsVisited(row, col))
            {
                if (!IsWall(row, col))
                {
                    if (!matrix[row, col].Equals(" "))
                    {
                        indexOfArea = int.Parse(matrix[row, col]);
                        startRow = row;
                        startCol = col;
                    }

                    #if DEBUG_MODE
                        var val = matrix[row, col];
                        matrix[row, col] = "+";
                        PrintMatrix(ref matrix, row, col);
                        matrix[row, col] = val;
                    #endif

                    countSizeOfArea++;
                    
                    visited[row, col] = true;
                    CrawlArea(row, col - 1, ref countSizeOfArea, ref indexOfArea, ref startRow, ref startCol); // left
                    CrawlArea(row - 1, col, ref countSizeOfArea, ref indexOfArea, ref startRow, ref startCol); // up
                    CrawlArea(row, col + 1, ref countSizeOfArea, ref indexOfArea, ref startRow, ref startCol); // right
                    CrawlArea(row + 1, col, ref countSizeOfArea, ref indexOfArea, ref startRow, ref startCol);
                }
                else
                {
                    cellsOfNotCrawledWalls.Enqueue(new Tuple<int, int>(row, col));
                }
            }
        }

        public static void CrawlWall(int row, int col)
        {
            countSteps++;
            if (InRange(row, col) && !IsVisited(row, col))
            {
                #if DEBUG_MODE
                    var val = matrix[row, col]; 
                    matrix[row, col] = "+";
                    PrintMatrix(ref matrix, row, col);
                    matrix[row, col] = val;
                #endif
                
                if (IsWall(row, col))
                {
                    visited[row, col] = true;
                    CrawlWall(row, col - 1); // left
                    CrawlWall(row - 1, col); // up
                    CrawlWall(row, col + 1); // right
                    CrawlWall(row + 1, col);
                }
                else
                {
                    cellsOfNotCrawledAreas.Enqueue(new Tuple<int, int>(row, col));
                }
            }
        }

        private static bool InRange(int row, int col)
        {
            if (row >= 0 && row < matrix.GetLength(0)
                    && col >= 0 && col < matrix.GetLength(1))
            {
                return true;
            }

            return false;
        }

        private static bool IsWall(int row, int col)
        {
            if (matrix[row, col].Equals("*")) { return true; }

            return false;
        }

        private static bool IsVisited(int row, int col)
        {
            if (visited[row, col]) { return true; }

            return false;
        }

        private static void PrintMatrix(ref string[,] perimeter, int currRow, int currCol)
        {
            for (int row = 0; row < perimeter.GetLength(0); row++)
            {
                for (int col = 0; col < perimeter.GetLength(1); col++)
                {
                    if (currRow == row && currCol == col)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write("{0} ", perimeter[row, col]);

                    Console.ResetColor();
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.ReadKey();
        }

        private static void PrintMatrix(ref string[,] perimeter)
        {
            for (int row = 0; row < perimeter.GetLength(0); row++)
            {
                for (int col = 0; col < perimeter.GetLength(1); col++)
                {
                    Console.Write("{0} ", perimeter[row, col]);
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public static void Main()
        {
            CrawlMatrix(0, 0);
            PrintMatrix(ref matrix);
            Console.WriteLine("Steps: {0}", countSteps);
            Console.WriteLine();

            foreach (var area in areas)
            {
                Console.WriteLine("Area #{0} at ({1}, {2}), size {3}", 
                    area.Index, area.StartRow, area.StartCol, area.Size);
            }
        }
    }
}
