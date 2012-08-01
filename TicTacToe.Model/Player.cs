namespace TicTacToe.Model.Android
{
    public class Player
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }

        public PlayerLevel PlayerLevel { get; set; }
    }
}