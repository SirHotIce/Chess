using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    class Mover
    {
        string _positionData;
        List<PictureBox> _theBoardData;
        string from;
        string to;
        Image containedImage;
        string containedName;


        public Mover(string positionData, List<PictureBox> theBoardData)
        {
            _positionData = positionData;
            _theBoardData = theBoardData;
            Decoder();
            Move();
        }


        private void Decoder()
        {
            to = _positionData.Substring(4);
            from = _positionData.Substring(0, 3);
        }

        private void Move()
        {
            foreach (PictureBox box in _theBoardData)
            {
                if (from.Equals(box.Name))
                {
                    containedImage = box.Image;
                    containedName = box.Tag.ToString();

                    box.Image = null;
                    box.Tag = "";
                }
            }
            foreach (PictureBox box in _theBoardData)
            {
                if (to.Equals(box.Name))
                {
                    if (containedName.Equals("W_Pawn") && to.EndsWith("8"))
                    {
                        box.Image = Image.FromFile(@"../../Sprites/W_Queen.png");
                        box.Tag = "W_Queen";
                    }else if (containedName.Equals("B_Pawn") && to.EndsWith("8"))
                    {
                        box.Image = Image.FromFile(@"../../Sprites/B_Queen.png");
                        box.Tag = "B_Queen";
                    }
                    else
                    {
                        box.Image = containedImage;
                        box.Tag = containedName;
                    }
                }
            }
        }
    }
}
