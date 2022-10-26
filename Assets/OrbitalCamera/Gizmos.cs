using UnityEditor;
using UnityEngine;

namespace OrbitalCamera
{
    internal static class Gizmos
    {
        internal static void Circle(Vector3 center, float radius, Vector3 normal)
        {
            const int segmentCount = 128;
            const float step = 360f / segmentCount;

            var points = new Vector3[segmentCount];
            var tangent = Vector3.Slerp(normal, -normal, 0.5f).normalized;

            for (int i = 0; i < segmentCount; i++)
            {
                points[i] = center + tangent * radius;
                tangent = Quaternion.AngleAxis(step, normal) * tangent;
            }

            for (int i = 0; i < segmentCount; i++)
            {
                var a = points[i];
                var b = points[(i + 1) % segmentCount];
                Handles.DrawAAPolyLine(3, a, b);
            }
        }
    }
}