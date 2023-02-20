using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using C5;

public static class AStar
{
    public static Path GetShortestPath(string startId, string endId, Dictionary<string, Node> allNodes)
    {
        List<Node> path = new List<Node>();
        var openHeap = new IntervalHeap<Node>(new NodeComparer());
        allNodes[startId].Open = true;
        openHeap.Add(allNodes[startId]);
        
        var nod = allNodes[startId];

        Node current;

        while (!openHeap.IsEmpty)
        {
            current = openHeap.DeleteMin();
            allNodes[current.Id].Open = false;
            allNodes[current.Id].Closed = true;

            if (current.Id == endId)
            {
                while (current.Id != startId)
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
                allNodes[i].F = allNodes[i].G + Vector3.Distance(allNodes[i].Position, allNodes[endId].Position);
                allNodes[i].Open = true;
                allNodes[i].Closed = false;
                allNodes[i].ParentId = current.Id;
                openHeap.Add(allNodes[i]);
            }
        }

        path.Add(allNodes[startId]);

        foreach (string s in allNodes.Keys)
        {
            allNodes[s].Open = false;
            allNodes[s].Closed = false;
            allNodes[s].F = 0;
            allNodes[s].G = 0;
            allNodes[s].H = 0;
        }

        path.Reverse();
        return new Path(path);
    }
}
