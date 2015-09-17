namespace Eight_Queens
{
    using System;

    public class EightQueens
    {
        const int Size = 8;
        static int solutionsFound = 0;
        static bool[,] chessboard = new bool[Size, Size];

        static bool[] attackedCols = new bool[Size];
        static bool[] attackedDiagonalsRight = new bool[2 * Size];
        static bool[] attackedDiagonalsLeft = new bool[2 * Size];

        public static void PutQueens(int row = 0)
        {
            if (row == Size)
            {
                PrintSolution();
            }
            else
            {
                for (int col = 0; col < Size; col++)
                {
                    if (CheckPosition(row, col))
                    {
                        MarkAllAttackedPositions(row, col);
                        PutQueens(row + 1);
                        UnmarkAllAttackedPositions(row, col);
                    }
                }
            }
        }

        private static void PrintSolution()
        {
            Console.WriteLine("Solution: {0}", ++solutionsFound);
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Console.Write("{0} ", chessboard[row, col] ? 'q' : '_');
                }
                Console.WriteLine();
            }
            
            Console.WriteLine();
        }

        private static void UnmarkAllAttackedPositions(int row, int col)
        {
            int left = col - row;
            int right = row + col;
            attackedCols[col] = false;
            attackedDiagonalsLeft[left < 0 || (left == 0 && col - row == 0) ?
                Size - left : left] = false;
            attackedDiagonalsRight[right] = false;
            chessboard[row, col] = false;
        }

        private static void MarkAllAttackedPositions(int row, int col)
        {
            int left = col - row;
            int right = row + col;
            attackedCols[col] = true;
            attackedDiagonalsLeft[left < 0 || (left == 0 && col - row == 0) ? 
                Size - left : left] = true;
            attackedDiagonalsRight[right] = true;
            chessboard[row, col] = true;
        }

        private static bool CheckPosition(int row, int col)
        {
            int left = col - row;
            int right = row + col;
            var positionOccupied =
                attackedCols[col]
                || attackedDiagonalsLeft[left < 0 || (left == 0 && col - row == 0) ?
                Size - left : left]
                || attackedDiagonalsRight[right];
            return !positionOccupied;
        }
    }
}
