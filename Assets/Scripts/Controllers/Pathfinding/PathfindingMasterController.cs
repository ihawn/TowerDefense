using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathfindingMasterController : MonoBehaviour
{
    public Dictionary<string, Node> AllNodes { get; set; }

    public List<PathfindingSurfaceController> AllSurfaceControllers { get; set; }

    public List<PathfindingObstacleController> Obstacles { get; set; }

    void Start()
    {
        BakeNodes();
    }

    void Update()
    {

    }

    public void BakeNodes()
    {
        AllSurfaceControllers = FindObjectsOfType<PathfindingSurfaceController>().ToList();
        Obstacles = FindObjectsOfType<PathfindingObstacleController>().ToList();
        AllNodes = new Dictionary<string, Node>();
        foreach (var surfaceController in AllSurfaceControllers)
        {
            surfaceController.Obstacles = Obstacles;
            surfaceController.GenerateNodes();
            surfaceController.Nodes.ForEach(n => AllNodes[n.Id] = n);
        }
    }

    public static void DrawPathDebug(List<Node> path)
    {
        for (int i = 0; i < path.Count - 1; i++)
            Debug.DrawLine(path[i].Position, path[i + 1].Position, Color.red);
    }
}
