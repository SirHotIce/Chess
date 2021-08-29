using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess.Pieces
{
    interface IChessPieces
    {

        string Color
        {
            get; set;
        }
        string Name
        {
            get; set;
        }

        Image Piece
        {
            get; set;
        }
        void MoveSet(int i, int j, out List<string> _validMove, List<PictureBox> board);
        void PieceSetter();
        void ColorIdentifier(string color);
    }
}
