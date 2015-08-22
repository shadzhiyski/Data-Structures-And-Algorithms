using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ride_The_Horse
{
    class Cell
    {
        public Cell(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public int Row { get; set; }

        public int Col { get; set; }
    }

    class Program
    {
        static int[,] matrix;
        static bool[,] visited;
        static int numberOfRows = 0;
        static int numberOfColumns = 0;
        static int count = 0;
        static int startRow = 0;
        static int startColumn = 0;

        static void ParseConsoleInput()
        {
            numberOfRows = int.Parse(Console.ReadLine());
            numberOfColumns = int.Parse(Console.ReadLine());
            startRow = int.Parse(Console.ReadLine());
            startColumn = int.Parse(Console.ReadLine());
            matrix = new int[numberOfRows, numberOfColumns];
            visited = new bool[numberOfRows, numberOfColumns];
            count = numberOfRows * numberOfColumns;
        }

        static bool IsElementInMatrix(int row, int col)
        {
            if(row < numberOfRows && col < numberOfColumns
                && row >= 0 && col >= 0) { return true; }

            return false;
        }

        static void SetCellMove(int row, int col, int move)
        {
            if(IsElementInMatrix(row, col))
            {
                if ((matrix[row, col] == 0 || matrix[row, col] >= move)
                    && !(startRow == row && startColumn == col)) 
                { 
                    matrix[row, col] = move; 
                }
            }
        }

        static void FindMovesOfHorse(int row, int col, int move = 0)
        {
            if (IsElementInMatrix(row, col) 
                && matrix[row, col] == move)
            {
                move++;
                SetCellMove(row - 2, col - 1, move);
                SetCellMove(row + 2, col - 1, move);
                SetCellMove(row - 2, col + 1, move);
                SetCellMove(row + 2, col + 1, move);
                SetCellMove(row - 1, col - 2, move);
                SetCellMove(row + 1, col - 2, move);
                SetCellMove(row - 1, col + 2, move);
                SetCellMove(row + 1, col + 2, move);
                PrintMatrix();
                Console.WriteLine();
                FindMovesOfHorse(row - 2, col - 1, move);
                FindMovesOfHorse(row + 2, col - 1, move);
                FindMovesOfHorse(row - 2, col + 1, move);
                FindMovesOfHorse(row + 2, col + 1, move);
                FindMovesOfHorse(row - 1, col - 2, move);
                FindMovesOfHorse(row + 1, col - 2, move);
                FindMovesOfHorse(row - 1, col + 2, move);
                FindMovesOfHorse(row + 1, col + 2, move);
            }
        }

        static void PrintMatrix()
        {
            for (int row = 0; row < numberOfRows; row++)
            {
                for (int col = 0; col < numberOfColumns; col++)
                {
                    Console.Write("{0} ", matrix[row, col]);
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            ParseConsoleInput();
            FindMovesOfHorse(startRow, startColumn);
            PrintMatrix();
        }
    }
}
