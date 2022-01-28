using System;
using System.IO;
using System.Linq;

namespace SudokuSolver
{
    public class SudokuHelpers
    {
        private static int GRID_SIZE = 0;

        public SudokuHelpers()
        {
            GRID_SIZE = 9;
        }

        public SudokuHelpers(int GridSize)
        {
            GRID_SIZE = GridSize;
        }

        public int[,] ReadPuzzleFromFile(string filePath)
        {
            int[,] puzzle = new int[GRID_SIZE, GRID_SIZE];
            try
            {
                // Create a StreamReader  
                using (StreamReader reader = new StreamReader(filePath))
                {
                    int row = 0;
                    string line;
                    // Read line by line  
                    while ((line = reader.ReadLine()) != null)
                    {
                        int[] value = line.Split(',').Select(i => int.Parse(i)).ToArray();
                        for (int column = 0; column < GRID_SIZE; column++)
                        {
                            puzzle[row, column] = value[column];
                        }
                        row++;
                    }
                }

                return puzzle;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }

            return null;
        }

        public void PrintBoard(int[,] board)
        {
            for (int row = 0; row < GRID_SIZE; row++)
            {
                for (int col = 0; col < GRID_SIZE; col++)
                {
                    Console.Write(board[row, col]);
                }
                Console.WriteLine();
            }
        }
        private static bool IsNumberInRow(int[,] board, int number, int row)
        {
            for (int i = 0; i < GRID_SIZE; i++)
            {
                if (board[row, i] == number)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsNumberInColumn(int[,] board, int number, int column)
        {
            for (int i = 0; i < GRID_SIZE; i++)
            {
                if (board[i, column] == number)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsNumberInBox(int[,] board, int number, int row, int column)
        {
            int localBoxRow = row - row % 3;
            int localBoxColumn = column - column % 3;

            for (int i = localBoxRow; i < localBoxRow + 3; i++)
            {
                for (int j = localBoxColumn; j < localBoxColumn + 3; j++)
                {
                    if (board[i, j] == number)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool IsValidPlacement(int[,] board, int number, int row, int column)
        {
            return !IsNumberInBox(board, number, row, column) && !IsNumberInColumn(board, number, column) && !IsNumberInRow(board, number, row);
        }

        public bool SolvePuzzle(int[,] board)
        {
            for (int row = 0; row < GRID_SIZE; row++)
            {
                for (int column = 0; column < GRID_SIZE; column++)
                {
                    if (board[row, column] == 0)
                    {
                        for (int numberToTry = 1; numberToTry <= GRID_SIZE; numberToTry++)
                        {
                            if (IsValidPlacement(board, numberToTry, row, column))
                            {
                                board[row, column] = numberToTry;

                                if (SolvePuzzle(board))
                                {
                                    return true;
                                }
                                else
                                {
                                    board[row, column] = 0;
                                }
                            }

                        }
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
