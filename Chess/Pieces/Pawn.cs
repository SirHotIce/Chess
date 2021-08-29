using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Chess.Pieces
{
    class Pawn : IChessPieces
    {
        public string Color { get; set; }
        public Image Piece { get; set; }
        public string Name { get; set; }

        private string currColor;

        public Pawn(string color)
        {
            Color = color;
            PieceSetter();
        }

        public  void MoveSet(int i, int j, out List<string> _validMove, List<PictureBox> board)
        {
            string move1;
            string move2;
            List<string> validMove = new List<string>();
            if (currColor.Equals("white"))
            {
                move1 = $"{i}.{j+1}";
                foreach (PictureBox piece in board)
                {
                    string move3 = $"{i + 1}.{j + 1}";
                    string move4 = $"{i -1}.{j + 1}";

                    if (piece.Name.Equals(move3) && piece.Tag.ToString().StartsWith("B"))
                    {
                        validMove.Add(move3);
                    }
                    else if (piece.Name.Equals(move4) && piece.Tag.ToString().StartsWith("B"))
                    {
                        validMove.Add(move4);
                    }
                    if (piece.Name.Equals(move1) && !piece.Tag.Equals(""))
                    {

                    }
                    else if (piece.Name.Equals(move1) && piece.Tag.Equals(""))
                    {
                        validMove.Add(move1);
                    }

                }
            }
            else
            {
                move1 = $"{i}.{j-1}";
                foreach (PictureBox piece in board)
                {
                    string move3 = $"{i + 1}.{j - 1}";
                    string move4 = $"{i - 1}.{j - 1}";

                    if (piece.Name.Equals(move3) && piece.Tag.ToString().StartsWith("W"))
                    {
                        validMove.Add(move3);
                    }
                    else if (piece.Name.Equals(move4) && piece.Tag.ToString().StartsWith("W"))
                    {
                        validMove.Add(move4);
                    }
                    if (piece.Name.Equals(move1) && !piece.Tag.Equals(""))
                    {

                    }
                    else if (piece.Name.Equals(move1) && piece.Tag.Equals(""))
                    {
                        validMove.Add(move1);
                    }
                    if ((piece.Name.Equals(move3) || (piece.Name.Equals(move4)) && piece.Tag.Equals("W_King")))
                    {
                        Console.WriteLine("\nBlack Declares Check On White.\n");
                    }
                }
            }

            if(currColor.Equals("white") && j == 2)
            {
                move2= $"{i}.{j + 2}";
                validMove.Add(move2);
                foreach (PictureBox piece in board)
                {
                    if (piece.Name.Equals(move2) && !piece.Tag.Equals(""))
                    {
                        validMove.Remove(move2);
                    }
                }
            }
            else if(currColor.Equals("black") && j == 7)
            {
                move2 = $"{i}.{j - 2}";
                validMove.Add(move2);
                foreach (PictureBox piece in board)
                {
                    if (piece.Name.Equals(move2) && !piece.Tag.Equals(""))
                    {
                        validMove.Remove(move2);
                    }
                }
            }
                _validMove = validMove;

        }

        public void ColorIdentifier(string color){
            currColor = color;
        }

        public void PieceSetter()
        {
            if (Color.Equals("white"))
            {
                Piece = Image.FromFile(@"../../Sprites/W_Pawn.png");
                Name = "W_Pawn";
            }
            else if (Color.Equals("black"))
            {
                Piece = Image.FromFile(@"../../Sprites/B_Pawn.png");
                Name = "B_Pawn";

            }
        }
    }
}