namespace TicTacToe.Logic.UnitTests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CheckForWin
    {
        private static void Check(int[][] boards, int[][] sizes, Move[][] positions, bool vertical)
        {
            for (var i = 0; i < boards.Length; i++)
            {
                var board = boards[i];

                var g = new CurrentGame(new Board(board, sizes[i][0], sizes[i][1]));

                var brainResult = new BrainResult();
                var canMove = GameBrain.CheckForWin(vertical, false, brainResult, g.Board);

                try
                {
                    Assert.AreEqual(true, canMove);

                    for (int x = 0; x < positions[i].Length; x++)
                    {
                        Assert.AreEqual(positions[i][x].X, brainResult.Moves[x].X);
                        Assert.AreEqual(positions[i][x].Y, brainResult.Moves[x].Y);
                    }
                }
                catch
                {
                    UnitTestHelpers.PrintBoard(board, brainResult.Moves[0].X, brainResult.Moves[0].Y, canMove, sizes[i][0]);

                    throw;
                }
            }
        }

        /// <summary>
        /// If a player has two in a row, play the third to get three in a row.
        /// </summary>
        [TestMethod]
        public void GetHorizontalWinTestMethod()
        {
            var boards = new[] { new[] { -1, -1, -1, -1,  0, 0, 0, 0,  0, 0, 0, 0 }, // xxxx
                                 //
                                 new[] { -1, -1, -1, 0, 0, 0, 0, 0, 0 }, // xxx
                                 new[] { 0, 0, 0, -1, -1, -1, 0, 0, 0 }, // xxx
                                 new[] { 0, 0, 0, 0, 0, 0, -1, -1, -1 }, // xxx
                               };

            var positions = new[] { new[] { new Move(0, 0), new Move(1, 0), new Move(2, 0), new Move(3, 0) }, // xxxx
                                    //
                                    new[] { new Move(0, 0), new Move(1, 0), new Move(2, 0) }, // xxx
                                    //
                                    new[] { new Move(0, 1), new Move(1, 1), new Move(2, 1) }, // xxx
                                    //
                                    new[] { new Move(0, 2), new Move(1, 2), new Move(2, 2) } // xxx
                                  };

            var sizes = new[]
                {
                    new[] { 4, 3 }, 
                    //
                    new[] { 3, 3 }, 
                    new[] { 3, 3 }, 
                    new[] { 3, 3 }
                };

            Check(boards, sizes, positions, false);
        }

        /// <summary>
        /// If a player has two in a row, play the third to get three in a row.
        /// </summary>
        [TestMethod]
        public void GetVerticalWinTestMethod()
        {
            var boards = new[]
                {
                    // 4x4
                    new[] { -1, 0, 0, 0,  -1, 0, 0, 0,  -1, 0, 0, 0,  -1, 0, 0, 0 }, 
                    new[] {   0, -1, 0, 0,   0, -1, 0, 0,   0, -1, 0, 0   }, 
                    //
                    // 3x3
                    new[] { -1, 0, 0,  -1, 0, 0,  -1, 0, 0 }, 
                    new[] { 0, -1, 0,  0, -1, 0,  0, -1, 0 }, 
                    new[] { 0, 0, -1,  0, 0, -1,  0, 0, -1 }, 
                };

            var positions = new[] { new[] { new Move(0, 0), new Move(0,1), new Move(0,2), new Move(0,3) }, // xxxx
                                    //
                                    new[] { new Move(1, 0), new Move(1, 1), new Move(1, 2) }, // xxx
                                    //
                                    new[] { new Move(0, 0), new Move(0, 1), new Move(0, 2) }, // xxx
                                    //
                                    new[] { new Move(1, 0), new Move(1, 1), new Move(1, 2) }, // xxx
                                    //
                                    new[] { new Move(2, 0), new Move(2, 1), new Move(2, 2) } // xxx
                                  };
            var sizes = new[]
                {
                    new[] { 4, 4 }, 
                    new[] { 4, 3 }, 
                    //
                    new[] { 3, 3 }, 
                    new[] { 3, 3 }, 
                    new[] { 3, 3 }  
                };

           Check(boards, sizes, positions, true);
        }

        /// <summary>
        /// If a player has two in a row, play the third to get three in a row.
        /// </summary>
        [TestMethod]
        public void GetDiagonalWinTestMethod()
        {
            var boards = new[]
                {
                    new[] { 1, 0, 0, 0,  0, 1, 0, 0,  0, 0, 1, 0,  0, 0, 0, 0 }, 
                    //
                    new[] { 1, 0, 0, 0, 1, 0, 0, 0, 0 }, 
                    new[] { 1, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                    new[] { 0, 0, 0, 0, 1, 0, 0, 0, 1 }, 
                    //
                    new[] { 0, 0, 1, 0, 1, 0, 0, 0, 0 }, 
                    new[] { 0, 0, 1, 0, 0, 0, 1, 0, 0 }, 
                    new[] { 0, 0, 0, 0, 1, 0, 1, 0, 0 }  
                };

            var positions = new[]
                {
                    new[] { 3, 3 }, 
                    //
                    new[] { 2, 2 }, 
                    new[] { 1, 1 }, 
                    new[] { 0, 0 }, 
                    //
                    new[] { 0, 2 }, 
                    new[] { 1, 1 }, 
                    new[] { 2, 0 },  
                };

            var sizes = new[]
                {
                    new[] { 4, 4 }, 
                    //
                    new[] { 3, 3 }, 
                    new[] { 3, 3 }, 
                    new[] { 3, 3 }, 
                    //
                    new[] { 3, 3 }, 
                    new[] { 3, 3 }, 
                    new[] { 3, 3 }  
                };

            for (var i = 0; i < boards.Length; i++)
            {
                var g = new CurrentGame(new Board(boards[i], sizes[i][0], sizes[i][1]));
                var brainResult = new BrainResult();
                var canMove = GameBrain.GetDiagonalWinningMove(true, brainResult, g.Board);

                try
                {
                    Assert.AreEqual(positions[i][0], brainResult.Moves[0].X);
                    Assert.AreEqual(positions[i][1], brainResult.Moves[0].Y);
                    Assert.AreEqual(true, canMove);

                    UnitTestHelpers.PrintBoard(boards[i], brainResult.Moves[0].X, brainResult.Moves[0].Y, canMove, sizes[i][0]);
                }
                catch
                {
                    UnitTestHelpers.PrintBoard(boards[i], brainResult.Moves[0].X, brainResult.Moves[0].Y, canMove, sizes[i][0]);

                    throw;
                }
            }
        }

    }
}