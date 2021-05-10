using System.Numerics;

namespace RayTracingFirst
{
    public abstract class Light
    {
        public double intensity;

        abstract public string GetLightType();
    }

    public class PointLight : Light
    {
        public Vector3 position;

        public PointLight(Vector3 position, double intensity)
        {
            this.position = position;
            this.intensity = intensity;
        }

        override public string GetLightType()
        {
            return "Point";
        }
    }

    public class DirectionalLight : Light
    {
        public Vector3 direction;

        public DirectionalLight(Vector3 direction, double intensity)
        {
            this.direction = direction;
            this.intensity = intensity;
        }

        override public string GetLightType()
        {
            return "Directional";
        }
    }

    public class AmbientLight : Light
    {
        public AmbientLight(double intensity)
        {
            this.intensity = intensity;
        }

        override public string GetLightType()
        {
            return "Ambient";
        }
    }
}