using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

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
    public bool IsBorderNode { get; set; }


    public Node(Vector3 position, bool isBorderNode = false)
    {
        Position = position;
        ConnectedNodes = new List<NodeConnection>();
        Id = Guid.NewGuid().ToString();
        IsBorderNode = isBorderNode;
    }
}

public static class NodeExtensions
{
    public static Node GetShortestNodeToPoint(List<Node> nodes, Vector3 point)
    {
        return nodes.Aggregate((a, b) =>
            Vector3.Distance(a.Position, point) < Vector3.Distance(b.Position, point) ? a : b);
    }
}

public class NodeComparer : IComparer<Node>
{
    public int Compare(Node x, Node y)
    {
        return x.F.CompareTo(y.F);
    }
}

