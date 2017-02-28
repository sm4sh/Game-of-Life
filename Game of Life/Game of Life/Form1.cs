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
        public const int COLUMNS = 20;
        public const int ROWS = 20;
		public const int CELLSIZE = 10;

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
			this.Size = new System.Drawing.Size(COLUMNS * CELLSIZE + 16, ROWS * CELLSIZE + 34);
			
		}
    }
}
