using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RectangularPathfindingSurfaceController : PathfindingSurfaceController
{
    public int NodeDensity = 10;
    public int ConnectionsPerNode = 8;
    public float BorderBuffer = 0.2f;

    void Start()
    {
        Collider = GetComponent<BoxCollider>();
        GenerateNodes();
    }

    void Update()
    {
        DrawNodeConnections();
    }

    public override void GenerateNodes()
    {
        Nodes = new List<Node>();

        float xStep = SurfaceBounds.x / NodeDensity;
        float zStep = SurfaceBounds.z / NodeDensity;

        for(float x = transform.position.x - SurfaceBounds.x / 2 + BorderBuffer; 
            x < transform.position.x + SurfaceBounds.x / 2 - BorderBuffer; 
            x += xStep)
            for (float z = transform.position.z - SurfaceBounds.z / 2 + BorderBuffer; 
                z < transform.position.z + SurfaceBounds.z / 2 - BorderBuffer; 
                z += xStep)
                Nodes.Add(new Node(new Vector3(x, transform.position.y, z)));
            
        for(int i = 0; i < Nodes.Count; i++)
            Nodes[i].ConnectedNodes = Nodes
                .Where(n => n.Id != Nodes[i].Id)
                .Select(n => new NodeConnection(n, Vector3.Distance(Nodes[i].Position, n.Position)))
                .OrderBy(c => c.EdgeWeight)
                .Take(ConnectionsPerNode)
                .ToList();
    }

    void DrawNodeConnections()
    {
        Nodes.ForEach(n1 =>
            n1.ConnectedNodes.ForEach(n2 => Debug.DrawLine(n1.Position, n2.Node.Position))
        );
    }
}
