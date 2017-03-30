using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Game_of_Life
{
    public partial class Canvas : Form
    {
        public const int COLUMNS = 10;
        public const int ROWS = 10;
		public const int CELLSIZE = 50;
        public const int PENSIZE = 1;

        public Canvas()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int[,] aCanvas = new int[COLUMNS, ROWS];
            for (int c = 0; c < COLUMNS; c++) {
				for (int r = 0; r < ROWS; r++) {
					aCanvas[c, r] = 0;
				}
			}
            this.Size = new System.Drawing.Size(COLUMNS * (CELLSIZE + PENSIZE) + 16, ROWS * (CELLSIZE + PENSIZE) + 34);
		}

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = Graphics.FromHwnd(Handle);
            Pen pen = new System.Drawing.Pen(System.Drawing.Color.Black, (float)PENSIZE);
            for (int c = 1; c < COLUMNS; c++)
            {
                g.DrawLine(pen,
                    new System.Drawing.Point((c * (CELLSIZE + PENSIZE)), 0),
                    new System.Drawing.Point((c * (CELLSIZE + PENSIZE)), (ROWS * (CELLSIZE + PENSIZE))));

            }
            for (int r = 0; r < ROWS; r++)
            {
                g.DrawLine(pen,
                    new System.Drawing.Point(0, (r * (CELLSIZE + PENSIZE))),
                    new System.Drawing.Point(COLUMNS * (CELLSIZE + PENSIZE), r * (CELLSIZE + PENSIZE)));
            }
        }

        private void Canvas_MouseClick(object sender, MouseEventArgs e)
        {
            int spalte = (e.Location.X - (e.Location.X  % (CELLSIZE + PENSIZE))) / (CELLSIZE + PENSIZE);
            int zeile = (e.Location.Y - (e.Location.Y % (CELLSIZE + PENSIZE))) / (CELLSIZE + PENSIZE);
        }
    }
}
