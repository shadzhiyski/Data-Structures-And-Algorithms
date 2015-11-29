namespace Line_Inverter
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            int matrixSize = int.Parse(Console.ReadLine());
            char[,] matrix = new char[matrixSize, matrixSize];
            for (int row = 0; row < matrixSize; row++)
            {
                string input = Console.ReadLine();
                Parallel.For(0, input.Length, (i) => 
                {
                    matrix[row, i] = input[i];
                });
                //for (int i = 0; i < input.Length; i++)
                //{
                    
                //}
            }

            int steps = ClearMatrix(matrix);
            Console.WriteLine(steps);
        }

        private static int ClearMatrix(char[,] matrix)
        {
            int stepsCount = 0;

            while(!IsClear(matrix))
            {
                int[] rowInvertionsGain, colInvertionsGain;
                FindInvertionsGains(matrix, out rowInvertionsGain, out colInvertionsGain);
                int bestRow = FindMostInvertionsGainingRow(rowInvertionsGain);
                int bestCol = FindMostInvertionsGainingCol(colInvertionsGain);

                stepsCount++;
                if (bestRow == -1 && bestCol == -1) { return -1; }

                if (bestRow >= bestCol)
                {
                    InvertRow(matrix, bestRow);
                } else
                {
                    InvertCol(matrix, bestCol);
                }
            }

            return stepsCount;
        }

        private static int FindMostInvertionsGainingCol(int[] colInvertionsGain)
        {
            int bestCol = -1, maxInvertionsGain = -1;
            for (int i = 0; i < colInvertionsGain.Length; i++)
            {
                if (colInvertionsGain[i] > maxInvertionsGain)
                {
                    maxInvertionsGain = colInvertionsGain[i];
                    bestCol = i;
                }
            }

            return bestCol;
        }

        private static int FindMostInvertionsGainingRow(int[] rowInvertionsGain)
        {
            int bestRow = -1, maxInvertionsGain = -1;
            for (int i = 0; i < rowInvertionsGain.Length; i++)
            {
                if(rowInvertionsGain[i] > maxInvertionsGain)
                {
                    maxInvertionsGain = rowInvertionsGain[i];
                    bestRow = i;
                }
            }

            return bestRow;
        }

        private static void InvertCol(char[,] matrix, int bestCol)
        {
            Parallel.For(0, matrix.GetLength(0), (row) => 
            {
                if (matrix[row, bestCol] == 'W')
                {
                    matrix[row, bestCol] = 'B';
                }
                else
                {
                    matrix[row, bestCol] = 'W';
                }
            });
            //for (int row = 0; row < matrix.GetLength(0); row++)
            //{
                
            //}
        }

        private static void InvertRow(char[,] matrix, int bestRow)
        {
            Parallel.For(0, matrix.GetLength(1), (col) => 
            {
                if (matrix[bestRow, col] == 'W')
                {
                    matrix[bestRow, col] = 'B';
                }
                else
                {
                    matrix[bestRow, col] = 'W';
                }
            });
            //for (int col = 0; col < matrix.GetLength(1); col++)
            //{
                
            //}
        }

        private static void FindInvertionsGains(
            char[,] matrix, out int[] rowInvertionsGain, out int[] colInvertionsGain)
        {
            rowInvertionsGain = new int[matrix.GetLength(0)];
            colInvertionsGain = new int[matrix.GetLength(1)];
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if(matrix[row, col] == 'W')
                    {
                        rowInvertionsGain[row]++;
                        colInvertionsGain[col]++;
                    }
                    else
                    {
                        rowInvertionsGain[row]--;
                        colInvertionsGain[col]--;
                    }
                }
            }
        }

        private static bool IsClear(char[,] matrix)
        {
            int result = 0;
            Parallel.For(0, matrix.GetLength(0), (row) => 
            {
                Parallel.For(0, matrix.GetLength(1), (col) => 
                {
                    if (matrix[row, col] == 'W')
                    { result++; }
                });
            });
            //for (int row = 0; row < matrix.GetLength(0); row++)
            //{
                //for (int col = 0; col < matrix.GetLength(1); col++)
                //{

                //}
            //}

            return result == 0;
        }
    }
}
