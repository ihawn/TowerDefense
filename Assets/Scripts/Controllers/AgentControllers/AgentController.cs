using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentController : MonoBehaviour
{
    public float Speed;
    public float Distance;

    public Path Path { get; set; }
    public Dictionary<string, Node> AllNodes { get { return GlobalReferences.gm.PathfindingMasterController.AllNodes; } }

    public Node StartNode { get; set; }
    public Node EndNode { get; set; }

    private void OnEnable()
    {
        Distance = 0;
    }


    public abstract void ConnectToPathGraph();

    public bool IsPossibleTarget;
}
