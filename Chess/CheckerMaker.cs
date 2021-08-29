using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;



namespace Chess
{
    class CheckerMaker
    {
        Control control;
        PictureBox box;
        List<PictureBox> checkers = new List<PictureBox>();

        public CheckerMaker(Control control, out List<PictureBox> checkers)
        {
            this.control = control;
            checkers = this.checkers;
            Generator();
        }

        private void Generator()
        {
            int x=0;
            for(int i=1; i<=8; i++)
            {
                int y = 0;
                for (int j=1; j<=8; j++)
                {

                    box = new PictureBox();
                    string genName = $"{i}.{j}";
                    box.Name = genName;
                    box.Height = 100;
                    box.Width = 100;
                    box.Location = new Point(x, y);
                    box.BackColor = Color.Transparent;
                    box.SizeMode = PictureBoxSizeMode.StretchImage;
                    box.Padding = new Padding(10,10,10,10);
                    box.Tag = "";
                    control.Controls.Add(box);
                    checkers.Add(box);

                    y += 100;
                }
                x += 100;
            }
        }
    }
}
