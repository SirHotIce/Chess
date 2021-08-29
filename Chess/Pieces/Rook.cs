using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Chess.Pieces
{
    class Rook : IChessPieces
    {
        public string Color { get; set; }
        public Image Piece { get; set; }
        public string Name { get; set; }

        private string currColor;

        public Rook(string color)
        {
            Color = color;
            PieceSetter();
        }


        public void MoveSet(int i, int j, out List<string> _validMove, List<PictureBox> board)
        {
            int startI = i;
            int startJ = j;
            List<string> moves = new List<string>();
            while (startI <= 8)
            {
                string move = $"{startI + 1}.{startJ}";
                startI++;
                moves.Add(move);
                foreach (PictureBox piece in board)
                {
                    if (move.Equals(piece.Name) && !piece.Tag.ToString().Equals(""))
                    {
                        goto End1;
                    }
                }
            }
            End1:
            startI = i;
            startJ = j;
            while (startJ > 0)
            {
                string move = $"{startI}.{startJ - 1}";
                startJ--;
                moves.Add(move);
                foreach (PictureBox piece in board)
                {
                    if (move.Equals(piece.Name) && !piece.Tag.ToString().Equals(""))
                    {
                        goto End2;
                    }
                }
            }
            End2:
            startI = i;
            startJ = j;
            while (startJ < 8)
            {
                string move = $"{startI}.{startJ + 1}";
                startJ++;
                moves.Add(move);
                foreach (PictureBox piece in board)
                {
                    if (move.Equals(piece.Name) && !piece.Tag.ToString().Equals(""))
                    {
                        goto End3;
                    }
                }
            }
            End3:
            startI = i;
            startJ = j;
            while (startI > 0)
            {
                string move = $"{startI - 1}.{startJ}";
                startI--;
                moves.Add(move);
                foreach (PictureBox piece in board)
                {
                    if (move.Equals(piece.Name) && !piece.Tag.ToString().Equals(""))
                    {
                        goto End4;
                    }
                }
            }
            End4:
            List<string> validMoves = new List<string>(moves);

            foreach (PictureBox piece in board)
            {
                foreach (string move in moves)
                {
                    if (currColor.Equals("black"))
                    {
                        if (move.Equals(piece.Name) && piece.Tag.ToString().StartsWith("B"))
                        {
                            validMoves.Remove(move.ToString());
                        }
                        if (move.Equals(piece.Name) && piece.Tag.Equals("W_King"))
                        {
                            Console.WriteLine("\nBlack Declares Check On White.\n");
                        }
                    }
                    else if (currColor.Equals("white"))
                    {
                        if (move.Equals(piece.Name) && piece.Tag.ToString().StartsWith("W"))
                        {
                            validMoves.Remove(move.ToString());
                        }
                        if (move.Equals(piece.Name) && piece.Tag.Equals("B_King"))
                        {
                            Console.WriteLine("\nWhite Declares Check On Black.\n");
                        }
                    }
                }
            }
            _validMove = validMoves;

        }

        public void ColorIdentifier(string color)
        {
            currColor = color;
        }

        public void PieceSetter()
        {
            if (Color.Equals("white"))
            {
                Piece = Image.FromFile(@"../../Sprites/W_Rook.png");
                Name = "W_Rook";
            }
            else if (Color.Equals("black"))
            {
                Piece = Image.FromFile(@"../../Sprites/B_Rook.png");
                Name = "B_Rook";

            }
        }
    }
}

