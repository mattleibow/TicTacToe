namespace TicTacToe.Logic.UnitTests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class WinningMove
    {
        /// <summary>
        /// If a player has two in a row, play the third to get three in a row.
        /// </summary>
        [TestMethod]
        public void GetHorizontalWinningMoveTestMethod()
        {
            var boards = new[] { new[] { -1, -1, 0, -1,  0, 0, 0, 0,  0, 0, 0, 0 }, // xxx_
                                 //
                                 new[] { -1, -1, 0, 0, 0, 0, 0, 0, 0 }, // xx_
                                 new[] { 0, 0, 0, -1, -1, 0, 0, 0, 0 }, // xx_
                                 new[] { 0, 0, 0, 0, 0, 0, -1, -1, 0 }, // xx_
                                 //
                                 new[] { 0, -1, -1, 0, 0, 0, 0, 0, 0 }, // _xx
                                 new[] { 0, 0, 0, 0, -1, -1, 0, 0, 0 }, // _xx
                                 new[] { 0, 0, 0, 0, 0, 0, 0, -1, -1 }, // _xx
                                 //
                                 new[] { -1, 0, -1, 0, 0, 0, 0, 0, 0 }, // x_x
                                 new[] { 0, 0, 0, -1, 0, -1, 0, 0, 0 }, // x_x 
                                 new[] { 0, 0, 0, 0, 0, 0, -1, 0, -1 } // x_x
                               };

            var positions = new[]
                {
                    new[] { 2, 0 }, // xx_
                    //
                    new[] { 2, 0 }, // xx_
                    new[] { 2, 1 }, // xx_
                    new[] { 2, 2 }, // xx_
                    //
                    new[] { 0, 0 }, // _xx
                    new[] { 0, 1 }, // _xx
                    new[] { 0, 2 }, // _xx
                    //
                    new[] { 1, 0 }, // x_x
                    new[] { 1, 1 }, // x_x 
                    new[] { 1, 2 }  // x_x
                };

            var sizes = new[]
                {
                    new[] { 4, 3 }, 
                    //
                    new[] { 3, 3 }, 
                    new[] { 3, 3 }, 
                    new[] { 3, 3 }, 
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
                var board = boards[i];

                var g = new CurrentGame(new Board(board, sizes[i][0], sizes[i][1]));

                var brainResult = new BrainResult();
                var canMove = g.GetStraightWinningMove(false, false, 1, brainResult);

                try
                {
                    Assert.AreEqual(positions[i][0], brainResult.Moves[0].X);
                    Assert.AreEqual(positions[i][1], brainResult.Moves[0].Y);
                    Assert.AreEqual(true, canMove);
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
        public void GetVerticalWinningMoveTestMethod()
        {
            var boards = new[]
                {
                    // 4x4
                    new[] { 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 }, 
                    new[] { 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 }, 
                    new[] { 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0 }, 
                    //
                    // 3x3
                    new[] { 1, 0, 0, 1, 0, 0, 0, 0, 0 }, 
                    new[] { 0, 1, 0, 0, 1, 0, 0, 0, 0 }, 
                    new[] { 0, 0, 1, 0, 0, 1, 0, 0, 0 }, 
                    //
                    new[] { 1, 0, 0, 0, 0, 0, 1, 0, 0 }, 
                    new[] { 0, 1, 0, 0, 0, 0, 0, 1, 0 }, 
                    new[] { 0, 0, 1, 0, 0, 0, 0, 0, 1 }, 
                    //
                    new[] { 0, 0, 0, 1, 0, 0, 1, 0, 0 }, 
                    new[] { 0, 0, 0, 0, 1, 0, 0, 1, 0 }, 
                    new[] { 0, 0, 0, 0, 0, 1, 0, 0, 1 }  
                };

            var positions = new[]
                {
                    new[] { 0, 3 }, 
                    new[] { 1, 2 }, 
                    new[] { 0, 2 }, 
                    //
                    new[] { 0, 2 }, 
                    new[] { 1, 2 }, 
                    new[] { 2, 2 }, 
                    //
                    new[] { 0, 1 }, 
                    new[] { 1, 1 }, 
                    new[] { 2, 1 }, 
                    //
                    new[] { 0, 0 }, 
                    new[] { 1, 0 }, 
                    new[] { 2, 0 }  
                };

            var sizes = new[]
                {
                    new[] { 4, 4 }, 
                    new[] { 4, 3 }, 
                    new[] { 3, 4 }, 
                    //
                    new[] { 3, 3 }, 
                    new[] { 3, 3 }, 
                    new[] { 3, 3 }, 
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
                var canMove = g.GetStraightWinningMove(true, true, 1, brainResult);

                try
                {
                    Assert.AreEqual(true, canMove);
                    Assert.AreEqual(positions[i][0], brainResult.Moves[0].X);
                    Assert.AreEqual(positions[i][1], brainResult.Moves[0].Y);
                }
                catch
                {
                    Console.WriteLine("Test: " + i);
                    var x = canMove ? brainResult.Moves[0].X : -1;
                    var y = canMove ? brainResult.Moves[0].Y : -1;
                    UnitTestHelpers.PrintBoard(boards[i], x, y, canMove, sizes[i][0]);

                    throw;
                }
            }
        }

        /// <summary>
        /// If a player has two in a row, play the third to get three in a row.
        /// </summary>
        [TestMethod]
        public void GetDiagonalWinningMoveTestMethod()
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
                var canMove = g.GetDiagonalWinningMove(true, brainResult);

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