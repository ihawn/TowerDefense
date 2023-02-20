using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class TestAgentController : AgentController
{ 
    void Update()
    {
        if (AllNodes == null || !AllNodes.Any())
            return;

        if (Path == null || !Path.Distances.Any())
        {
            Distance = 0;
            Path = AStar.GetShortestPath(StartNode.Id, EndNode.Id, AllNodes);
        }

        if (Path != null && Distance > 0.95f * Path.PathLength)
            gameObject.SetActive(false);

        Distance += Speed * Time.deltaTime;

        if (Path.Distances.Any())
            transform.position = Path.GetPositionAlongPath(Distance);
    }

    public override void ConnectToPathGraph()
    {
        StartNode = NodeExtensions.GetShortestNodeToPoint(AllNodes.Values.ToList(), transform.position);
        EndNode = GlobalReferences.gm.PathfindingMasterController.GoalNode;
    }
}
