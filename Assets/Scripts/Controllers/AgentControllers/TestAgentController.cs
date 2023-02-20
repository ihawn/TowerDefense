using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//controller for pathfinding test agent
public class TestAgentController : AgentController
{ 
    public float Speed;
    public float Distance;

    public Path Path { get; set; }
    public Dictionary<string, Node> AllNodes { get { return GameManager.PathfindingMasterController.AllNodes; } }

    Node StartNode { get; set; }
    Node EndNode { get; set; }

    void Update()
    {
        if(GameManager != null)
        {
            if (AllNodes == null || !AllNodes.Any())
                return;

            if (Path == null || !Path.Distances.Any())
            {
                Distance = 0;
                Path = AStar.GetShortestPath(StartNode.Id, EndNode.Id, AllNodes);
            }

            if (Path != null && Distance > Path.PathLength)
                gameObject.SetActive(false);

            Distance += Speed * Time.deltaTime;

            if (Path.Distances.Any())
                transform.position = Path.GetPositionAlongPath(Distance);
        }
    }

    public override void ConnectToPathGraph()
    {
        StartNode = NodeExtensions.GetShortestNodeToPoint(AllNodes.Values.ToList(), transform.position);
        EndNode = GameManager.PathfindingMasterController.GoalNode;
    }
}
