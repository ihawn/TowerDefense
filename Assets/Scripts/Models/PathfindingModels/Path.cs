using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Path
{
    public List<Node> Nodes { get; set; }
    public List<float> Distances { get; set; }
    public float PathLength { get; set; }

    public Path(List<Node> nodes)
    {
        Nodes = nodes;

        PathLength = 0;
        Distances = new List<float>();
        for (int i = 1; i < nodes.Count; i++)
        {
            float distance = Vector3.Distance(Nodes[i].Position, Nodes[i - 1].Position);
            Distances.Add(distance);
            PathLength += distance;
        }
    }

    public Vector3 GetPositionAlongPath(float distance)
    {
        Node lastNodeTravelled = null;

        distance = distance % PathLength;
        float travelledDistance = 0;

        int j = 0;
        for (int i = 0; i < Distances.Count; i++)
        {
            travelledDistance += Distances[i];
            if (travelledDistance > distance)
            {
                lastNodeTravelled = Nodes[i];
                j = i;
                break;
            }
        }

        float remainder = Distances[j] - (travelledDistance - distance);
        return lastNodeTravelled.Position + Vector3.Normalize(Nodes[j + 1].Position - lastNodeTravelled.Position) * remainder;
    }
}
