namespace TicTacToe.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Board
    {
        #region Constructors and Destructors

        public Board(int[] boardArray = null, int boardWidth = 3, int boardHeight = 3)
        {
            this.BoardWidth = boardWidth;
            this.BoardHeight = boardHeight;
            this.BoardSize = this.BoardWidth * this.BoardHeight;
            this.BoardArray = new int[this.BoardSize];

            if (boardArray != null)
            {
                Array.Copy(boardArray, this.BoardArray, this.BoardSize);
            }

            this.Corners = new[] { // opposites together
                                     new Move(0, 0), // top left
                                     new Move(this.BoardWidth - 1, this.BoardHeight - 1), // bottom right
                                     
                                     new Move(this.BoardWidth - 1, 0), // top right
                                     new Move(0, this.BoardHeight - 1) // bottom left
                                 };
        }

        #endregion

        #region Public Properties

        public int[] BoardArray { get; private set; }

        public int BoardHeight { get; private set; }

        public int BoardSize { get; private set; }

        public int BoardWidth { get; private set; }

        public IEnumerable<Move> Corners { get; private set; }

        #endregion

        #region Public Methods and Operators

        public Board Clone()
        {
            var clone = new Board(null, this.BoardWidth, this.BoardHeight);
            Array.Copy(this.BoardArray, clone.BoardArray, this.BoardSize);
            return clone;
        }

        public int GetIndex(Move move)
        {
            return this.GetIndex(move.X, move.Y);
        }

        public int GetIndex(int x, int y)
        {
            return y * this.BoardWidth + x;
        }

        public Move GetMove(int i)
        {
            return new Move(i % this.BoardWidth, i / this.BoardWidth);
        }

        /// <summary>
        /// Returns True if the piece at the specified position belongs to the 
        /// player specified.
        /// </summary>
        /// <param name="playerPiece">The player type to use.</param>
        /// <param name="i">The position.</param>
        /// <returns>
        /// True if this is the specified player's piece. Otherwise False.
        /// </returns>
        public bool IsSamePlayerPiece(bool playerPiece, int i)
        {
            return (this.BoardArray[i] > 0 && playerPiece) // positive for human
                   || (this.BoardArray[i] < 0 && !playerPiece); // negative for AI
        }

        public bool IsSamePlayerPiece(bool playerPiece, Move move)
        {
            return this.IsSamePlayerPiece(playerPiece, this.GetIndex(move.X, move.Y));
        }

        public bool IsValidMove(int x, int y)
        {
            return x >= 0 && x < this.BoardWidth // x range
                   && y >= 0 && y < this.BoardHeight // y range
                   && this.BoardArray[this.GetIndex(x, y)] == 0; // empty
        }

        public bool IsValidMove(int i)
        {
            return i >= 0 && i < this.BoardSize // in range
                   && this.BoardArray[i] == 0; // empty
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(String.Format("Size: {0} X {1}", this.BoardWidth, this.BoardHeight));

            for (int i = 0; i < this.BoardSize; i++)
            {
                if (i % this.BoardWidth == 0)
                {
                    sb.AppendLine();
                }

                sb.Append(this.BoardArray[i].ToString("#").PadLeft(3) + " | ");
            }

            return sb.ToString();
        }

        #endregion
    }
}