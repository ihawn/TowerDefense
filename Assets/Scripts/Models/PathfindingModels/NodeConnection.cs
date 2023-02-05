using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeConnection
{
    public Node Node { get; set; }
    public float EdgeWeight { get; set; }

    public NodeConnection(Node node, float weight)
    { 
        Node = node;
        EdgeWeight = weight;
    }
}
