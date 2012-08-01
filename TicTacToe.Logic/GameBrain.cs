namespace TicTacToe.Logic
{
    using System.Linq;

    public static class GameBrain
    {
        #region 1. & 2. Winning Moves
 
        /// <summary>
        /// If a player has two in a row, play the third to get three in a row.
        /// </summary>
        public static bool GetWinningMove(bool playerPiece, int minResults, BrainResult result, Board board)
        {
            return (GetStraightWinningMove(playerPiece, minResults, result, board) // staight win
                    || GetDiagonalWinningMove(playerPiece, result, board)) // diagonal win
                   && result.Moves.Count >= minResults; // are moves
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

        /// <summary>
        /// Find a place where, if the current player place his piece there, 
        /// will result in a win for that player.
        /// </summary>
        /// <param name="vert">
        /// True if we are searching vertically, otherwise False if we are 
        /// searching horizontaly.
        /// </param>
        /// <param name="playerPiece">
        /// Flag for representing the diferent players.
        /// </param>
        /// <param name="minResults">
        /// The number of wins that need to be found before returning.
        /// </param>
        /// <param name="result">
        /// The result set for storing the values.
        /// </param>
        /// <param name="board">
        /// The board to use.
        /// </param>
        /// <returns>
        /// True if at least <paramref name="minResults"/> possible wins were 
        /// found.
        /// </returns>
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

        #region 3. & 4. Forking Moves

        /// <summary>
        /// Find the places that will force the other player to defend, put his
        /// piece in a place that is not going to create a fork.
        /// </summary>
        /// <param name="playerPiece">
        /// Flag for representing the diferent players.
        /// </param>
        /// <param name="result">
        /// The result set for storing the values.
        /// </param>
        /// <param name="board">
        /// The board to use.
        /// </param>
        /// <returns>
        /// True if at least 1 possible wins was found.
        /// </returns>
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
        /// <param name="playerPiece">
        /// Flag for representing the diferent players.
        /// </param>
        /// <param name="result">
        /// The result set for storing the values.
        /// </param>
        /// <param name="board">
        /// The board to use.
        /// </param>
        /// <param name="getAllForks">
        /// Are we to get all the forks or stop after the first one is found.
        /// </param>
        /// <returns>
        /// True if at least 1 possible forking move was found.
        /// </returns>
        public static bool GetForkingMove(bool playerPiece, BrainResult result, Board board, bool getAllForks = false)
        {
            var tempBoard = board.Clone();

            for (int i = 0; i < board.BoardSize; i++)
            {
                if (tempBoard.BoardArray[i] == 0)
                {
                    var piece = playerPiece ? 1 : -1;

                    tempBoard.BoardArray[i] = piece;

                    var outerResult = new BrainResult();
                    if (GetWinningMove(playerPiece, 2, outerResult, tempBoard))
                    {
                        var m = board.GetMove(i);
                        var innerResult = new BrainResult();
                        var notWin = !GetWinningMove(!playerPiece, 1, innerResult, tempBoard);

                        // switch last to the other player
                        var index = board.GetIndex(m.X, m.Y);
                        tempBoard.BoardArray[index] = piece * -1;

                        var notFork = !GetOppositionForkingMove(playerPiece, tempBoard);
                        if (notWin && notFork)
                        {
                            result.Moves.Add(m);
                            if (!getAllForks)
                            {
                                return true;
                            }
                        }

                        // switch back
                        tempBoard.BoardArray[index] = piece;
                    }

                    tempBoard.BoardArray[i] = 0;
                }
            }

            return getAllForks && result.Moves.Count > 0;
        }

        /// <summary>
        /// Using the given board, see if the other play can make any forks.
        /// </summary>
        /// <param name="playerPiece">
        /// Flag for representing the diferent players.
        /// </param>
        /// <param name="board">
        /// The board to use.
        /// </param>
        /// <returns>
        /// True if at least 1 possible wins was found.
        /// </returns>
        public static bool GetOppositionForkingMove(bool playerPiece, Board board)
        {
            var tempBoard = board.Clone();

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

        #region 5. Play Centre

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

        #endregion

        #region 6. Play Opposite Corner

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

        #endregion

        #region 7. Play Empty Corner
        
        public static bool GetEmptyCornerMove(BrainResult result, Board board)
        {
            var move = board.Corners.FirstOrDefault(m => board.BoardArray[board.GetIndex(m)] == 0);
            if (move != null)
            {
                result.Moves.Add(move);
            }

            return move != null;
        }

        #endregion

        #region 8. Play Empty Side

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