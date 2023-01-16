using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PathfindingSurfaceController : MonoBehaviour
{
    public List<Node> Nodes { get; set; }

    public Collider Collider { get; set; }

    public Vector3 SurfaceBounds { get { return Collider.bounds.size; } }

    public abstract void GenerateNodes();
}
