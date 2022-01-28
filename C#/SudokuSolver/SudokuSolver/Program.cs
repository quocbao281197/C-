using System;

namespace SudokuSolver
{
    class Program
    {
        private const int GRID_SIZE = 9;
        private const string FILE_PATH = "puzzle.txt";

        public static void Main(string[] args)
        {
            SudokuHelpers sudokuHelpers = new SudokuHelpers(GRID_SIZE);
            int[,] board = sudokuHelpers.ReadPuzzleFromFile(FILE_PATH);

            if (sudokuHelpers.SolvePuzzle(board))
            {
                Console.WriteLine("Solved Puzzle!!!");
                sudokuHelpers.PrintBoard(board);
            }
            else
            {
                Console.WriteLine("Unsolvable Puzzle!!!");
            }
        }
    }
}
