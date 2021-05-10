namespace RayTracingFirst
{
    internal class ViewportPixel
    {
        public double X;
        public double Y;
        public double Z;

        public ViewportPixel(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public static ViewportPixel CanvasToViewPort(int cX, int cY, double d, int Cw, int Ch, int Vw, int Vh)
        {
            return new ViewportPixel((double)cX * Vw / Cw , (double)cY * Vh / Ch, d);
        }
    }
}