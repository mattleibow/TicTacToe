namespace TicTacToe.Logic.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BlockingMove
    {
        [TestMethod]
        public void GetBlockingMoveTestMethod()
        {
            var boards = new[] { new[] { -1, -1, 0, 0, 1, 1, 0, 0, 0 }, //
                                 new[] { 0, 0, 1, -1, 1, 0, 0, 0, 0 } };

            var positions = new[]
                {
                    new[] { 2, 0 }, // 
                    new[] { 0, 2 }, //
                };

            for (var i = 0; i < boards.Length; i++)
            {
                var board = boards[i];

                var g = new CurrentGame(new Board(board));

                var brainResult = new BrainResult();
                var canMove = g.GetNewMove(brainResult);

                try
                {
                    Assert.AreEqual(positions[i][0], brainResult.Moves[0].X);
                    Assert.AreEqual(positions[i][1], brainResult.Moves[0].Y);
                    Assert.AreEqual(true, canMove);
                }
                catch
                {
                    UnitTestHelpers.PrintBoard(board, brainResult.Moves[0].X, brainResult.Moves[0].Y, canMove);

                    throw;
                }
            }
        }
    }
}