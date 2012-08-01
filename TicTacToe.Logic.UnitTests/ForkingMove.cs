using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TicTacToe.Logic.UnitTests
{
    [TestClass]
    public class ForkingMove
    {
        [TestMethod]
        public void GetForkingMoveTestMethod()
        {
            var boards = new[] { new[] { 1, 1, 0, 0, -1, 0, 0, 0, -1 }, //
                                 new[] { 1, -1, 0, 0, 0, 1, 0, 0, -1 }, //
                                 //
                                 new[] { -1, -1, 0, 0, 1, 0, 0, 0, 1 }, //
                                 new[] { -1, 1, 0, 0, 0, -1, 0, 0, 1 }, //
                                 new[] { 1, 0, 0, 0, -1, 0, 0, 0, 1 } };

            var positions = new[] { new[] { 2, 0 }, // 
                                    new[] { 1, 2 }, //
                                    //
                                    new[] { 0, 1 }, // 
                                    new[] { 0, 1 }, // 
                                    new[] { -1, -1 }, //
                                  };

            for (var i = 0; i < boards.Length; i++)
            {
                var board = boards[i];

                var g = new CurrentGame(new Board(board));

                var brainResult = new BrainResult();
                var canMove = g.GetForkingMove(false, brainResult);

                try
                {
                    var x = canMove ? brainResult.Moves[0].X : -1;
                    var y = canMove ? brainResult.Moves[0].Y : -1;

                    var expected = positions[i][0] != -1 && positions[i][1] != -1;
                    Assert.AreEqual(expected, canMove, "Can Move");
                    Assert.AreEqual(positions[i][0], x, "X");
                    Assert.AreEqual(positions[i][1], y, "Y");

                    UnitTestHelpers.PrintBoard(board, x, y, canMove);
                }
                catch
                {
                    Console.WriteLine("Test: " + i);

                    var x = canMove ? brainResult.Moves[0].X : -1;
                    var y = canMove ? brainResult.Moves[0].Y : -1;
                    UnitTestHelpers.PrintBoard(board, x, y, canMove);

                    throw;
                }
            }
        }

        [TestMethod]
        public void GetBlockingForkingMoveTestMethod()
        {
            var boards = new[] { new[] { 1, 0, 0, 0, -1, 0, 0, 0, 1 } };

            var positions = new[] { new[] { 1, 0 } };

            for (var i = 0; i < boards.Length; i++)
            {
                var board = boards[i];

                var g = new CurrentGame(new Board(board));

                var brainResult = new BrainResult();
                var canMove = g.GetBlockForkingMove(false, brainResult);

                try
                {
                    Assert.AreEqual(true, canMove, "Can Move");
                    var x = brainResult.Moves[0].X;
                    Assert.AreEqual(positions[i][0], x, "X");
                    var y = brainResult.Moves[0].Y;
                    Assert.AreEqual(positions[i][1], y, "Y");
                    UnitTestHelpers.PrintBoard(board, x, y, canMove);
                }
                catch
                {
                    var x = canMove ? brainResult.Moves[0].X : -1;
                    var y = canMove ? brainResult.Moves[0].Y : -1;
                    UnitTestHelpers.PrintBoard(board, x, y, canMove);

                    throw;
                }
            }
        }

        [TestMethod]
        public void GetOpositionForkingMoveTestMethod()
        {
            var boards = new[] { new[] { 1, 1, 0, 0, -1, 0, 0, 0, -1 }, //
                                 new[] { 1, -1, 0, 0, 0, 1, 0, 0, -1 }, //
                                 //
                                 new[] { -1, -1, 0, 0, 1, 0, 0, 0, 1 }, //
                                 new[] { -1, 1, 0, 0, 0, -1, 0, 0, 1 }, //
                                 new[] { 1, 0, 0, 0, -1, 0, 0, 0, 1 } };

            var positions = new[] { new[] { 0, 1 }, // 
                                    new[] { 0, 1 }, //
                                    //
                                    new[] { 2, 0 }, // 
                                    new[] { 1, 2 }, // 
                                    new[] { 2, 0 }, //
                                  };

            for (var i = 0; i < boards.Length; i++)
            {
                var board = boards[i];

                var g = new CurrentGame(new Board(board));

                var brainResult = new BrainResult();
                var canMove = g.GetForkingMove(true, brainResult);

                try
                {
                    Assert.AreEqual(true, canMove, "Can Move");
                    var x = brainResult.Moves[0].X;
                    Assert.AreEqual(positions[i][0], x, "X");
                    var y = brainResult.Moves[0].Y;
                    Assert.AreEqual(positions[i][1], y, "Y");
                    UnitTestHelpers.PrintBoard(board, x, y, canMove);
                }
                catch
                {
                    var x = canMove ? brainResult.Moves[0].X : -1;
                    var y = canMove ? brainResult.Moves[0].Y : -1;
                    UnitTestHelpers.PrintBoard(board, x, y, canMove);

                    throw;
                }
            }
        }
    }
}
