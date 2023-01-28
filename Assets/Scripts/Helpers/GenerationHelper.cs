using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GenerationHelper
{
    public static List<Vector3> GenerateWeightedPointsArountRect(BoxCollider collider, Vector3 radialPoint, float density, float buffer = 0.05f)
    {
        List<Vector3> points = new List<Vector3>();

        float step = 1f / density;
        for (float theta = 0; theta < Mathf.PI * 2; theta += step)
        {
            float ell = 0.2f;
            float cosTheta = Mathf.Cos(theta);
            float sinTheta = Mathf.Sin(theta);
            Vector3 probe = radialPoint + new Vector3(ell * cosTheta, 0, ell * sinTheta);

            while(Vector3.Distance(collider.ClosestPoint(probe), probe) < 0.001f)
            {
                ell += 0.2f;
                probe = radialPoint + new Vector3(ell * cosTheta, 0, ell * sinTheta);
            }

            Vector3 closestPoint = collider.ClosestPoint(probe);
            points.Add(closestPoint + buffer * (closestPoint - radialPoint));
        }
        return points;
    }

    public static bool LinePassesThroughColliders(List<Collider> colliders, Vector3 point1, Vector3 point2)
    {
        foreach (Collider collider in colliders)
        {
            float step = collider.bounds.size.magnitude / 50f;
            for (float s = 0; s <= 1; s += step)
            {
                Vector3 probe = point1 * s + point2 * (1 - s);
                if (Vector3.Distance(collider.ClosestPoint(probe), probe) < 0.001f)
                    return true;
            }
        }

        return false;
    }
}
