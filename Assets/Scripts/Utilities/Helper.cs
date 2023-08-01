using UnityEngine;

namespace Utilities
{
    public static class Helper
    {
        public static Vector3 LerpParabola(Vector3 a, Vector3 b, float t, float k)
        {
            return Vector3.Lerp(a, b, FParabola(t, k));
        }

        private static float FParabola(float t, float k)
        {
            return Mathf.Pow(4f * t * (1f - t), k);
        }
    }
}