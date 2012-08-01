namespace TicTacToe.WinForms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCreate = new System.Windows.Forms.Button();
            this.numWidth = new System.Windows.Forms.NumericUpDown();
            this.numHeight = new System.Windows.Forms.NumericUpDown();
            this.pnlBoard = new System.Windows.Forms.Panel();
            this.chkComputerStarts = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(230, 71);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 46);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "Create Game";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // numWidth
            // 
            this.numWidth.Location = new System.Drawing.Point(168, 71);
            this.numWidth.Name = "numWidth";
            this.numWidth.Size = new System.Drawing.Size(56, 20);
            this.numWidth.TabIndex = 1;
            this.numWidth.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // numHeight
            // 
            this.numHeight.Location = new System.Drawing.Point(168, 97);
            this.numHeight.Name = "numHeight";
            this.numHeight.Size = new System.Drawing.Size(56, 20);
            this.numHeight.TabIndex = 2;
            this.numHeight.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // pnlBoard
            // 
            this.pnlBoard.Location = new System.Drawing.Point(121, 147);
            this.pnlBoard.Name = "pnlBoard";
            this.pnlBoard.Size = new System.Drawing.Size(259, 208);
            this.pnlBoard.TabIndex = 3;
            // 
            // chkComputerStarts
            // 
            this.chkComputerStarts.AutoSize = true;
            this.chkComputerStarts.Location = new System.Drawing.Point(168, 124);
            this.chkComputerStarts.Name = "chkComputerStarts";
            this.chkComputerStarts.Size = new System.Drawing.Size(101, 17);
            this.chkComputerStarts.TabIndex = 4;
            this.chkComputerStarts.Text = "Computer Starts";
            this.chkComputerStarts.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 428);
            this.Controls.Add(this.chkComputerStarts);
            this.Controls.Add(this.pnlBoard);
            this.Controls.Add(this.numHeight);
            this.Controls.Add(this.numWidth);
            this.Controls.Add(this.btnCreate);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.NumericUpDown numWidth;
        private System.Windows.Forms.NumericUpDown numHeight;
        private System.Windows.Forms.Panel pnlBoard;
        private System.Windows.Forms.CheckBox chkComputerStarts;
    }
}

