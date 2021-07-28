using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/* Guan, ZiChuan, 1810060
 * Tuesday , May 15
 * R. Vincent , instructor
 * Final project
 */
 
namespace Minesweeper {
    public partial class Minesweeper : Form {
        public Minesweeper() {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen; //https://stackoverflow.com/questions/14007458/center-form-on-startup
            StartPanel.Show();
            DoubleBuffered = true;
        }
        // initialize
        int mCol; // number of column/width of mine zone
        int mRow; // number of rows/height of mine zone
        int mTotal; // number of mine
        bool EndGame; // game state
        int level; // indicator for game level, from 1 to 4 for easy to custom level
        bool a; // for returning to start menu if custom setting is cancelled from the start menue

        // mouse click
        bool mouseLeft;
        bool mouseRight;

        // mouse position
        Point PMousePosition;

        // maximum dimension
        const int maxWidth = 50;
        const int maxHeight = 50;

        // arrays indicatiing tile states
        int[,] SurroundMines = new int[maxWidth, maxHeight]; // idea borrowed from https://codereview.stackexchange.com/questions/144174/c-minesweeper-project
        bool[,] isMine = new bool[maxWidth, maxHeight];
        bool[,] Flagged = new bool[maxWidth, maxHeight];
        bool[,] Uncover = new bool[maxWidth, maxHeight];
        bool[,] FirstClick = new bool[maxWidth, maxHeight];
        bool[,] Explode = new bool[maxWidth, maxHeight];
        bool[,] BadFlag = new bool[maxWidth, maxHeight];

        // pannel components
        // input panel needed to create a custom sized game board
        FlowLayoutPanel customPanel = new FlowLayoutPanel();

        TextBox TBwidth = new TextBox();
        TextBox TBheight = new TextBox();
        TextBox TBmine = new TextBox();

        Label LBwidth = new Label();
        Label LBheight = new Label();
        Label LBmine = new Label();

        Button confirm = new Button();
        Button cancel = new Button();

        private void ClearSettings() {
            Array.Clear(SurroundMines, 0, SurroundMines.Length);
            Array.Clear(isMine, 0, isMine.Length);
            Array.Clear(Flagged, 0, Flagged.Length);
            Array.Clear(Uncover, 0, Uncover.Length);
            LBtimer.Text = "00";
            Timer.Enabled = false;
            EndGame = false;
        }

        // the game just started
        private bool FreshBoard() {
            foreach (bool x in Uncover) {
                if (x == true) {
                    return false;
                }
            }
            return true;
        }
        
        private void SetGame(int width, int height, int mineNmb) {
            // method to create the game board, requiring the dimension of the board and the total number of mines
            mCol = width;
            mRow = height;
            mTotal = mineNmb;
            UpdateSize();
            CenterToScreen();
            LBdimension.Text = mCol + " x " + mRow + " , " + mTotal + " mines";
            LBmineCount.Text = mTotal.ToString();
            MenuLv.Enabled = true;
            Restart.Enabled = true;
            Refresh();
        }

        
        private void Easy() {
            // easy mode game board
            SetGame(8, 8, 10);
            level = 1;
        }

        private void Intermediate() {
            // intermediate mode game board
            SetGame(16, 16, 40);
            level = 2;
        }

        private void Hard() {
            // hard mode game board
            SetGame(24, 24, 99);
            level = 3;
        }

        private void OpenNeighbor(int sx, int sy) {
            // recursively open surrounding tiles
            Uncover[sx, sy] = true;
            for (int i = -1; i < 2; i++) {
                for (int j = -1; j < 2; j++) {
                    int x = sx + i;
                    int y = sy + j;
                    if (x >= 0 && y >= 0 && x < mCol && y < mRow &&
                        !isMine[x, y] && !Uncover[x, y] && SurroundMines[sx, sy] == 0) {
                        /* conditions: 
                         * 1. the center tile (sx,sy) is a blank tile (no surrounding mines)
                         * 2. go search all the surrounding tiles
                         * 3. surrounding mines are legal (x,y position is in board)
                         * 4. if not mine, uncover recursively
                         */
                        OpenNeighbor(x, y);
                    }
                }
            }
        }

        private void ShowAllMines() {
            // show all mines on board
            for (int i = 0; i < maxWidth; i++) {
                for (int j = 0; j < maxHeight; j++) {
                    if (isMine[i, j]) {
                        Uncover[i, j] = true;
                    }
                }
            }
        }

        private bool Completed() {
            // check if the board is completed
            for (int i = 0; i < mCol; i++) {
                for (int j = 0; j < mRow; j++) {
                    if (!isMine[i, j] && !Uncover[i, j]) { // game is not won if a none-mine tile is still coverd
                        return false;
                    }
                }
            }
            return true;
        }

        private bool AllNeighborMineFlagged(int sx, int sy) {
            // return true if all neighbor mines are flagged
            for (int i = -1; i < 2; i++) {
                for (int j = -1; j < 2; j++) {
                    int x = sx + i;
                    int y = sy + j;
                    if (x >= 0 && y >= 0 && x < mCol && y < mRow && isMine[x, y] && !Flagged[x, y]) {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool LessFlag(int sx, int sy) {
            // return true if flagged neighbor tiles is less than the surrounding mine number
            int c = 0;
            for (int i = -1; i < 2; i++) {
                for (int j = -1; j < 2; j++) {
                    int x = sx + i;
                    int y = sy + j;
                    if (x >= 0 && y >= 0 && x < mCol && y < mRow && Flagged[x, y]) {
                        c++;
                    }
                }
            }
            if (c < SurroundMines[sx, sy]) {
                return true;
            }
            else
                return false;
        }
        
        private void PaintBoard(Graphics g) {
            // paint the mine zone
            Color controlColor = Color.FromKnownColor(KnownColor.Control); //https://social.msdn.microsoft.com/Forums/windows/en-US/7fb6c010-4d54-48c9-b290-af71d7db4be5/rgb-value-of-system-color-control?forum=winforms
            g.Clear(controlColor);
            SolidBrush tileColor = new SolidBrush(Color.Orange); // color of covered tiles
            SolidBrush openTileColor = new SolidBrush(Color.LightGray); // color of uncovered tiles
            SolidBrush explodedMine = new SolidBrush(Color.Red); // color of exploded mine
            // color of numbers
            SolidBrush One = new SolidBrush(Color.Blue);
            SolidBrush Two = new SolidBrush(Color.Green);
            SolidBrush Three = new SolidBrush(Color.Red);
            SolidBrush Four = new SolidBrush(Color.DarkBlue);
            SolidBrush Five = new SolidBrush(Color.Brown);
            SolidBrush Six = new SolidBrush(Color.Cyan);
            SolidBrush Seven = new SolidBrush(Color.Black);
            SolidBrush Eight = new SolidBrush(Color.Gray);

            // initial point coordinate x, y of the board, which is the top-left corner of the first mine
            int x = 20;
            int y = 20 + MainMenuStrip.Height + TopPanel.Height;

            for (int i = 0; i < mCol; i++) {
                // for every column 
                for (int j = 0; j < mRow; j++) {
                    // for every row
                    
                    if (! Uncover[i, j]) { // a covered tile
                        if (Flagged[i, j]) { // flag image found in https://ru.wikipedia.org/wiki/%D0%A4%D0%B0%D0%B9%D0%BB:Flag_icon_red_4.svg
                            if (BadFlag[i, j]) {
                                g.FillRectangle(openTileColor, new Rectangle(x + 32 * i, y + 32 * j, 30, 30));
                                g.DrawImage(Properties.Resources.WrongFlag_icon, x + 32 * i, y + 32 * j);
                            }
                            else {
                                g.FillRectangle(openTileColor, new Rectangle(x + 32 * i, y + 32 * j, 30, 30));
                                g.DrawImage(Properties.Resources.Flag_icon, x + 32 * i, y + 32 * j);
                            }
                        }
                        else {
                            g.FillRectangle(tileColor, new Rectangle(x + 32 * i, y + 32 * j, 30, 30));
                            /*
                             * each x,y coordinate of a new mine tile will be slightly off by 2 to create the gap between mines, 
                             * as the height and width of the tiles are set to 30.
                             */
                        }
                    }
                    else if (Uncover[i,j]) { // a uncovered tile
                        if (! isMine[i,j]) { // not mine, show blank/numbers
                            if (SurroundMines[i,j] == 0) {
                                g.FillRectangle(openTileColor, new Rectangle(x + 32 * i, y + 32 * j, 30, 30));
                            }
                            else {
                                g.FillRectangle(openTileColor, new Rectangle(x + 32 * i, y + 32 * j, 30, 30));

                                switch (SurroundMines[i, j]) {
                                    case 1:
                                        g.DrawString("1", new Font("Arial", 20), One, x + 32 * i, y + 32 * j);
                                        break;

                                    case 2:
                                        g.DrawString("2", new Font("Arial", 20), Two, x + 32 * i, y + 32 * j);
                                        break;

                                    case 3:
                                        g.DrawString("3", new Font("Arial", 20), Three, x + 32 * i, y + 32 * j);
                                        break;

                                    case 4:
                                        g.DrawString("4", new Font("Arial", 20), Four, x + 32 * i, y + 32 * j);
                                        break;

                                    case 5:
                                        g.DrawString("5", new Font("Arial", 20), Five, x + 32 * i, y + 32 * j);
                                        break;

                                    case 6:
                                        g.DrawString("6", new Font("Arial", 20), Six, x + 32 * i, y + 32 * j);
                                        break;

                                    case 7:
                                        g.DrawString("7", new Font("Arial", 20), Seven, x + 32 * i, y + 32 * j);
                                        break;

                                    default:
                                        g.DrawString("8", new Font("Arial", 20), Eight, x + 32 * i, y + 32 * j);
                                        break;
                                }
                            }
                        }
                        else { // show mine
                            if (Explode[i, j]) {
                                g.FillRectangle(explodedMine, new Rectangle(x + 32 * i, y + 32 * j, 30, 30));
                                g.DrawImage(Properties.Resources.Mine_icon, x + 32 * i, y + 32 * j);
                                // mine icon is drawn by myself
                            }
                            else {
                                g.FillRectangle(openTileColor, new Rectangle(x + 32 * i, y + 32 * j, 30, 30));
                                g.DrawImage(Properties.Resources.Mine_icon, x + 32 * i, y + 32 * j);
                            }
                        }
                    }
                }
            }
        }

        private void easyToolStripMenuItem1_Click(object sender, EventArgs e) {
            // change game level to easy 
            MenuLv.Text = MenuLvEasy.Text;
            ClearSettings();
            Easy();

        }

        private void intermediateToolStripMenuItem_Click(object sender, EventArgs e) {
            // change game level to intermediate
            MenuLv.Text = MenuLvIntermediate.Text;
            ClearSettings();
            Intermediate();
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e) {
            // change game level to hard
            MenuLv.Text = MenuLvHard.Text;
            ClearSettings();
            Hard();
        }
        
        private void ShowCustomPanel() {
            // set a custom board size
            customPanel.BackColor = Color.WhiteSmoke;
            customPanel.Size = new Size(125, 210); // bug when custom board is too small in height
            customPanel.Location = new Point((ClientSize.Width - customPanel.Width) / 2, (ClientSize.Height - customPanel.Height) / 2);
            Controls.Add(customPanel);

            LBwidth.Text = "Width (max 50)";
            LBheight.Text = "Height (max 50)";
            LBmine.Text = "Mine (max 50)";

            confirm.Text = "OK";
            confirm.Click += new EventHandler(confirm_Click);
            cancel.Text = "Cancel";
            cancel.Click += new EventHandler(cancel_Click);

            customPanel.Controls.Add(LBwidth);
            customPanel.Controls.Add(TBwidth);
            customPanel.Controls.Add(LBheight);
            customPanel.Controls.Add(TBheight);
            customPanel.Controls.Add(LBmine);
            customPanel.Controls.Add(TBmine);
            customPanel.Controls.Add(confirm);
            customPanel.Controls.Add(cancel);
            AcceptButton = confirm;
            customPanel.BorderStyle = BorderStyle.FixedSingle;
            customPanel.Show();
        }

        private void customPanel_VisibleChanged(object sender, EventArgs e) {
            // event handler for custom panel visibility change
            if (customPanel.Visible) {
                MenuLv.Enabled = false;
                Restart.Enabled = false;
            }
        }

        private void customToolStripMenuItem_Click(object sender, EventArgs e) {
            // when clicked at custom level form strip menu
            ShowCustomPanel();
        }

        private void confirm_Click(object sender, EventArgs e) {
            // set the custom board with the user's inputs
            int x, y, z;
            if (Int32.TryParse(TBwidth.Text, out x) &&
            Int32.TryParse(TBheight.Text, out y) &&
            Int32.TryParse(TBmine.Text, out z) &&
            x <= 50 &&
            y <= 50 &&
            z <= x * y -1 &&
            x > 0 &&
            y > 0 &&
            z > 0) {
                // all inputs are numbers between 1 and 50
                // set the game
                customPanel.Hide();
                MenuLv.Text = MenuLvCus.Text;
                ClearSettings();
                SetGame(x, y, z);
                level = 4;
            }
            else { // otherwise show an error message 
                MessageBox.Show("Please enter a number between 1 and 50");
            }
        }

        private void cancel_Click(object sender, EventArgs e) {
            // cancel custom settings
            if (a) { // from start menu
                customPanel.Hide();
                StartPanel.Show();
                StartPanel.Location = new Point((ClientSize.Width - StartPanel.Width) / 2, (ClientSize.Height - StartPanel.Height) / 2);
                TBheight.Clear();
                TBwidth.Clear();
                TBmine.Clear();
                Refresh();
            }
            else {
                customPanel.Hide();
                TBheight.Clear();
                TBwidth.Clear();
                TBmine.Clear();
                Refresh();
            }
        }

        private void BTeasy_Click(object sender, EventArgs e) {
            StartPanel.Hide();
            Easy();
            a = false;
        }

        private void BTintermediate_Click(object sender, EventArgs e) {
            StartPanel.Hide();
            Intermediate();
            a = false;
        }

        private void BThard_Click(object sender, EventArgs e) {
            StartPanel.Hide();
            Hard();
            a = false;
        }

        private void BTcustom_Click(object sender, EventArgs e) {
            StartPanel.Hide();
            ShowCustomPanel();
            a = true;
        }

        private void StartPanel_VisibleChanged(object sender, EventArgs e) {
            if (StartPanel.Visible) {
                MenuLv.Enabled = false;
                Restart.Enabled = false;
            }
        }

        private void UpdateSize() {
            // update window size according to board size
            // border
            int bx = Width - ClientSize.Width;
            int by = Height - ClientSize.Height;

            int x = bx + 20 * 2 + mCol * 32;
            // boarder size + top and bot 20 (refer to Paint()'s first mine position)
            // + horizontal tile number * 32 (32 being 30 + 2 for the gap)
            int y = by + TopPanel.Height + 20 * 2 + mRow * 32 + LBdimension.Height; // +25 for bottom size display panel

            if (x < 220 + bx) { // minimum size
                Width = 220 + bx;
            }
            else {
                Width = x;
            }

            Height = y;

            // also update panels' displacement
            TopPanel.Size = new Size(ClientSize.Width, TopPanel.Height);
            Restart.Location = new Point(TopPanel.Width / 2 - Restart.Width / 2, TopPanel.Height / 2 - Restart.Height / 2);
            LBtimer.Location = new Point(ClientSize.Width - 20 - LBtimer.Width, LBtimer.Location.Y);
        }
        
        private void StartGame() {
            Random rng = new Random();
            
            for (int i = 0; i < mTotal;) {
                // random mine position x,y
                int x = rng.Next(mCol);
                int y = rng.Next(mRow);
                // lay mines
                if (isMine[x, y] == false && (!Enumerable.Range(PMousePosition.X - mCol / 8, (mCol / 8) * 2 + 1).Contains(x)
                || !Enumerable.Range(PMousePosition.Y - mRow / 8, (mRow / 8) * 2 + 1).Contains(y))) {
                    // not repetitive and not within surrounding of first click position
                    isMine[x, y] = true;
                    i++;
                }
            }
            
            // search the whole board
            for (int i = 0; i < mCol; i++) {
                for (int j = 0; j < mRow; j++) {
                    if (isMine[i, j] == false) { // not a mine, search neighbors (8 surrounding tiles)
                        for (int k = -1; k < 2; k++) {
                            for (int z = -1; z < 2; z++) {
                                int x = i + k;
                                int y = j + z;
                                if (x >= 0 && y >= 0 && isMine[x,y] == true) {
                                    SurroundMines[i, j]++;
                                }
                            }
                        }

                    }
                }
            }
        }
        
        private void Timer_Tick(object sender, EventArgs e) {
            LBtimer.Text = (Int32.Parse(LBtimer.Text) + 1).ToString("00");
        }

        // capture mouse position
        private void Minesweeper_MouseMove(object sender, MouseEventArgs e) { //https://www.daniweb.com/programming/software-development/threads/317766/mouse-coordinates-within-a-form
            // convert to relative column and row position
            int mStart_X = 20;
            int mEnd_X = mStart_X + mCol * 32;
            int mStart_Y = 20 + MainMenuStrip.Height + TopPanel.Height;
            int mEnd_Y = mStart_Y + mRow * 32;
            if (e.X < mStart_X || e.X > mEnd_X || e.Y < mStart_Y || e.Y > mEnd_Y) {
                PMousePosition.X = -1;
                PMousePosition.Y = -1;
            }
            else {
                PMousePosition.X = (e.X - mStart_X) / 32;
                PMousePosition.Y = (e.Y - mStart_Y) / 32;
            }
        }
        
        // click
        private void Minesweeper_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                mouseLeft = true;
            }
            else if (e.Button == MouseButtons.Right) {
                mouseRight = true;
            }
        }

        // release
        private void Minesweeper_MouseUp(object sender, MouseEventArgs e) {
            // location of the click
            if (PMousePosition.X < 0 || PMousePosition.Y < 0 || EndGame) { 
                // not on the tiles or game is over
            }
            else if (mouseLeft) {
                // first move?
                if (FreshBoard()) {
                    ClearSettings();
                    Timer.Enabled = true;
                    StartGame();
                    OpenNeighbor(PMousePosition.X, PMousePosition.Y);
                }
                else {
                    if (!Uncover[PMousePosition.X, PMousePosition.Y]) {
                        //uncover a tile
                        if (!isMine[PMousePosition.X, PMousePosition.Y] && !Flagged[PMousePosition.X, PMousePosition.Y] && SurroundMines[PMousePosition.X, PMousePosition.Y] == 0) { 
                            //blank tile: is not a mine, not flagged, and no surrounding mines
                            OpenNeighbor(PMousePosition.X, PMousePosition.Y);
                        }
                        else if (isMine[PMousePosition.X, PMousePosition.Y] && !Flagged[PMousePosition.X, PMousePosition.Y]) { // is mine = game over
                            Timer.Enabled = false;
                            ShowAllMines();
                            Explode[PMousePosition.X, PMousePosition.Y] = true;
                            MessageBox.Show("Game Over");
                            EndGame = true;
                        }
                        else if(!isMine[PMousePosition.X, PMousePosition.Y] && !Flagged[PMousePosition.X, PMousePosition.Y] && SurroundMines[PMousePosition.X, PMousePosition.Y] != 0) {
                            //is a tile with number
                            Uncover[PMousePosition.X, PMousePosition.Y] = true;
                        }
                    }
                    else if (Uncover[PMousePosition.X, PMousePosition.Y] && !isMine[PMousePosition.X, PMousePosition.Y] && SurroundMines[PMousePosition.X, PMousePosition.Y] != 0) {
                        //click on a number
                        if (AllNeighborMineFlagged(PMousePosition.X, PMousePosition.Y)) {
                            // all neighbor mines are flagged
                            // open neighbors that are not mine
                            for (int i = -1; i < 2; i++) {
                                for (int j = -1; j < 2; j++) {
                                    int x = PMousePosition.X + i;
                                    int y = PMousePosition.Y + j;
                                    if (x >= 0 && y >= 0 && x < mCol && y < mRow &&
                                        !isMine[x, y] && !Uncover[x, y]) {
                                        OpenNeighbor(x, y);
                                    }
                                }
                            }
                        }
                        else if (LessFlag(PMousePosition.X,PMousePosition.Y)) {
                            // less flag than mine, do nothing
                            return;
                        }
                        else {// wrong flag = game over
                            ShowAllMines();
                            EndGame = true;
                            // determine exploded mine
                            for (int i = -1; i < 2; i++) {
                                for (int j = -1; j < 2; j++) {
                                    int x = PMousePosition.X + i;
                                    int y = PMousePosition.Y + j;
                                    if (x >= 0 && y >= 0 && x < mCol && y < mRow &&
                                        isMine[x, y] && ! Flagged[x, y]) {
                                        Explode[x, y] = true;
                                    }
                                }
                            }
                            for (int i = -1; i < 2; i++) {
                                for (int j = -1; j < 2; j++) {
                                    int x = PMousePosition.X + i;
                                    int y = PMousePosition.Y + j;
                                    if (x >= 0 && y >= 0 && x < mCol && y < mRow &&
                                        !isMine[x, y] && Flagged[x, y]) {
                                        BadFlag[x, y] = true;
                                    }
                                }
                            }
                            Timer.Enabled = false;
                            MessageBox.Show("Game Over");
                        }
                    }
                }
                if (Completed()) {
                    Timer.Enabled = false;
                    for (int i = 0; i < mCol; i++) {
                        for (int j = 0; j < mRow; j++) {
                            if (!Uncover[i, j]) {
                                Flagged[i, j] = true;
                            }
                        }
                    }
                    EndGame = true;
                    MessageBox.Show("You Won!");
                }
            }
            else if (mouseRight) {
                if (FreshBoard()) {
                    // cannot flag a freshboard
                }
                else if (!Flagged[PMousePosition.X, PMousePosition.Y]) { // flag
                    Flagged[PMousePosition.X, PMousePosition.Y] = true; // flag and mine -1
                    LBmineCount.Text = (Int32.Parse(LBmineCount.Text) - 1).ToString();
                }
                else { // unflag
                    Flagged[PMousePosition.X, PMousePosition.Y] = false;
                    LBmineCount.Text = (Int32.Parse(LBmineCount.Text) + 1).ToString();
                }
            }
            Refresh();
            mouseLeft = false;
            mouseRight = false;
        }

        private void Restart_Click(object sender, EventArgs e) {
            // restart the game based on the previous board configuration
            switch (level) {
                case 1:
                    ClearSettings();
                    Easy();
                    break;
                case 2:
                    ClearSettings();
                    Intermediate();
                    break;
                case 3:
                    ClearSettings();
                    Hard();
                    break;
                default:
                    ClearSettings();
                    SetGame(Int32.Parse(TBwidth.Text), Int32.Parse(TBheight.Text), Int32.Parse(TBmine.Text));
                    break;
            }
        }

        private void Minesweeper_Paint(object sender, PaintEventArgs e) {
            // paint event
            PaintBoard(e.Graphics);
        }

    }
}

    
 