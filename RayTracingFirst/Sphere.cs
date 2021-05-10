using System.Drawing;
using System.Numerics;

namespace RayTracingFirst
{
    internal class Sphere
    {
        public Vector3 center;
        public double radius;
        public Color color;
        public int specular;

        public Sphere(Vector3 center, double radius, Color color, int specular)
        {
            this.center = center;
            this.radius = radius;
            this.color = color;
            this.specular = specular;
        }
    }
}