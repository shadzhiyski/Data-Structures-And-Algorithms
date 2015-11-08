namespace Knights_Tour
{
    using System;

    class Program
    {
        static int[,] board;
        static int[,] offset = new int[,] 
        {
            {  2,  1 },
            {  2, -1 },
            { -2,  1 },
            { -2, -1 },
            {  1,  2 },
            {  1, -2 },
            { -1,  2 },
            { -1, -2 }
        };

        static void Main(string[] args)
        {
            int boardSize = int.Parse(Console.ReadLine());
            board = new int[boardSize, boardSize];
            
            FindKnightsTour();
            PrintBoard(board);
        }

        private static void PrintBoard(int[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(" {0}", board[i, j]);
                }
                Console.WriteLine();
            }
        }

        private static void FindKnightsTour(int row = 0, int col = 0, int move = 0)
        {
            if(board[row, col] == 0)// cell is not visited
            {
                board[row, col] = ++move;

                int minMoves = 9;
                int nextCellRow = 0, nextCellCol = 0;
                for (int i = 0; i < 8; i++)
                {
                    int adjacentRow = row + offset[i, 0];
                    int adjacentCol = col + offset[i, 1];
                    if (InRange(adjacentRow, adjacentCol)
                        && board[adjacentRow, adjacentCol] == 0)
                    {
                        int moves = CalcMovesOfCell(adjacentRow, adjacentCol);
                        if (minMoves > moves)
                        {
                            minMoves = moves;
                            nextCellRow = adjacentRow;
                            nextCellCol = adjacentCol;
                        }
                    }
                }

                FindKnightsTour(nextCellRow, nextCellCol, move);
            }
        }

        private static int CalcMovesOfCell(int row, int col)
        {
            int count = 0;
            for (int i = 0; i < 8; i++)
            {
                int adjacentRow = row + offset[i, 0];
                int adjacentCol = col + offset[i, 1];
                if (InRange(adjacentRow, adjacentCol)
                    && board[adjacentRow, adjacentCol] == 0)
                { count++; }
            }

            return count;
        }

        private static bool InRange(int rowMove, int colMove)
        {
            return rowMove >= 0
                && colMove >= 0
                && rowMove < board.GetLength(0)
                && colMove < board.GetLength(1);
        }
    }
}
