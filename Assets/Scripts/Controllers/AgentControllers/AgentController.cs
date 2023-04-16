using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AgentController : Controller
{
    public float Speed;
    public float Distance;
    public bool IsPossibleTarget;
    public bool IsStationary;

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

    public bool CanMove { get { return GlobalReferences.gm.GameActive; } }

    private void OnEnable()
    {
        Health = MaxHealth;
        Distance = 0;
        OnEnableEvents();
    }

    private void Update()
    {
        if (CanMove)
            Movement();
    }

    public abstract void OnEnableEvents();
    public abstract void Movement();
    public abstract void ConnectToPathGraph();
    public abstract void OnGoalReached();
}
