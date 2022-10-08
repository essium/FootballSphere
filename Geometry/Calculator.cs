using FootballSphere.Geometry;
using UnityEngine;

namespace FootballSphere.Geometry
{
    // x = cos(phi)cos(lambda)
    // z = cos(phi)sin(lambda)
    // y = sin(phi)
    internal class Calculator
    {
        public static float a = Mathf.Cos(Mathf.PI / 5f);
        public static float asq = a * a;
        public static float l = 2f * Mathf.Sqrt((3f - 4f * asq) / (4f - 4f * asq));
        public static float d = Mathf.Sqrt(1f - l * l / 3f);
        public static float h = (1f - d) * Mathf.Sqrt((4f - 4f * asq) / (3f - 4f * asq));
        public static float rho = h / l;
        public static LLtude o1 = new LLtude(Mathf.PI / 2f, 0f);
        public static LLtude o2 = new LLtude(Mathf.Acos(Mathf.Sqrt(3f - 4f * asq) / (2f - 2 * asq)), 0);
        public static LLtude o3 = new LLtude(o2.phi, 0.4f * Mathf.PI);
        public static LLtude o4 = new LLtude(-o2.phi, -0.2f * Mathf.PI);
        public static LLtude o5 = new LLtude(-o2.phi, 0.2f * Mathf.PI);
        public static LLtude o6 = new LLtude(-o2.phi, 0.6f * Mathf.PI);
        public static LLtude o7 = new LLtude(-Mathf.PI / 2f, 0f);
        public static LLtude a1 = RatioPoint(o1, o2, rho);
        public static LLtude a1x = new LLtude(a1.phi, 0.4f * Mathf.PI);
        public static LLtude a2 = RatioPoint(o1, o2, 1f - rho);
        public static LLtude a2x = new LLtude(a2.phi, 0.4f * Mathf.PI);
        public static LLtude a3 = RatioPoint(o2, o3, rho);
        public static LLtude a4 = RatioPoint(o2, o3, 1f - rho);
        public static LLtude a5 = RatioPoint(o2, o5, rho);
        public static LLtude a6 = RatioPoint(o3, o5, rho);
        public static LLtude a6x = new LLtude(a5.phi, a5.lambda + 0.4f * Mathf.PI);
        public static LLtude a7 = RatioPoint(o2, o5, 1f - rho);
        public static LLtude a8 = RatioPoint(o3, o5, 1f - rho);
        public static LLtude a9 = RatioPoint(o4, o5, 1f - rho);
        public static LLtude a10 = RatioPoint(o5, o6, rho);
        public static LLtude a10x = new LLtude(a9.phi, a9.lambda + 0.4f * Mathf.PI);
        public static LLtude a11 = RatioPoint(o5, o7, rho);
        public static LLtude a12 = RatioPoint(o5, o7, 1f - rho);
        public static LLtude a12x = new LLtude(a12.phi, a12.lambda + 0.4f * Mathf.PI);
        public static float bigAngle = AngularBetween(a1, a1x) * 180f / Mathf.PI;
        public static float smallAngle = AngularBetween(a1, a2) * 180f / Mathf.PI;

        public static LLtude RatioPoint(LLtude p0, LLtude p1, float rho)
        {
            Vector3 q0 = LLtude.UnitPoint(p0);
            Vector3 q1 = LLtude.UnitPoint(p1);
            Vector3 q = (1f - rho) * q0 + rho * q1;
            return LLtude.FromPosition(q);
        }
        
        public static LLtude AngularRatioPoint(LLtude p0, LLtude p1, float rho)
        {
            float l = Mathf.Sqrt(2f - 2f * Mathf.Cos(p0.phi) * Mathf.Cos(p1.phi) * Mathf.Cos(p1.lambda - p0.lambda)
                - 2f * Mathf.Sin(p0.phi) * Mathf.Sin(p1.phi));
            float theta =  2f * Mathf.Asin(0.5f * l);
            float d = Mathf.Sin(0.5f * theta) - Mathf.Tan(0.5f * theta - rho * theta) * Mathf.Cos(0.5f * theta);
            float ratio = d / l;
            return RatioPoint(p0, p1, ratio);
        }

        public static float AngularBetween(LLtude p0, LLtude p1)
        {
            float l = Mathf.Sqrt(2f - 2f * Mathf.Cos(p0.phi) * Mathf.Cos(p1.phi) * Mathf.Cos(p1.lambda - p0.lambda)
                - 2f * Mathf.Sin(p0.phi) * Mathf.Sin(p1.phi));
            return 2f * Mathf.Asin(0.5f * l);
        }
    }
}
