using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PathfindingSurfaceController : MonoBehaviour
{
    public List<Node> Nodes { get; set; }

    public Collider Collider { get; set; }

    public List<PathfindingObstacleController> Obstacles { get; set; }

    public List<PathfindingSurfaceController> ConnectedSurfaces { get; set; }

    public float ObstacleConsiderationThresholdDistance;


    public abstract void GenerateNodes();

    public abstract void ConnectNodes();

    private void OnTriggerEnter(Collider other)
    {
        if(ConnectedSurfaces == null)
            ConnectedSurfaces = new List<PathfindingSurfaceController>();

        PathfindingSurfaceController adjacentSurface = other.gameObject.GetComponent<PathfindingSurfaceController>();
        if(adjacentSurface != null)
        {
            ConnectedSurfaces.Add(adjacentSurface);
        }
    }
}
