using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Node
{
    public List<NodeConnection> ConnectedNodes { get; set; }
    public Vector3 Position { get; set; }
    public string Id { get; set; }

    public Node(Vector3 position)
    {
        Position = position;
        ConnectedNodes = new List<NodeConnection>();
        Id = Guid.NewGuid().ToString();
    }
}
