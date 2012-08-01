namespace TicTacToe.Logic
{
    using System.Linq;

    public static class GameBrain
    {
        public static bool GetEmptySideMove(BrainResult result, Board board)
        {
            for (int i = 0; i < board.BoardSize;i++)
            {
                // is empty and is not corner
                if (board.BoardArray[i] == 0 && board.Corners.All(c => board.GetIndex(c) != i))
                {
                    result.Moves.Add(board.GetMove(i));
                    return true;
                }
            }

            return false;
        }

        public static bool GetEmptyCornerMove(BrainResult result, Board board)
        {
            var move = board.Corners.FirstOrDefault(m => board.BoardArray[board.GetIndex(m)] == 0);
            if (move != null)
            {
                result.Moves.Add(move);
            }

            return move != null;
        }

        public static bool GetOppositeCornerMove(bool playerPiece, BrainResult result, Board board)
        {
            var corners = board.Corners.ToArray();
            for (int i = 0; i < corners.Count(); i++)
            {
                if (board.IsSamePlayerPiece(!playerPiece, corners[i]))
                {
                    var o = (i % 2 == 0) ? (1) : (-1); // either the one forward or backwards
                    var opp = corners[i + o];
                    if (board.BoardArray[board.GetIndex(opp.X, opp.Y)] == 0)
                    {
                        result.Moves.Add(opp);
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Play the center.
        /// </summary>
        /// <remarks>
        /// If it is the first move of the game, playing on a corner gives the 
        /// other player more opportunities to make a mistake and may therefore
        /// be the better choice.
        /// However, it makes no difference between perfect players.
        /// </remarks>
        public static bool GetCentreMove(BrainResult result, Board board)
        {
            // only if the board has odd dimensions
            if (board.BoardWidth % 2 != 0 && board.BoardHeight % 2 != 0)
            {
                var x = board.BoardWidth / 2;
                var y = board.BoardHeight / 2;
                var i = board.GetIndex(x, y);

                // and if the place is empty
                if (board.BoardArray[i] == 0)
                {
                    result.Moves.Add(board.GetMove(i));
                    return true;
                }
            }

            return false;
        }

        #region Forking moves

        public static bool GetBlockForkingMove(bool playerPiece, BrainResult result, Board board)
        {
            // can the other player create a fork?
            var outerResult = new BrainResult();
            var fork = GetForkingMove(!playerPiece, outerResult, board, true);

            if (fork && outerResult.Moves.Count > 0)
            {
                var tempBoard = board.Clone();
                for (int i = 0; i < board.BoardSize; i++)
                {
                    if (tempBoard.BoardArray[i] == 0)
                    {
                        tempBoard.BoardArray[i] = playerPiece ? 1 : -1;

                        var innerResult = new BrainResult();
                        if (GetWinningMove(playerPiece, 1, innerResult, tempBoard) // am I going to make two in a row
                            && !outerResult.ContainsMove(tempBoard, i)) // this pos won't allow the user to fork
                        {
                            result.Moves.Add(board.GetMove(i));
                            return true;
                        }

                        tempBoard.BoardArray[i] = 0;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Fork by creating an opportunity where you can win in two ways.
        /// </summary>
        public static bool GetForkingMove(bool playerPiece, BrainResult result, Board board, bool getAllForks = false)
        {
            var tempBoard = board.Clone();

            for (int i = 0; i < board.BoardSize; i++)
            {
                if (tempBoard.BoardArray[i] == 0)
                {
                    tempBoard.BoardArray[i] = playerPiece ? 1 : -1;

                    var outerResult = new BrainResult();
                    if (GetWinningMove(playerPiece, 2, outerResult, tempBoard))
                    {
                        var moveThatMakesTwoInARow = board.GetMove(i);
                        var innerResult = new BrainResult();
                        var notWin = !GetWinningMove(!playerPiece, 1, innerResult, tempBoard);
                        var notFork = !GetOppositionForkingMove(playerPiece, tempBoard, moveThatMakesTwoInARow);
                        if (notWin && notFork)
                        {
                            result.Moves.Add(moveThatMakesTwoInARow);
                            if (!getAllForks)
                            {
                                return true;
                            }
                        }
                    }

                    tempBoard.BoardArray[i] = 0;
                }
            }

            return getAllForks && result.Moves.Count > 0;
        }

        public static bool GetOppositionForkingMove(bool playerPiece, Board board, Move move)
        {
            var tempBoard = board.Clone();

            // switch last to the correct player
            tempBoard.BoardArray[board.GetIndex(move.X, move.Y)] = playerPiece ? -1 : 1;

            for (int i = 0; i < board.BoardSize; i++)
            {
                if (tempBoard.BoardArray[i] == 0)
                {
                    tempBoard.BoardArray[i] = playerPiece ? 1 : -1;

                    var result = new BrainResult();
                    if (GetWinningMove(!playerPiece, 2, result, tempBoard))
                    {
                        return true;
                    }

                    tempBoard.BoardArray[i] = 0;
                }
            }

            return false;
        }

        #endregion

        #region Find moves that could result in a win

        /// <summary>
        /// If a player has two in a row, play the third to get three in a row.
        /// </summary>
        public static bool GetWinningMove(bool playerPiece, int minResults, BrainResult result, Board board)
        {
            if (GetStraightWinningMove(playerPiece, minResults, result, board)
                || GetDiagonalWinningMove(playerPiece, result, board))
            {
                if (result.Moves.Count >= minResults)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool GetStraightWinningMove(bool playerPiece, int minResults, BrainResult result, Board board)
        {
            return GetStraightWinningMove(true, playerPiece, minResults, result, board) // down
                   || GetStraightWinningMove(false, playerPiece, minResults, result, board); // accross
        }

        public static bool GetDiagonalWinningMove(bool playerPiece, BrainResult result, Board board)
        {
            return GetDiagonalWinningMove(false, playerPiece, result, board) // top left to bottom right
                   || GetDiagonalWinningMove(true, playerPiece, result, board); // top right to bottom left
        }

        public static bool GetStraightWinningMove(bool vert, bool playerPiece, int minResults, BrainResult result, Board board)
        {
            int outMax = vert ? board.BoardWidth : board.BoardSize;
            int outStp = vert ? 1 : board.BoardWidth;
            int inMax = vert ? board.BoardSize : board.BoardWidth;
            int inStp = vert ? board.BoardWidth : 1;

            int notPlayerPieceCount = 0;

            for (int i = 0; i < outMax; i += outStp)
            {
                int x = 0;
                for (int j = 0; j < inMax; j += inStp)
                {
                    if (!board.IsSamePlayerPiece(playerPiece, i + j))
                    {
                        notPlayerPieceCount++;

                        if (notPlayerPieceCount > 1)
                        {
                            notPlayerPieceCount = 0;
                            break;
                        }

                        x = vert ? j / board.BoardWidth : j;
                    }
                }

                int y = vert ? i : i / board.BoardWidth;
                var move = vert ? new Move(y, x) : new Move(x, y);
                if (notPlayerPieceCount == 1 && board.IsValidMove(move.X, move.Y))
                {
                    result.Moves.Add(move);

                    if (result.Moves.Count >= minResults)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool GetDiagonalWinningMove(bool reverse, bool playerPiece, BrainResult result, Board board)
        {
            // setup
            int notPlayerPieceCount = 0;

            // get direction values
            int num = reverse ? board.BoardWidth - 1 : 0;
            int step = reverse ? -1 : 1;
            int x = 0, y = 0;
            // find lines
            for (int i = num; i < board.BoardSize - num; i += board.BoardWidth + step)
            {
                if (!board.IsSamePlayerPiece(playerPiece, i))
                {
                    notPlayerPieceCount++;
                    if (notPlayerPieceCount > 1)
                    {
                        notPlayerPieceCount = 0;
                        break;
                    }

                    // get the xy co-ords
                    x = reverse ? i % board.BoardWidth : i / board.BoardWidth;
                    y = i / board.BoardHeight;
                }
            }

            var move = new Move(x, y);
            if (notPlayerPieceCount == 1 && board.IsValidMove(x, y))
            {
                // we found a place
                result.Moves.Add(move);

                return true;
            }

            // no place
            return false;
        }

        #endregion

        public static bool CheckForWin(bool playerPiece, BrainResult result, Board board)
        {
            return CheckForWin(true, playerPiece, result, board) // down
                   || CheckForWin(false, playerPiece, result, board); // accross
        }

        public static bool CheckForWin(bool vert, bool playerPiece, BrainResult result, Board board)
        {
            int outMax = vert ? board.BoardWidth : board.BoardSize;
            int outStp = vert ? 1 : board.BoardWidth;
            int inMax = vert ? board.BoardSize : board.BoardWidth;
            int inStp = vert ? board.BoardWidth : 1;

            int size = vert ? board.BoardHeight : board.BoardWidth;

            BrainResult tempResult = new BrainResult();
            for (int i = 0; i < outMax; i += outStp)
            {
                for (int j = 0; j < inMax; j += inStp)
                {
                    if (board.IsSamePlayerPiece(playerPiece, i + j))
                    {
                        int x = vert ? j / board.BoardWidth : j;
                        int y = vert ? i : i / board.BoardWidth;
                        var move = vert ? new Move(y, x) : new Move(x, y);

                        tempResult.Moves.Add(move);
                    }
                }

                if (tempResult.Moves.Count == size)
                {
                    break;
                }
            }

            if (tempResult.Moves.Count == size)
            {
                foreach (var move in tempResult.Moves)
                {
                    result.Moves.Add(move);
                }

                return true;
            }

            return false;
        }
    }
}