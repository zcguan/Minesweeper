namespace Minesweeper {
    partial class Minesweeper {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Minesweeper));
            this.Restart = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuLv = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuLvEasy = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuLvIntermediate = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuLvHard = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuLvCus = new System.Windows.Forms.ToolStripMenuItem();
            this.LBdimension = new System.Windows.Forms.Label();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.LBmineCount = new System.Windows.Forms.Label();
            this.LBtimer = new System.Windows.Forms.Label();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.StartPanel = new System.Windows.Forms.Panel();
            this.BTcustom = new System.Windows.Forms.Button();
            this.BThard = new System.Windows.Forms.Button();
            this.BTintermediate = new System.Windows.Forms.Button();
            this.BTeasy = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.TopPanel.SuspendLayout();
            this.StartPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Restart
            // 
            this.Restart.Enabled = false;
            this.Restart.Location = new System.Drawing.Point(160, 3);
            this.Restart.Name = "Restart";
            this.Restart.Size = new System.Drawing.Size(100, 30);
            this.Restart.TabIndex = 1;
            this.Restart.Text = "Restart";
            this.Restart.UseVisualStyleBackColor = true;
            this.Restart.Click += new System.EventHandler(this.Restart_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuLv});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(426, 28);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuLv
            // 
            this.MenuLv.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuLvEasy,
            this.MenuLvIntermediate,
            this.MenuLvHard,
            this.MenuLvCus});
            this.MenuLv.Enabled = false;
            this.MenuLv.Name = "MenuLv";
            this.MenuLv.Size = new System.Drawing.Size(55, 24);
            this.MenuLv.Text = "Level";
            // 
            // MenuLvEasy
            // 
            this.MenuLvEasy.Name = "MenuLvEasy";
            this.MenuLvEasy.Size = new System.Drawing.Size(169, 26);
            this.MenuLvEasy.Text = "Easy";
            this.MenuLvEasy.Click += new System.EventHandler(this.easyToolStripMenuItem1_Click);
            // 
            // MenuLvIntermediate
            // 
            this.MenuLvIntermediate.Name = "MenuLvIntermediate";
            this.MenuLvIntermediate.Size = new System.Drawing.Size(169, 26);
            this.MenuLvIntermediate.Text = "Intermediate";
            this.MenuLvIntermediate.Click += new System.EventHandler(this.intermediateToolStripMenuItem_Click);
            // 
            // MenuLvHard
            // 
            this.MenuLvHard.Name = "MenuLvHard";
            this.MenuLvHard.Size = new System.Drawing.Size(169, 26);
            this.MenuLvHard.Text = "Hard";
            this.MenuLvHard.Click += new System.EventHandler(this.hardToolStripMenuItem_Click);
            // 
            // MenuLvCus
            // 
            this.MenuLvCus.Name = "MenuLvCus";
            this.MenuLvCus.Size = new System.Drawing.Size(169, 26);
            this.MenuLvCus.Text = "Custom";
            this.MenuLvCus.Click += new System.EventHandler(this.customToolStripMenuItem_Click);
            // 
            // LBdimension
            // 
            this.LBdimension.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LBdimension.Location = new System.Drawing.Point(287, 445);
            this.LBdimension.Name = "LBdimension";
            this.LBdimension.Size = new System.Drawing.Size(139, 23);
            this.LBdimension.TabIndex = 4;
            this.LBdimension.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.LBmineCount);
            this.TopPanel.Controls.Add(this.LBtimer);
            this.TopPanel.Controls.Add(this.Restart);
            this.TopPanel.Location = new System.Drawing.Point(12, 31);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(402, 35);
            this.TopPanel.TabIndex = 5;
            // 
            // LBmineCount
            // 
            this.LBmineCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LBmineCount.Font = new System.Drawing.Font("Arial Black", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBmineCount.ForeColor = System.Drawing.Color.Red;
            this.LBmineCount.Location = new System.Drawing.Point(20, 8);
            this.LBmineCount.Name = "LBmineCount";
            this.LBmineCount.Size = new System.Drawing.Size(60, 20);
            this.LBmineCount.TabIndex = 3;
            this.LBmineCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBtimer
            // 
            this.LBtimer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LBtimer.Font = new System.Drawing.Font("Arial Black", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBtimer.ForeColor = System.Drawing.Color.Red;
            this.LBtimer.Location = new System.Drawing.Point(311, 8);
            this.LBtimer.Name = "LBtimer";
            this.LBtimer.Size = new System.Drawing.Size(60, 20);
            this.LBtimer.TabIndex = 2;
            this.LBtimer.Text = "00";
            this.LBtimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Timer
            // 
            this.Timer.Interval = 1000;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // StartPanel
            // 
            this.StartPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StartPanel.Controls.Add(this.BTcustom);
            this.StartPanel.Controls.Add(this.BThard);
            this.StartPanel.Controls.Add(this.BTintermediate);
            this.StartPanel.Controls.Add(this.BTeasy);
            this.StartPanel.Location = new System.Drawing.Point(118, 93);
            this.StartPanel.Name = "StartPanel";
            this.StartPanel.Size = new System.Drawing.Size(200, 313);
            this.StartPanel.TabIndex = 6;
            this.StartPanel.VisibleChanged += new System.EventHandler(this.StartPanel_VisibleChanged);
            // 
            // BTcustom
            // 
            this.BTcustom.Location = new System.Drawing.Point(30, 245);
            this.BTcustom.Name = "BTcustom";
            this.BTcustom.Size = new System.Drawing.Size(141, 52);
            this.BTcustom.TabIndex = 3;
            this.BTcustom.Text = "Custom";
            this.BTcustom.UseVisualStyleBackColor = true;
            this.BTcustom.Click += new System.EventHandler(this.BTcustom_Click);
            // 
            // BThard
            // 
            this.BThard.Location = new System.Drawing.Point(30, 173);
            this.BThard.Name = "BThard";
            this.BThard.Size = new System.Drawing.Size(141, 52);
            this.BThard.TabIndex = 2;
            this.BThard.Text = "Hard";
            this.BThard.UseVisualStyleBackColor = true;
            this.BThard.Click += new System.EventHandler(this.BThard_Click);
            // 
            // BTintermediate
            // 
            this.BTintermediate.Location = new System.Drawing.Point(30, 91);
            this.BTintermediate.Name = "BTintermediate";
            this.BTintermediate.Size = new System.Drawing.Size(141, 52);
            this.BTintermediate.TabIndex = 1;
            this.BTintermediate.Text = "Intermediate";
            this.BTintermediate.UseVisualStyleBackColor = true;
            this.BTintermediate.Click += new System.EventHandler(this.BTintermediate_Click);
            // 
            // BTeasy
            // 
            this.BTeasy.Location = new System.Drawing.Point(32, 14);
            this.BTeasy.Name = "BTeasy";
            this.BTeasy.Size = new System.Drawing.Size(141, 52);
            this.BTeasy.TabIndex = 0;
            this.BTeasy.Text = "Easy";
            this.BTeasy.UseVisualStyleBackColor = true;
            this.BTeasy.Click += new System.EventHandler(this.BTeasy_Click);
            // 
            // Minesweeper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 467);
            this.Controls.Add(this.StartPanel);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.LBdimension);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Minesweeper";
            this.Text = "Minesweeper";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Minesweeper_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Minesweeper_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Minesweeper_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Minesweeper_MouseUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.TopPanel.ResumeLayout(false);
            this.StartPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Restart;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuLv;
        private System.Windows.Forms.ToolStripMenuItem MenuLvEasy;
        private System.Windows.Forms.ToolStripMenuItem MenuLvIntermediate;
        private System.Windows.Forms.ToolStripMenuItem MenuLvHard;
        private System.Windows.Forms.ToolStripMenuItem MenuLvCus;
        private System.Windows.Forms.Label LBdimension;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Timer Timer;
        private System.Windows.Forms.Label LBtimer;
        private System.Windows.Forms.Label LBmineCount;
        private System.Windows.Forms.Panel StartPanel;
        private System.Windows.Forms.Button BThard;
        private System.Windows.Forms.Button BTintermediate;
        private System.Windows.Forms.Button BTeasy;
        private System.Windows.Forms.Button BTcustom;
    }
}

