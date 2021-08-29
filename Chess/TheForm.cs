using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chess.Pieces;

namespace Chess
{
    class TheForm : Form
    {

        public static string side;
        Panel board = new Panel();
        List<PictureBox> listOfCheckers = new List<PictureBox>();
        List<string> validMove = new List<string>();
        Bishop bishop;
        Knight knight;
        Rook rook;
        Queen queen;
        King king;
        Pawn pawn;
        //movement related
        Mover mover;
        PopImap connection;
        string from;
        string to;
        public static bool isTurn;

        public TheForm()
        {
            this.Height = 940;
            this.Width = 910;
            this.BackColor = Color.FromArgb(20, 10, 0);
            this.Text = "Chess 2p";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.DoubleBuffered = true;
            this.Icon = new Icon(@"../../Sprites/B_Knight.ico");

            Button white = new Button();
            white.SetBounds(290, 5, 150, 20);
            white.Text = "White";
            white.BackColor = Color.White;
            this.Controls.Add(white);

            Button black = new Button();
            black.SetBounds(450, 5, 150, 20);
            black.Text = "Black";
            black.BackColor = Color.Black;
            black.ForeColor = Color.White;
            this.Controls.Add(black);

            white.Click += (s, e) => {
                side = "white";
                connection = new PopImap(side, listOfCheckers);
                isTurn = true;
                this.Controls.Remove(black);
                this.Controls.Remove(white);
            };
            black.Click += (s, e) => {
                side = "black";
                connection = new PopImap(side, listOfCheckers);
                isTurn = false;
                this.Controls.Remove(black);
                this.Controls.Remove(white);
            };

            board.Location = new Point(50, 50);
            board.Height = 800;
            board.Width = 800;
            board.BackgroundImage = Image.FromFile(@"../../Sprites/Board.png");
            this.Controls.Add(board);

            CheckerMaker checkers = new CheckerMaker(board, out listOfCheckers);

            CheckerEventAssigner();
            StartingPositions();
            this.Show();
            Application.Run(this);
        }

        private void CheckerEventAssigner()
        {
            foreach (PictureBox box in listOfCheckers)
            {
                box.Click += (s, e) =>
                {
                    if (isTurn)
                    {
                        string name = box.Name;
                        string tag = box.Tag.ToString();
                        var boxPos = NameDecoder(name);
                        int i = boxPos.Item1;
                        int j = boxPos.Item2;


                        foreach (string move in validMove)
                        {
                            if (box.Name.Equals(move))
                            {
                                Console.WriteLine("Yep Thats a valid move.");
                                to = name;
                                Console.WriteLine($"{from}-{to}");
                                isTurn = false;
                                mover = new Mover($"{from}-{to}", listOfCheckers);
                                connection.Send($"{from}-{to}");
                                CheckmateChecker();
                            }
                        }
                        validMove.Clear();
                        if (side.Equals("white") && tag.StartsWith("W"))
                        {
                            MovesetGenration(box, i, j);
                        }
                        else if (side.Equals("black") && tag.StartsWith("B"))
                        {
                            MovesetGenration(box, i, j);
                        }

                        foreach (PictureBox movebox in listOfCheckers)
                        {
                            movebox.BackColor = Color.Transparent;

                            foreach (string move in validMove)
                            {
                                if (move.Equals(movebox.Name))
                                {
                                    movebox.BackColor = Color.FromArgb(100, 0, 255, 0);
                                }
                            }
                        }
                        from = name;
                        box.BackColor = Color.FromArgb(100, 255, 0, 0);
                    }
                };
            }
        }

        private void MovesetGenration(PictureBox box, int i, int j)
        {
            if (!box.Tag.ToString().Equals(""))
            {
                switch (box.Tag.ToString().Substring(2))
                {
                    case "Pawn":
                        if (box.Tag.ToString().StartsWith("W"))
                        {
                            pawn.ColorIdentifier("white");
                            pawn.MoveSet(i, j, out validMove, listOfCheckers);
                        }
                        else if (box.Tag.ToString().StartsWith("B"))
                        {
                            pawn.ColorIdentifier("black");
                            pawn.MoveSet(i, j, out validMove, listOfCheckers);
                        }
                        break;
                    case "King":
                        if (box.Tag.ToString().StartsWith("W"))
                        {
                            king.ColorIdentifier("white");
                            king.MoveSet(i, j, out validMove, listOfCheckers);
                        }
                        else if (box.Tag.ToString().StartsWith("B"))
                        {
                            king.ColorIdentifier("black");
                            king.MoveSet(i, j, out validMove, listOfCheckers);
                        }
                        break;
                    case "Queen":
                        if (box.Tag.ToString().StartsWith("W"))
                        {
                            queen.ColorIdentifier("white");
                            queen.MoveSet(i, j, out validMove, listOfCheckers);
                        }
                        else if (box.Tag.ToString().StartsWith("B"))
                        {
                            queen.ColorIdentifier("black");
                            queen.MoveSet(i, j, out validMove, listOfCheckers);
                        }
                        break;
                    case "Knight":
                        if (box.Tag.ToString().StartsWith("W"))
                        {
                            knight.ColorIdentifier("white");
                            knight.MoveSet(i, j, out validMove, listOfCheckers);
                        }
                        else if (box.Tag.ToString().StartsWith("B"))
                        {
                            knight.ColorIdentifier("black");
                            knight.MoveSet(i, j, out validMove, listOfCheckers);
                        }
                        break;
                    case "Bishop":
                        if (box.Tag.ToString().StartsWith("W"))
                        {
                            bishop.ColorIdentifier("white");
                            bishop.MoveSet(i, j, out validMove, listOfCheckers);
                        }
                        else if (box.Tag.ToString().StartsWith("B"))
                        {
                            bishop.ColorIdentifier("black");
                            bishop.MoveSet(i, j, out validMove, listOfCheckers);
                        }
                        break;
                    case "Rook":
                        if (box.Tag.ToString().StartsWith("W"))
                        {
                            rook.ColorIdentifier("white");
                            rook.MoveSet(i, j, out validMove, listOfCheckers);
                        }
                        else if (box.Tag.ToString().StartsWith("B"))
                        {
                            rook.ColorIdentifier("black");
                            rook.MoveSet(i, j, out validMove, listOfCheckers);
                        }
                        break;
                    default:
                        break;
                }
                Console.WriteLine($"{box.Name} was Clicked");
                if (box.Tag.ToString()[0].Equals('W'))
                {
                    Console.WriteLine($"It contains a White {box.Tag.ToString().Substring(2)}.");
                }
                else if (box.Tag.ToString()[0].Equals('B'))
                {
                    Console.WriteLine($"It contains a Black {box.Tag.ToString().Substring(2)}.");
                }
            }
        }

        private (int, int) NameDecoder(string name)
        {
            string iInStr = name[0].ToString();
            string jInStr = name[2].ToString();

            int i = int.Parse(iInStr);
            int j = int.Parse(jInStr);

            return (i, j);
        }

        private void StartingPositions()
        {
            foreach (PictureBox box in listOfCheckers)
            {
                if (box.Name.EndsWith("2"))
                {
                    pawn = new Pawn("white");
                    box.Image = pawn.Piece;
                    box.Tag = pawn.Name;
                }
                else if (box.Name.EndsWith("7"))
                {
                    pawn = new Pawn("black");
                    box.Image = pawn.Piece;
                    box.Tag = pawn.Name;
                }
                if (box.Name.Equals("1.1"))
                {
                    rook = new Rook("white");
                    box.Image = rook.Piece;
                    box.Tag = rook.Name;
                }
                else if (box.Name.Equals("2.1"))
                {
                    knight = new Knight("white");
                    box.Image = knight.Piece;
                    box.Tag = knight.Name;
                }
                else if (box.Name.Equals("3.1"))
                {
                    bishop = new Bishop("white");
                    box.Image = bishop.Piece;
                    box.Tag = bishop.Name;
                }
                else if (box.Name.Equals("4.1"))
                {
                    king = new King("white");
                    box.Image = king.Piece;
                    box.Tag = king.Name;
                }
                else if (box.Name.Equals("5.1"))
                {
                    queen = new Queen("white");
                    box.Image = queen.Piece;
                    box.Tag = queen.Name;
                }
                else if (box.Name.Equals("6.1"))
                {
                    bishop = new Bishop("white");
                    box.Image = bishop.Piece;
                    box.Tag = bishop.Name;
                }
                else if (box.Name.Equals("7.1"))
                {
                    knight = new Knight("white");
                    box.Image = knight.Piece;
                    box.Tag = knight.Name;
                }
                else if (box.Name.Equals("8.1"))
                {
                    rook = new Rook("white");
                    box.Image = rook.Piece;
                    box.Tag = rook.Name;
                }
                else if (box.Name.Equals("1.8"))
                {
                    rook = new Rook("black");
                    box.Image = rook.Piece;
                    box.Tag = rook.Name;
                }
                else if (box.Name.Equals("2.8"))
                {
                    knight = new Knight("black");
                    box.Image = knight.Piece;
                    box.Tag = knight.Name;
                }
                else if (box.Name.Equals("3.8"))
                {
                    bishop = new Bishop("black");
                    box.Image = bishop.Piece;
                    box.Tag = bishop.Name;
                }
                else if (box.Name.Equals("4.8"))
                {
                    king = new King("black");
                    box.Image = king.Piece;
                    box.Tag = king.Name;
                }
                else if (box.Name.Equals("5.8"))
                {
                    queen = new Queen("black");
                    box.Image = queen.Piece;
                    box.Tag = queen.Name;
                }
                else if (box.Name.Equals("6.8"))
                {
                    bishop = new Bishop("black");
                    box.Image = bishop.Piece;
                    box.Tag = bishop.Name;
                }
                else if (box.Name.Equals("7.8"))
                {
                    knight = new Knight("black");
                    box.Image = knight.Piece;
                    box.Tag = knight.Name;
                }
                else if (box.Name.Equals("8.8"))
                {
                    rook = new Rook("black");
                    box.Image = rook.Piece;
                    box.Tag = rook.Name;
                }
            }
        }

        private void CheckmateChecker()
        {
            bool WKingExists = true;
            bool BKingExists = true;
            if (!listOfCheckers.Any(p => p.Tag.ToString().Equals("W_King")))
            {
                WKingExists = false;
                string message = $"Checkmate\nBlack Wins.\nDo you want to Quit?";
                string title = "CheckMate";
                DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.OK);
                switch (result)
                {
                    case DialogResult.OK:
                        Application.Exit();
                        break;
                }
            }
            if (!listOfCheckers.Any(p => p.Tag.ToString().Equals("B_King")))
            {
                BKingExists = false;
                string message = $"Checkmate\nWhite Wins.\nDo you want to Quit?";
                string title = "CheckMate";
                DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.OK);
                switch (result)
                {
                    case DialogResult.OK:
                        Application.Exit();
                        break;
                }

            }
        }
    }
}


