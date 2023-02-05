using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using C5;

public static class AStar
{
    public static Path GetShortestPath(Node start, Node end, Dictionary<string, Node> allNodes)
    {
        List<Node> path = new List<Node>();
        var openHeap = new IntervalHeap<Node>(new NodeComparer());
        start.Open = true;
        openHeap.Add(start);

        Node current;

        while (!openHeap.IsEmpty)
        {
            current = openHeap.DeleteMin();
            allNodes[current.Id].Open = false;
            allNodes[current.Id].Closed = true;

            if (current.Id == end.Id)
            {
                while (current.Id != start.Id)
                {
                    path.Add(current);
                    current = allNodes[current.ParentId];
                }

                break;
            }

            foreach (NodeConnection nc in current.ConnectedNodes)
            {
                string i = nc.Node.Id;
                if (allNodes[i].Open || allNodes[i].Closed) { continue; }

                allNodes[i].G = allNodes[current.Id].G + nc.EdgeWeight;
                allNodes[i].F = allNodes[i].G + Vector3.Distance(allNodes[i].Position, end.Position);
                allNodes[i].Open = true;
                allNodes[i].Closed = false;
                allNodes[i].ParentId = current.Id;
                openHeap.Add(allNodes[i]);
            }
        }

        path.Add(start);

        foreach (string s in allNodes.Keys)
        {
            allNodes[s].Open = false;
            allNodes[s].Closed = false;
            allNodes[s].F = 0;
            allNodes[s].G = 0;
            allNodes[s].H = 0;
        }

        return new Path(path);
    }
}
