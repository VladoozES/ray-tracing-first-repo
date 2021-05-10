using System.Numerics;

namespace RayTracingFirst
{
    internal class ViewportPixel
    {
        public Vector3 vector;
        //public double X;
        //public double Y;
        //public double Z;

        public ViewportPixel(float X, float Y, float Z)
        {
            vector.X = X;
            vector.Y = Y;
            vector.Z = Z;
        }

        public static ViewportPixel CanvasToViewPort(int cX, int cY, double d, int Cw, int Ch, int Vw, int Vh)
        {
            return new ViewportPixel((float)cX * Vw / Cw , (float)cY * Vh / Ch, (float)d);
        }
    }
}