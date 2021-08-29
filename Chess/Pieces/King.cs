using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Chess.Pieces
{
    class King : IChessPieces
    {
        public string Color { get; set; }
        public Image Piece { get; set; }
        public string Name { get; set; }

        private string currColor;


        public King(string color)
        {
            Color = color;
            PieceSetter();
        }

        public void MoveSet(int i, int j, out List<string> _validMove, List<PictureBox> board)
        {
            string move1 = $"{i + 1}.{j}";
            string move2 = $"{i - 1}.{j}";
            string move3 = $"{i + 1}.{j + 1}";
            string move4 = $"{i - 1}.{j + 1}";
            string move5 = $"{i}.{j - 1}";
            string move6 = $"{i}.{j + 1}";
            string move7 = $"{i + 1}.{j - 1}";
            string move8 = $"{i - 1}.{j - 1}";
            List<string> moves = new List<string>();
            moves.Add(move1);
            moves.Add(move2);
            moves.Add(move3);
            moves.Add(move4);
            moves.Add(move5);
            moves.Add(move6);
            moves.Add(move7);
            moves.Add(move8);
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
                Piece = Image.FromFile(@"../../Sprites/W_King.png");
                Name = "W_King";
            }
            else if (Color.Equals("black"))
            {
                Piece = Image.FromFile(@"../../Sprites/B_King.png");
                Name = "B_King";

            }
        }
    }
}
