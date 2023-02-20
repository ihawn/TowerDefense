using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentController : MonoBehaviour
{
    public Agent Agent { get; set; }

    public GameManager GameManager { get; set; }

    public abstract void ConnectToPathGraph();
}
