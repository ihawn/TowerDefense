using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class TestAgentController : AgentController
{
    private MeshRenderer meshRenderer;


    public override void OnEnableEvents()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    public override void Movement()
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
        {
            if (!meshRenderer.enabled)
                meshRenderer.enabled = true;
            transform.position = Path.GetPositionAlongPath(Distance);
        }
    }

    public override void ConnectToPathGraph()
    {
        StartNode = NodeExtensions.GetShortestNodeToPoint(AllNodes.Values.ToList(), transform.position);
        EndNode = GlobalReferences.gm.PathfindingMasterController.GoalNode;
    }

    public override void DoDeath()
    {
        GameObject particles = GlobalReferences.gm.ObjectPoolers["TestAgentDeathParticles"].GetPooledObject();
        particles.transform.position = transform.position;
        gameObject.SetActive(false);
    }

    public override void ResetState()
    {
        gameObject.SetActive(false);
    }
}
