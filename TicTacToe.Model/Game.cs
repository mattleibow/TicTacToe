namespace TicTacToe.Model.Android
{
    public class Game
    {
        public Game()
        {
            this.Id = -1;
            this.UserId = -1;
            this.GameState = GameState.NotStarted;
            this.Moves = new int[9];
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public GameState GameState { get; set; }

        public int[] Moves { get; set; }
    }
}