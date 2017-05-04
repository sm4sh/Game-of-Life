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
        // Konstanten
        public const int COLUMNS = 50;
        public const int ROWS = 50;
		public const int CELLSIZE = 10;
        public const int PENSIZE = 1;

        // Klassenvariablen
        private bool[,] aCanvasData;
        private int dGeneration = 0;

        public Canvas()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(COLUMNS * (CELLSIZE + PENSIZE) + 16, ROWS * (CELLSIZE + PENSIZE) + 34 + 42);
		}

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = Graphics.FromHwnd(Handle);
            Pen pen = new System.Drawing.Pen(System.Drawing.Color.Black, (float)PENSIZE);
            
            // vertikale Linien zeichnen
            for (int c = 1; c < COLUMNS; c++)
            {
                g.DrawLine(pen,
                    new System.Drawing.Point((c * (CELLSIZE + PENSIZE)), 0),
                    new System.Drawing.Point((c * (CELLSIZE + PENSIZE)), (ROWS * (CELLSIZE + PENSIZE))));

            }
            // horizontale Linien zeichnen
            for (int r = 0; r < ROWS; r++)
            {
                g.DrawLine(pen,
                    new System.Drawing.Point(0, (r * (CELLSIZE + PENSIZE))),
                    new System.Drawing.Point(COLUMNS * (CELLSIZE + PENSIZE), r * (CELLSIZE + PENSIZE)));
            }
            resetCanvas();
        }

        // Nutzereingabe
        private void Canvas_MouseClick(object sender, MouseEventArgs e)
        {
            int spalte = (e.Location.X - (e.Location.X  % (CELLSIZE + PENSIZE))) / (CELLSIZE + PENSIZE);
            int zeile = (e.Location.Y - (e.Location.Y % (CELLSIZE + PENSIZE))) / (CELLSIZE + PENSIZE);
            this.aCanvasData[spalte, zeile] = !this.aCanvasData[spalte, zeile];
            updateCanvas();
        }

        // Start/Stop
        private void btn_Click(object sender, EventArgs e)
        {
            if (this.gameloop.Enabled) {
                this.gameloop.Stop();
                this.btn.Text = "Start";
            } else {
                this.gameloop.Start();
                this.btn.Text = "Stop";
            }
        }

        private void updateData()
        {
            int[,] aInt = new int[COLUMNS,ROWS];
            for (int x = 0; x < COLUMNS; x++)
                for (int y = 0; y < ROWS; y++)
                    aInt[x,y] = countNeighbours(x, y);
            updateTiles(aInt);
            this.dGeneration++;
        }

        // Überlebenslogik
        private void updateTiles(int[,] aInt)
        {
            for (int x = 0; x < COLUMNS; x++)
                for (int y = 0; y < ROWS; y++)
                    this.aCanvasData[x, y] = ((aInt[x, y] == 3) || (aInt[x, y] == 2 && this.aCanvasData[x, y]));
        }

        // Zählen der Nachbarn
        private int countNeighbours(int x, int y)
        {
            int neighbours = 0;

            for (int xl = -1; xl < 2; xl++)
                    if((x+xl >=0) && (x+xl < COLUMNS-1))
                        for (int yl = -1; yl < 2;yl++ )
                            if ((y + yl >= 0) && (y + yl <= ROWS-1))
                                if (this.aCanvasData[x+xl, y+yl])
                                    if(!(xl == 0 && yl == 0))
                                        neighbours += 1;
            return neighbours;
        }

        // Updaten der View
        private void updateCanvas()
        {
            Graphics g = Graphics.FromHwnd(Handle);
            Pen pen = new System.Drawing.Pen(System.Drawing.Color.Red, (float)PENSIZE);
            SolidBrush brush_red = new SolidBrush(System.Drawing.Color.Red);
            SolidBrush brush_green = new SolidBrush(System.Drawing.Color.Green);
 
            for (int r = 0; r < ROWS; r++)
                for (int c = 0; c < COLUMNS; c++)
                {
                    g.FillRectangle(aCanvasData[c,r] ? brush_green : brush_red,
                        (c * (CELLSIZE + PENSIZE))+PENSIZE + (CELLSIZE >= 25 ? 3 : 1),
                        (r * (CELLSIZE + PENSIZE)) + PENSIZE + (CELLSIZE >= 25 ? 3 : 1),
                        CELLSIZE - (CELLSIZE >= 25 ? 6 : 2),
                        CELLSIZE - (CELLSIZE >= 25 ? 6 : 2)
                    );

                 }
            groupBox1.Text = "Generation: " + this.dGeneration;
           
        }

        // Reset + Initialisierung des Spielfeldes
        private void resetCanvas()
        {
            bool[,] aCanvas = new bool[COLUMNS, ROWS];
            for (int c = 0; c < COLUMNS; c++)
            {
                for (int r = 0; r < ROWS; r++)
                {
                    aCanvas[c, r] = false;
                }
            }
            this.aCanvasData = aCanvas;
            this.dGeneration = 0;
            updateCanvas();
            this.gameloop.Stop();
            this.btn.Text = "Start";
        }

        private void gameLoop(object sender, EventArgs e)
        {
            updateData();
            updateCanvas();

        }

        private void bnt_step_Click(object sender, EventArgs e)
        {
            updateData();
            updateCanvas();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            resetCanvas();
        }
    }
}
