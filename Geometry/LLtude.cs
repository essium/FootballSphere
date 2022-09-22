using UnityEngine;

namespace FootballSphere.Geometry
{
    internal class LLtude
    {
        public float phi;
        public float lambda;

        public LLtude()
        {

        }

        public LLtude(float phi, float lambda)
        {
            this.phi = phi;
            this.lambda = lambda;
        }
        public Vector3 Position(float radius)
        {
            return radius * new Vector3(Mathf.Cos(phi) * Mathf.Cos(lambda),
                Mathf.Sin(phi),
                Mathf.Cos(phi) * Mathf.Sin(lambda));
        }

        public static Vector3 UnitPoint(LLtude p)
        {
            return new Vector3(Mathf.Cos(p.phi) * Mathf.Cos(p.lambda),
                Mathf.Sin(p.phi),
                Mathf.Cos(p.phi) * Mathf.Sin(p.lambda));
        }

        public static LLtude FromPosition(Vector3 p)
        {
            LLtude q = new LLtude();
            float r = Mathf.Sqrt(p.x * p.x + p.z * p.z);
            q.phi = Mathf.Asin(p.y / p.magnitude);
            q.lambda = Mathf.Acos(p.x / r);
            return q;
        }
    }
}
