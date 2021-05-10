using System.Collections.Generic;

namespace RayTracingFirst
{
    internal class Scene
    {
        public List<Sphere> spheres = new List<Sphere>();

        public Scene()
        {
        }

        public void addSphere(Sphere sphere)
        {
            spheres.Add(sphere);
        }
    }
}