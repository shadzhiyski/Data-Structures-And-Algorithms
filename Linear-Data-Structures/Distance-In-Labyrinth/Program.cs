namespace Distance_In_Labyrinth
{
    using System;

    class Program
    {
        static void CreateDefaultLabyrinth(out int[,] labyrinth)
        {
            labyrinth = new int[,] 
            {
                {0, 0, 0,-1, 0,-1},
                {0,-1, 0,-1, 0,-1},
                {0, 0,-1, 0,-1, 0},
                {0,-1, 0, 0, 0, 0},
                {0, 0, 0,-1,-1, 0},
                {0, 0, 0,-1, 0,-1}
            };
        }

        static void TraverseLabyrinth(int[,] labyrinth, int startRow, int startCol)
        {
            int len = 1;
            if (labyrinth[startRow, startCol] == -1) 
            { 
                throw new Exception("Cannot start from full cell."); 
            }

            labyrinth[startRow, startCol] = -2;

            if (startRow - 1 >= 0)
            {
                if (labyrinth[startRow - 1, startCol] == 0) 
                {
                    TraverseLabyrinth(labyrinth, startRow - 1, startCol, len);
                }
            }
            if (startRow + 1 < labyrinth.GetLength(0))
            { 
                if (labyrinth[startRow + 1, startCol] == 0) 
                {
                    TraverseLabyrinth(labyrinth, startRow + 1, startCol, len);
                }
            }
            if (startCol - 1 >= 0)
            {
                if (labyrinth[startRow, startCol - 1] == 0) 
                {
                    TraverseLabyrinth(labyrinth, startRow, startCol - 1, len);
                }
            }
            if (startCol + 1 < labyrinth.GetLength(0))
            {
                if (labyrinth[startRow, startCol + 1] == 0) 
                {
                    TraverseLabyrinth(labyrinth, startRow, startCol + 1, len);
                }
            }
        }

        static void TraverseLabyrinth(
            int[,] labyrinth, int startRow, int startCol, int len)
        {
            if (len == 0) { labyrinth[startRow, startCol] = -2; len++; }
            else { labyrinth[startRow, startCol] = len++; }

            if (startRow - 1 >= 0)
            {
                if (labyrinth[startRow - 1, startCol] > 0 
                    && labyrinth[startRow - 1, startCol] < labyrinth[startRow, startCol])
                {
                    labyrinth[startRow, startCol] = labyrinth[startRow - 1, startCol] + 1;
                }
            }
            if (startRow + 1 < labyrinth.GetLength(0))
            {
                if (labyrinth[startRow + 1, startCol] > 0
                    && labyrinth[startRow + 1, startCol] < labyrinth[startRow, startCol])
                {
                    labyrinth[startRow, startCol] = labyrinth[startRow + 1, startCol] + 1;
                }
            }
            if (startCol - 1 >= 0)
            {
                if (labyrinth[startRow, startCol - 1] > 0
                    && labyrinth[startRow, startCol - 1] < labyrinth[startRow, startCol])
                {
                    labyrinth[startRow, startCol] = labyrinth[startRow, startCol - 1] + 1;
                }
            }
            if (startCol + 1 < labyrinth.GetLength(0))
            {
                if (labyrinth[startRow, startCol + 1] > 0
                    && labyrinth[startRow, startCol + 1] < labyrinth[startRow, startCol])
                {
                    labyrinth[startRow, startCol] = labyrinth[startRow, startCol + 1] + 1;
                }
            }

            if (startRow - 1 >= 0)
            {
                if (labyrinth[startRow - 1, startCol] == 0) 
                {
                    TraverseLabyrinth(labyrinth, startRow - 1, startCol, len);
                }
            }
            if (startRow + 1 < labyrinth.GetLength(0))
            { 
                if (labyrinth[startRow + 1, startCol] == 0) 
                {
                    TraverseLabyrinth(labyrinth, startRow + 1, startCol, len);
                }
            }
            if (startCol - 1 >= 0)
            {
                if (labyrinth[startRow, startCol - 1] == 0) 
                {
                    TraverseLabyrinth(labyrinth, startRow, startCol - 1, len);
                }
            }
            if (startCol + 1 < labyrinth.GetLength(0))
            {
                if (labyrinth[startRow, startCol + 1] == 0) 
                {
                    TraverseLabyrinth(labyrinth, startRow, startCol + 1, len);
                }
            }
        }

        static void PrintLabyrinth(int[,] labyrinth, int startRow, int startCol) 
        {
            for (int i = 0; i < labyrinth.GetLength(0); i++)
            {
                for (int j = 0; j < labyrinth.GetLength(1); j++)
                {
                    if (labyrinth[i, j] == 0) 
                    { 
                        Console.Write("  u "); 
                    }
                    else if (labyrinth[i, j] == -2)
                    {
                        Console.Write("  * ");
                    }
                    else if (labyrinth[i, j] == -1)
                    {
                        Console.Write("  x ");
                    }
                    else
                    {
                        Console.Write(" {0:00} ", labyrinth[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            int[,] labyrinth;
            int startRow = 2, startCol = 1;
            CreateDefaultLabyrinth(out labyrinth);

            TraverseLabyrinth(labyrinth, startRow, startCol);

            PrintLabyrinth(labyrinth, startRow, startCol);
        }
    }
}
