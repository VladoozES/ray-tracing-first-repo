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
        public float reflective;

        public Sphere(Vector3 center, double radius, Color color, int specular, float reflective)
        {
            this.center = center;
            this.radius = radius;
            this.color = color;
            this.specular = specular;
            this.reflective = reflective;
        }
    }
}