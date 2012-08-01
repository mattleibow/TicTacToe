namespace TicTacToe.Logic
{
    using System.Collections.Generic;
    using System.Linq;

    public class BrainResult
    {
        public BrainResult()
        {
            this.Moves = new List<Move>();
        }

        public List<Move> Moves { get; private set; }

        public bool ContainsMove(Board board, int i)
        {
            return this.Moves.Any(x => board.GetIndex(x.X, x.Y) == i);
        }

        public override string ToString()
        {
            var moves = Moves.Select(x => x.ToString()).ToArray();
            return string.Format("({0}) : {1}", Moves.Count, string.Join(", ", moves));
        }
    }
}