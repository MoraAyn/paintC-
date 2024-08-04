using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private bool isMouse = false;
        private class ArrayPoints
        {
            private int index = 0;
            private Point[] points;
            public ArrayPoints(int size)
            {
                if(size <= 0)
                {
                    size = 2;
                }
                points = new Point[size];
            }
            public void setPoint(int x, int y)
            {
                if (index >= points.Length) index = 0;
                points[index] = new Point(x, y);
                index++;
            }

            public void resetIndex()
            {
                index = 0;
            }

            public Point[] getPoints()
            {
                return points;
            }

            public int getCountPoint()
            {
                return index;
            }
        }
        private ArrayPoints ap = new ArrayPoints(2);

        Bitmap map = new Bitmap(100, 100);
        Graphics graph;
        Pen pen = new Pen(Color.Black, 3f);


        public Form1()
        {
            InitializeComponent();
            setSize();
        }

        private void setSize()
        {
            Rectangle rec = Screen.PrimaryScreen.Bounds;
            map = new Bitmap(rec.Width, rec.Height);
            graph = Graphics.FromImage(map);

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        private void panelColor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picture_MouseDown(object sender, MouseEventArgs e)
        {
            isMouse = true;
        }

        private void picture_MouseUp(object sender, MouseEventArgs e)
        {
            isMouse = false;
            ap.resetIndex();
        }

        private void picture_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouse) return;
            ap.setPoint(e.X, e.Y);
            if (ap.getCountPoint() >= 2)
            {
                graph.DrawLines(pen, ap.getPoints());
                picture.Image = map;
                ap.setPoint(e.X, e.Y);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void btnPal_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pen.Color = colorDialog1.Color;
                ((Button)sender).BackColor = colorDialog1.Color;
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            graph.Clear(picture.BackColor);
            picture.Image = map;
            pen.Width = 3f;
            trackBar1.Value = 0;
            pen.Color = Color.Black;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPG(*.JPG)|*.jpg";
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if(picture.Image == null)
                {
                    picture.Image.Save(saveFileDialog1.FileName);
                }
            }
        }
    }
}
