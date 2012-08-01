using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.Model.Android
{
    public enum GameState
    {
        NotStarted = 0,

        InProgress = 1,

        Won = 2,
        Lost = 3,
        Draw = 4
    }
}
