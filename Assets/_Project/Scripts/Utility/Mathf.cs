using UnityEngine;

namespace ChopChop.Utility
{
    public static class Math
    {
        public static float GetAngle0360(Vector3 from, Vector3 to, Vector3 axis)
        {
            float angle = Vector3.SignedAngle(from, to, axis);

            if (angle < 0)
            {
                angle = 360 - (angle * -1);
            }

            return angle;
        }

        public static float Clamp(float value, float max, float min = 0.0f)
        {
            value %= max;

            if (value < 0.0f)
                value = max + value;

            if (value < min)
                value = min;

            return value;
        }
    }
}