namespace OrbitalCamera
{
    internal static class FloatExtensions
    {
        internal static float Clamp(this float value, float min, float max)
        {
            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }
    }
}