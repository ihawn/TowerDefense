using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Node
{
    public List<NodeConnection> ConnectedNodes { get; set; }
    public Vector3 Position { get; set; }
    public string Id { get; set; }
    public string ParentId { get; set; }
    public float F { get; set; }
    public float G { get; set; }
    public float H { get; set; }
    public bool Open { get; set; }
    public bool Closed { get; set; }


    public Node(Vector3 position)
    {
        Position = position;
        ConnectedNodes = new List<NodeConnection>();
        Id = Guid.NewGuid().ToString();
    }
}

public class NodeComparer : IComparer<Node>
{
    public int Compare(Node x, Node y)
    {
        return x.F.CompareTo(y.F);
    }
}

