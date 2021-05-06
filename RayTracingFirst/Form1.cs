using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace RayTracingFirst
{
    public partial class Form1 : Form
    {
        public Bitmap canvas;
        Graphics g;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           



            /*Bitmap myImage = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics graph = Graphics.FromImage(myImage);

            graph.DrawImage(myImage, 0, 0, myImage.Width, myImage.Height);

            for (var i = 0; i < myImage.Width; i++)
                myImage.SetPixel(i, 15, Color.Black);//*/
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ///ОЧЕНЬ ВАЖНО!!!!
            ///Для удобства я представляю, будто координаты 0, 0 лежат в центре канваса
            ///Но когда отправляются координаты на отрисовку, их нужно преобразовывать в первоначальную систему координат
            ///x = x' + a
            ///y = b - y'
            ///Пояснения на странице 69 космического блокнота
            button1.Enabled = false;
            g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);


            canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            int Cw = pictureBox1.Width;
            int Ch = pictureBox1.Height;
            var a = Cw / 2;
            var b = Ch / 2;
            Vector3 O = new Vector3(0f, 0f, 0f);

            /*
            for (int x = - Cw / 2; x < Cw / 2; x++)
            {

                for (int y = -Ch / 2 + 1; y <= Ch / 2; y++)
                {

                    Viewport D = CanvasToViewPort(x, y);
                    canvas.SetPixel(x + a, b - y, Color.Black);

                }

            }//*/

            
            g.DrawImage(canvas, 0, 0);//*/
        }
    }
}
