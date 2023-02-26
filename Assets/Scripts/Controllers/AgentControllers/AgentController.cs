using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AgentController : MonoBehaviour
{
    public float Speed;
    public float Distance;
    public float Health;
    public float MaxHealth;

    private string id;
    public string Id
    { 
        get 
        { 
            if(id == null) 
                id = Guid.NewGuid().ToString();
            return id;
        }
    }
    public Path Path { get; set; }
    public Dictionary<string, Node> AllNodes { get { return GlobalReferences.gm.PathfindingMasterController.AllNodes; } }

    public Node StartNode { get; set; }
    public Node EndNode { get; set; }

    private void OnEnable()
    {
        Health = MaxHealth;
        Distance = 0;
    }


    public abstract void ConnectToPathGraph();

    public abstract void Death();

    public bool IsPossibleTarget;
}
