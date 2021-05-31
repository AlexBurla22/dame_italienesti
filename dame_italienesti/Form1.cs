using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dame_italienesti
{
    public partial class Form1 : Form
    {
        Game jocNou;
        bool clicked = false;
        PictureBox[,] pictureBoxes;
        Tuple<int, int> source;
        Tuple<int, int> destination;
        public Form1()
        {
            InitializeComponent();
            jocNou = new Game();
            pictureBoxes = new PictureBox[8, 8];
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            int left_ini = 47;
            int top_ini = 47;

            Panel panel = new Panel();
            panel.SetBounds(0, 0, 670, 670);
            panel.Visible = true;

            panel.BackgroundImage = Properties.Resources.board;
            panel.BackgroundImageLayout = ImageLayout.Stretch;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    pictureBoxes[i, j] = new PictureBox();
                    pictureBoxes[i, j].Left = left_ini + j * 72;
                    pictureBoxes[i, j].Top = top_ini + i * 72;

                    pictureBoxes[i, j].Width = 70;
                    pictureBoxes[i, j].Height = 70;
                    pictureBoxes[i, j].BackColor = Color.Transparent;
                    pictureBoxes[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBoxes[i, j].MouseClick += clickOnPictureBox;
                    panel.Controls.Add(pictureBoxes[i, j]);
                }
            }

            Controls.Add(panel);
            randPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            afisare_tabla(jocNou.GetTablaJoc(), pictureBoxes);
            afisare_randJoc(jocNou.GetRandJoc());
            afisare_numar_piese(jocNou.GetNumarPieseAlb(), jocNou.GetNumarPieseNegre());
        }
        private void afisare_randJoc(Culoare culoare)
        {
            if (culoare == Culoare.alb)
            {
                randPictureBox.Image = Properties.Resources.checkers_white;
            }
            else
            {
                randPictureBox.Image = Properties.Resources.checkers_black;
            }
        }
        private void clickOnPictureBox(object sender, MouseEventArgs e)
        {
            PictureBox pbClicked = (sender as PictureBox);

            if (!clicked)
            {
                if (pbClicked.Image != null)
                {
                    source = GetPosition(pbClicked);

                    pbClicked.BorderStyle = BorderStyle.Fixed3D;
                    clicked = true;
                }
            }
            else
            {
                destination = GetPosition(pbClicked);
                if (jocNou.TryMakeMove(source, destination))
                {
                    afisare_tabla(jocNou.GetTablaJoc(), pictureBoxes);
                    afisare_randJoc(jocNou.GetRandJoc());
                    afisare_numar_piese(jocNou.GetNumarPieseAlb(), jocNou.GetNumarPieseNegre());
                }
                //MessageBox.Show("Sursa: Line " + source.Item1 + " Coloana " + source.Item2 + "\nDestinatie: Line " + destination.Item1 + " Coloana " + destination.Item2);
                pictureBoxes[source.Item1, source.Item2].BorderStyle = BorderStyle.None;
                clicked = false;
            }
        }
        private Tuple<int, int> GetPosition(PictureBox pictureBox)
        {
            for (int x = 0; x < 8; ++x)
            {
                for (int y = 0; y < 8; ++y)
                {
                    if (pictureBoxes[x, y].Equals(pictureBox))
                        return Tuple.Create(x, y);
                }
            }
            return Tuple.Create(-1, -1);
        }
        private void afisare_piesa(Piesa piece, PictureBox picture)
        {
            if (piece.GetCuloare() == Culoare.negru)
            {
                if (piece.GetTipPiesa() == TipPiesa.man)
                {
                    picture.Image = Properties.Resources.checkers_black;
                }
                else if (piece.GetTipPiesa() == TipPiesa.king)
                {

                }
            }

            if (piece.GetCuloare() == Culoare.alb)
            {
                if (piece.GetTipPiesa() == TipPiesa.man)
                {
                    picture.Image = Properties.Resources.checkers_white;
                }
                else if(piece.GetTipPiesa() == TipPiesa.king)
                {

                }
            }

            if (piece.GetCuloare() == Culoare.gol)
            {
                picture.Image = null;
            }

           /* if (piece.GetCuloare() == jocNou.GetRandJoc() || piece.GetCuloare() == Culoare.gol)
            {
                picture.Enabled = true;
            }
            else
            {
                picture.Enabled = false;
            }*/
        }
        private void afisare_tabla(Piesa[,] piese, PictureBox[,] pictureBoxes)
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    afisare_piesa(piese[i, j], pictureBoxes[i, j]);
                }
        }
        private void afisare_numar_piese(int numarPieseAlb, int numarPieseNegre)
        {
            labelPieseAlbe.Text = numarPieseAlb.ToString();
            labelPieseNegre.Text = numarPieseNegre.ToString();
        }
    }
}
