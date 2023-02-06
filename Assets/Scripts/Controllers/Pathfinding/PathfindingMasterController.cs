using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathfindingMasterController : MonoBehaviour
{
    public List<TestAgentController> TestAgents; // temporary for debugging

    public Dictionary<string, Node> AllNodes { get; set; }
    public List<PathfindingSurfaceController> AllSurfaceControllers { get; set; }
    public List<PathfindingObstacleController> Obstacles { get; set; }

    void Start()
    {
        UnityEditor.SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));
        StartCoroutine(BakeDelay());
    }

    void Update()
    {
        // temporary for debugging
        foreach (var agent in TestAgents)
            if(agent.Path != null)
                DrawPathDebug(agent.Path.Nodes);
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
        AllSurfaceControllers.ForEach(s => s.ConnectNodes());

        // temporary for debugging
        for (int i = 0; i < TestAgents.Count; i++)
            TestAgents[i].AllNodes = AllNodes;
    }

    public static void DrawPathDebug(List<Node> path)
    {
        for (int i = 0; i < path.Count - 1; i++)
            Debug.DrawLine(path[i].Position, path[i + 1].Position, Color.yellow);
    }

    IEnumerator BakeDelay()
    {
        yield return new WaitForSeconds(0.3f);
        BakeNodes();
    }
}
