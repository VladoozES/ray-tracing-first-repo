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
        Scene scene;
        Color BACKGROUND_COLOR = Color.Black;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
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

            scene = new Scene();
            scene.addSphere(new Sphere(new Vector3(0, -1, 3), 1, Color.Red, 500));
            scene.addSphere(new Sphere(new Vector3(2, 0, 4), 1, Color.Blue, 500));
            scene.addSphere(new Sphere(new Vector3(-2, 0, 4), 1, Color.Green, 25));
            scene.addSphere(new Sphere(new Vector3(0, -5001, 0), 5000, Color.Yellow, 1000));
            scene.addLight(new AmbientLight(0.2));
            scene.addLight(new PointLight(new Vector3(2, 1, 0), 0.6));
            scene.addLight(new DirectionalLight(new Vector3(1, 4, 4), 0.2));


            canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            int Cw = pictureBox1.Width;
            int Ch = pictureBox1.Height;
            int Vw = 2;
            int Vh = 1;
            double d = 1;
            var a = Cw / 2;
            var b = Ch / 2;
            Vector3 O = new Vector3(0f, 0f, 0f);

            ///*
            for (int x = - Cw / 2; x < Cw / 2; x++)
            {

                for (int y = -Ch / 2 + 1; y <= Ch / 2; y++)
                {

                    ViewportPixel D = ViewportPixel.CanvasToViewPort(x + a, b - y, d, Cw, Ch, Vw, Vh);
                    D = new ViewportPixel(D.vector.X - (float)Vw / 2, (float)Vh / 2 - D.vector.Y, D.vector.Z);
                    Color color = TraceRay(O, D, 1, 100);
                    canvas.SetPixel(x + a, b - y, color);

                }

            }//*/

            
            g.DrawImage(canvas, 0, 0);//*/
        }

        private Color TraceRay(Vector3 O, ViewportPixel D, double tMin, double tMax)
        {
            var closetT = tMax;
            Sphere closetSphere = null;
            foreach (Sphere sphere in scene.spheres)
            {
                List<double> tList = IntersectRaySphere(O, D, sphere);
                for (var i = 0; i < tList.Count; i++)
                    if (tList[i] >= tMin && tList[i] <= tMax && tList[i] < closetT)
                    {
                        closetT = tList[i];
                        closetSphere = sphere;
                    }
            }
            if (closetSphere == null) return BACKGROUND_COLOR;
            var P = O + new Vector3((float)D.vector.X * (float)closetT, (float)D.vector.Y * (float)closetT, (float)D.vector.Z * (float)closetT);
            var N = P - closetSphere.center;
            N = N / N.Length();
            var computeLighting = ComputeLighting(P, N, -D.vector, closetSphere.specular);
            if (computeLighting > 1) computeLighting = 1;
            var res_color = Color.FromArgb((int)(closetSphere.color.R * computeLighting),
                                            (int)(closetSphere.color.G * computeLighting),
                                            (int)(closetSphere.color.B * computeLighting));
            return res_color;
        }

        private List<double> IntersectRaySphere(Vector3 O, ViewportPixel D, Sphere sphere)
        {
            var vectorD = new Vector3((float) D.vector.X, (float) D.vector.Y, (float) D.vector.Z);
            List<double> result = new List<double>();
            var k1 = Vector3.Dot(vectorD, vectorD);
            var k2 = 2 * Vector3.Dot(O - sphere.center, vectorD);
            var k3 = Vector3.Dot(O - sphere.center, O - sphere.center) - sphere.radius * sphere.radius;

            var discriminant = k2 * k2 - 4 * k1 * k3;
            if (discriminant < 0) return result;

            result.Add((-k2 + Math.Sqrt(discriminant)) / (2 * k1));
            result.Add((-k2 - Math.Sqrt(discriminant)) / (2 * k1));
            return result;
        }

        private double ComputeLighting(Vector3 P, Vector3 N, Vector3 V, int s)
        {
            var intensity = 0.0;
            foreach (Light light in scene.lights)
            {
                if (light.GetLightType() == "Ambient")
                    intensity += light.intensity;
                else
                {
                    var L = new Vector3();
                    if (light.GetLightType() == "Point")
                    {
                        L = ((PointLight)light).position - P;
                    }
                    else
                    {
                        L = ((DirectionalLight)light).direction;
                    }

                    //Диффузность
                    var n_dot_l = Vector3.Dot(N, L);
                    if (n_dot_l > 0)
                    {
                        intensity += light.intensity * n_dot_l / (N.Length() * L.Length());
                    }

                    //Зеркальность
                    if (s != -1)
                    {
                        var R = 2 * N * Vector3.Dot(N, L) - L;
                        var r_dot_v = Vector3.Dot(R, V);
                        if (r_dot_v > 0)
                            intensity += light.intensity * Math.Pow(r_dot_v / (R.Length() * V.Length()), s);
                    }
                }
            }
            return intensity;
        }
    }
}
