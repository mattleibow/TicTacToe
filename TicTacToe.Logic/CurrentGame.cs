namespace TicTacToe.Logic
{
    using TicTacToe.Model.Android;

    public class CurrentGame
    {
        private Player player;

        private GameState gameState;

        private int moveNumber;

        public Board Board { get; private set; }

        public CurrentGame()
            : this(new Board())
        {
        }

        public CurrentGame(Board board)
        {
            this.Board = board;
            this.moveNumber = 1;
            this.gameState = GameState.NotStarted;
        }

        public bool MakeMove(int x, int y, bool isPlayer)
        {
            return this.MakeMove(this.Board.GetIndex(x, y), isPlayer);
        }

        public bool MakeMove(int i, bool isPlayer)
        {
            if (!this.CanPlay || !this.Board.IsValidMove(i))
            {
                return false;
            }

            this.Board.BoardArray[i] = (isPlayer ? 1 : -1) * this.moveNumber++;

            //this.CheckForWin();

            return true;
        }

        public bool GetNewMove(BrainResult result)
        {
            return GameBrain.GetWinningMove(false, 1, result, this.Board) // <-------- 1. Can we win?
                   || GameBrain.GetWinningMove(true, 1, result, this.Board) // <------ 2. Can we stop you from winning?
                   || GameBrain.GetForkingMove(false, result, this.Board) // <-------- 3. Can we create a fork?
                   || GameBrain.GetBlockForkingMove(true, result, this.Board) // <---- 4. (a) Can we force you not to make a fork?
                   || GameBrain.GetForkingMove(true, result, this.Board) // <---------    (b) Can we stop your fork?
                   || GameBrain.GetCentreMove(result, this.Board) // <---------------- 5. Play in the centre
                   || GameBrain.GetOppositeCornerMove(false, result, this.Board) // <- 6. Play the opposite corner if the opponent is in the corner.
                   || GameBrain.GetEmptyCornerMove(result, this.Board) // <----------- 7. Play in a corner square.
                   || GameBrain.GetEmptySideMove(result, this.Board); // <------------ 8. Play in a middle square on any of the 4 sides.
        }

        public bool CheckForWin(BrainResult result)
        {
            return GameBrain.CheckForWin(false, result, this.Board)
                   || GameBrain.CheckForWin(true, result, this.Board);
        }

        private bool CanPlay
        {
            get
            {
                return this.gameState == GameState.InProgress || this.gameState == GameState.NotStarted;
            }
        }
    }
}
