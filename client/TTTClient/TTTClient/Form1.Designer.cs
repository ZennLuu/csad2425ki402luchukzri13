namespace TTTClient
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tt0 = new System.Windows.Forms.PictureBox();
            this.tt8 = new System.Windows.Forms.PictureBox();
            this.tt1 = new System.Windows.Forms.PictureBox();
            this.tt7 = new System.Windows.Forms.PictureBox();
            this.tt2 = new System.Windows.Forms.PictureBox();
            this.tt6 = new System.Windows.Forms.PictureBox();
            this.tt3 = new System.Windows.Forms.PictureBox();
            this.tt5 = new System.Windows.Forms.PictureBox();
            this.tt4 = new System.Windows.Forms.PictureBox();
            this.Grid = new System.Windows.Forms.PictureBox();
            this.loadGame = new System.Windows.Forms.Button();
            this.saveGame = new System.Windows.Forms.Button();
            this.panelInGame = new System.Windows.Forms.Panel();
            this.panelPlay = new System.Windows.Forms.Panel();
            this.newGame = new System.Windows.Forms.Button();
            this.gameMode = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.playGame = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tt0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tt8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tt1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tt7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tt2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tt6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tt3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tt5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tt4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            this.panelInGame.SuspendLayout();
            this.panelPlay.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Command";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(160, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Response";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Choose COM-Port";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(100, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(149, 21);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComPortsBox_SelectedIndexChanged);
            // 
            // tt0
            // 
            this.tt0.Enabled = false;
            this.tt0.Location = new System.Drawing.Point(12, 192);
            this.tt0.Name = "tt0";
            this.tt0.Size = new System.Drawing.Size(75, 75);
            this.tt0.TabIndex = 6;
            this.tt0.TabStop = false;
            this.tt0.Click += new System.EventHandler(this.tt0_Click);
            // 
            // tt8
            // 
            this.tt8.Enabled = false;
            this.tt8.Location = new System.Drawing.Point(172, 352);
            this.tt8.Name = "tt8";
            this.tt8.Size = new System.Drawing.Size(75, 75);
            this.tt8.TabIndex = 14;
            this.tt8.TabStop = false;
            this.tt8.Click += new System.EventHandler(this.tt8_Click);
            // 
            // tt1
            // 
            this.tt1.Enabled = false;
            this.tt1.Location = new System.Drawing.Point(92, 192);
            this.tt1.Name = "tt1";
            this.tt1.Size = new System.Drawing.Size(75, 75);
            this.tt1.TabIndex = 7;
            this.tt1.TabStop = false;
            this.tt1.Click += new System.EventHandler(this.tt1_Click);
            // 
            // tt7
            // 
            this.tt7.Enabled = false;
            this.tt7.Location = new System.Drawing.Point(92, 352);
            this.tt7.Name = "tt7";
            this.tt7.Size = new System.Drawing.Size(75, 75);
            this.tt7.TabIndex = 13;
            this.tt7.TabStop = false;
            this.tt7.Click += new System.EventHandler(this.tt7_Click);
            // 
            // tt2
            // 
            this.tt2.Enabled = false;
            this.tt2.Location = new System.Drawing.Point(172, 192);
            this.tt2.Name = "tt2";
            this.tt2.Size = new System.Drawing.Size(75, 75);
            this.tt2.TabIndex = 8;
            this.tt2.TabStop = false;
            this.tt2.Click += new System.EventHandler(this.tt2_Click);
            // 
            // tt6
            // 
            this.tt6.Enabled = false;
            this.tt6.Location = new System.Drawing.Point(12, 352);
            this.tt6.Name = "tt6";
            this.tt6.Size = new System.Drawing.Size(75, 75);
            this.tt6.TabIndex = 12;
            this.tt6.TabStop = false;
            this.tt6.Click += new System.EventHandler(this.tt6_Click);
            // 
            // tt3
            // 
            this.tt3.Enabled = false;
            this.tt3.Location = new System.Drawing.Point(12, 272);
            this.tt3.Name = "tt3";
            this.tt3.Size = new System.Drawing.Size(75, 75);
            this.tt3.TabIndex = 9;
            this.tt3.TabStop = false;
            this.tt3.Click += new System.EventHandler(this.tt3_Click);
            // 
            // tt5
            // 
            this.tt5.Enabled = false;
            this.tt5.Location = new System.Drawing.Point(172, 272);
            this.tt5.Name = "tt5";
            this.tt5.Size = new System.Drawing.Size(75, 75);
            this.tt5.TabIndex = 11;
            this.tt5.TabStop = false;
            this.tt5.Click += new System.EventHandler(this.tt5_Click);
            // 
            // tt4
            // 
            this.tt4.Enabled = false;
            this.tt4.Location = new System.Drawing.Point(92, 272);
            this.tt4.Name = "tt4";
            this.tt4.Size = new System.Drawing.Size(75, 75);
            this.tt4.TabIndex = 10;
            this.tt4.TabStop = false;
            this.tt4.Click += new System.EventHandler(this.tt4_Click);
            // 
            // Grid
            // 
            this.Grid.Location = new System.Drawing.Point(12, 192);
            this.Grid.Name = "Grid";
            this.Grid.Size = new System.Drawing.Size(235, 235);
            this.Grid.TabIndex = 15;
            this.Grid.TabStop = false;
            // 
            // loadGame
            // 
            this.loadGame.Location = new System.Drawing.Point(4, 32);
            this.loadGame.Name = "loadGame";
            this.loadGame.Size = new System.Drawing.Size(116, 23);
            this.loadGame.TabIndex = 16;
            this.loadGame.Text = "Load";
            this.loadGame.UseVisualStyleBackColor = true;
            this.loadGame.Click += new System.EventHandler(this.loadGame_Click);
            // 
            // saveGame
            // 
            this.saveGame.Location = new System.Drawing.Point(120, 4);
            this.saveGame.Name = "saveGame";
            this.saveGame.Size = new System.Drawing.Size(116, 23);
            this.saveGame.TabIndex = 17;
            this.saveGame.Text = "Save";
            this.saveGame.UseVisualStyleBackColor = true;
            this.saveGame.Click += new System.EventHandler(this.saveGame_Click);
            // 
            // panelInGame
            // 
            this.panelInGame.Controls.Add(this.saveGame);
            this.panelInGame.Controls.Add(this.newGame);
            this.panelInGame.Location = new System.Drawing.Point(12, 156);
            this.panelInGame.Name = "panelInGame";
            this.panelInGame.Size = new System.Drawing.Size(236, 32);
            this.panelInGame.TabIndex = 18;
            // 
            // panelPlay
            // 
            this.panelPlay.Controls.Add(this.playGame);
            this.panelPlay.Controls.Add(this.label6);
            this.panelPlay.Controls.Add(this.loadGame);
            this.panelPlay.Controls.Add(this.gameMode);
            this.panelPlay.Location = new System.Drawing.Point(8, 128);
            this.panelPlay.Name = "panelPlay";
            this.panelPlay.Size = new System.Drawing.Size(244, 60);
            this.panelPlay.TabIndex = 19;
            // 
            // newGame
            // 
            this.newGame.Location = new System.Drawing.Point(0, 4);
            this.newGame.Name = "newGame";
            this.newGame.Size = new System.Drawing.Size(116, 23);
            this.newGame.TabIndex = 18;
            this.newGame.Text = "New Game";
            this.newGame.UseVisualStyleBackColor = true;
            this.newGame.Click += new System.EventHandler(this.newGame_Click);
            // 
            // gameMode
            // 
            this.gameMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gameMode.FormattingEnabled = true;
            this.gameMode.Items.AddRange(new object[] {
            "Man vs. Man",
            "Man vs. AI",
            "AI vs. AI"});
            this.gameMode.Location = new System.Drawing.Point(100, 4);
            this.gameMode.Name = "gameMode";
            this.gameMode.Size = new System.Drawing.Size(141, 21);
            this.gameMode.TabIndex = 20;
            this.gameMode.SelectedIndexChanged += new System.EventHandler(this.gameMode_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Game mode:";
            // 
            // playGame
            // 
            this.playGame.Location = new System.Drawing.Point(124, 32);
            this.playGame.Name = "playGame";
            this.playGame.Size = new System.Drawing.Size(116, 23);
            this.playGame.TabIndex = 21;
            this.playGame.Text = "Play";
            this.playGame.UseVisualStyleBackColor = true;
            this.playGame.Click += new System.EventHandler(this.playGame_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 435);
            this.Controls.Add(this.panelPlay);
            this.Controls.Add(this.panelInGame);
            this.Controls.Add(this.tt0);
            this.Controls.Add(this.tt8);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.tt1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tt7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tt2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tt6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tt3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tt5);
            this.Controls.Add(this.tt4);
            this.Controls.Add(this.Grid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Tic-Tac-Toe";
            ((System.ComponentModel.ISupportInitialize)(this.tt0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tt8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tt1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tt7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tt2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tt6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tt3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tt5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tt4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.panelInGame.ResumeLayout(false);
            this.panelPlay.ResumeLayout(false);
            this.panelPlay.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.PictureBox tt4;
        private System.Windows.Forms.PictureBox tt5;
        private System.Windows.Forms.PictureBox tt3;
        private System.Windows.Forms.PictureBox tt6;
        private System.Windows.Forms.PictureBox tt2;
        private System.Windows.Forms.PictureBox tt7;
        private System.Windows.Forms.PictureBox tt1;
        private System.Windows.Forms.PictureBox tt8;
        private System.Windows.Forms.PictureBox tt0;
        private System.Windows.Forms.PictureBox Grid;
        private System.Windows.Forms.Button loadGame;
        private System.Windows.Forms.Button saveGame;
        private System.Windows.Forms.Panel panelInGame;
        private System.Windows.Forms.Panel panelPlay;
        private System.Windows.Forms.Button newGame;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox gameMode;
        private System.Windows.Forms.Button playGame;
    }
}

