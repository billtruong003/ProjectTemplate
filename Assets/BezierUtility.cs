using UnityEngine;

public static class BezierUtility
{
    public static Vector3 GetBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 point = uu * p0; // (1-t)^2 * p0
        point += 2 * u * t * p1; // 2 * (1-t) * t * p1
        point += tt * p2;        // t^2 * p2

        return point;
    }
}
