using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TicTacToe.Logic;

namespace TicTacToe.WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Button[] buttons;

        private CurrentGame game;

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var width = (int)numWidth.Value;
            var height = (int)numHeight.Value;

            buttons = new Button[width * height];
            game = new CurrentGame(new Board(null, width, height));
            pnlBoard.Controls.Clear();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var i = (width * y) + x;
                    buttons[i] = new Button { Width = 32, Height = 32, Left = x * 32, Top = y * 32 };
                    buttons[i].Click += delegate
                        {
                            if (game.MakeMove(i, true))
                            {
                                this.ComputerPlay();
                            }
                        };
                    pnlBoard.Controls.Add(buttons[i]);
                }
            }

            if (chkComputerStarts.Checked)
            {
                this.ComputerPlay();
            }
        }

        private void ComputerPlay()
        {
            var result = new BrainResult();
            if (this.game.GetNewMove(result))
            {
                var move = result.Moves.First();
                this.game.MakeMove(move.X, move.Y, false);
            }

            this.UpdateUI();
        }

        private void UpdateUI()
        {
            for (int i = 0; i < game.Board.BoardSize; i++)
            {
                var piece = game.Board.BoardArray[i];
                if (piece == 0) buttons[i].Text = string.Empty;
                else if (piece > 0) buttons[i].Text = "X";
                else buttons[i].Text = "O";
            }

            BrainResult result = new BrainResult();
            if (game.CheckForWin(result))
            {
                this.Text = string.Join(" : ", result.Moves.Select(x => x.ToString()));
            }
        }
    }
}
