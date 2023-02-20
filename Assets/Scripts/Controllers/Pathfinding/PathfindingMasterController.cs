using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathfindingMasterController : MonoBehaviour
{
    public Dictionary<string, Node> AllNodes { get; set; }
    public Node GoalNode { get; set; }
    public List<PathfindingSurfaceController> AllSurfaceControllers { get; set; }
    public List<PathfindingObstacleController> Obstacles { get; set; }
    public GameManager GameManager { get; set; }

    void Start()
    {
        UnityEditor.SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));
        StartCoroutine(BakeDelay());
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
        AllSurfaceControllers.ForEach(s => s.AddSharedNodes());
        AllSurfaceControllers.ForEach(s => s.ConnectNodes());

        //verify that all node connections are mutual
        foreach(KeyValuePair<string, Node> kvp in AllNodes)
        {

            for (int j = 0; j < AllNodes[kvp.Key].ConnectedNodes.Count; j++)
                AllNodes[kvp.Key].ConnectedNodes[j].Node.ConnectedNodes.Add(
                    new NodeConnection(
                        AllNodes[kvp.Key],
                        Vector3.Distance(AllNodes[kvp.Key].Position, AllNodes[kvp.Key].ConnectedNodes[j].Node.Position)
                    )
                );
        }
       foreach (KeyValuePair<string, Node> kvp in AllNodes)
            AllNodes[kvp.Key].ConnectedNodes = 
                AllNodes[kvp.Key].ConnectedNodes.GroupBy(x => x.Node.Id).Select(y => y.First()).ToList();



        Vector3 goalPosition = FindObjectOfType<GoalController>().gameObject.transform.position;
        GoalNode = NodeExtensions.GetShortestNodeToPoint(AllNodes.Values.ToList(), goalPosition);

        // temporary for debugging
        GameManager.SpawnControllers[0].SpawnAgent("TestAgent");
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
