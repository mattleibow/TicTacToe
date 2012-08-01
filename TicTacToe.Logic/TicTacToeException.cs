namespace TicTacToe.Logic
{
    using System;
    using System.Collections.Generic;

    public class TicTacToeException : Exception
    {
        public TicTacToeException(TicTacToeReason reason)
            : this(Messages[reason])
        {
        }

        public TicTacToeException(string message)
            : base(message)
        {
        }

        public TicTacToeException()
        {
        }

        public static Dictionary<TicTacToeReason, string> Messages = new Dictionary<TicTacToeReason, string>
            { { TicTacToeReason.GameInProgress, "Unable to switch game while in progress." } };
    }

    public enum TicTacToeReason
    {
        GameInProgress
    }
}