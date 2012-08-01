namespace TicTacToe.Logic.UnitTests
{
    using System;

    public class UnitTestHelpers
    {
        public static void PrintBoard(int[] board, int x, int y, bool canMove)
        {
            var width = Math.Sqrt(board.Length);

            PrintBoard(board, x, y, canMove, width);
        }

        public static void PrintBoard(int[] board, int x, int y, bool canMove, double width)
        {
            Console.WriteLine("Can Move: " + canMove);
            Console.WriteLine("Position: {0}, {1}", x, y);
            Console.Write("Board: ");

            for (int i = 0; i < board.Length; i++)
            {
                if ((int)(i % width) == 0)
                {
                    Console.WriteLine();
                }

                Console.Write(board[i].ToString("#").PadLeft(3) + " | ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}