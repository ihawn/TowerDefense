using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RectangularPathfindingSurfaceController : PathfindingSurfaceController
{
    public int NodeDensity = 10;
    public int ConnectionsPerNode = 8;
    public float BorderBuffer = 0.2f;
    public bool DrawDebugPaths;

    void Awake()
    {
        Collider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (DrawDebugPaths)
            DrawNodeConnectionsDebug();
    }

    public override void GenerateNodes()
    {
        Nodes = new List<Node>();

        float xStep = SurfaceBounds.x / NodeDensity;
        float zStep = SurfaceBounds.z / NodeDensity;

        for (float x = transform.position.x - SurfaceBounds.x / 2 + BorderBuffer;
            x < transform.position.x + SurfaceBounds.x / 2 - BorderBuffer;
            x += xStep)
            for (float z = transform.position.z - SurfaceBounds.z / 2 + BorderBuffer;
                z < transform.position.z + SurfaceBounds.z / 2 - BorderBuffer;
                z += zStep)
            {
                Vector3 position = new Vector3(x, transform.position.y, z);

                Collider colliderContainingPoint = Obstacles
                    .Select(x => x.Boundary)
                    .FirstOrDefault(x => Vector3.Distance(x.ClosestPoint(position), position) < 0.001f);

                if (colliderContainingPoint != null)
                {
                    if (colliderContainingPoint is BoxCollider)
                        Nodes.AddRange(
                            GenerationHelper.GenerateWeightedPointsArountRect((BoxCollider)colliderContainingPoint, position, NodeDensity / 100f)
                                .Select(pos => new Node(pos, isBorderNode: true))
                        );
                }
                else
                    Nodes.Add(new Node(position));
            }

        for (int i = 0; i < Nodes.Count; i++)
        {
            List<Collider> closeObstacles = Obstacles.Where(o =>
                Vector3.Distance(o.Boundary.ClosestPoint(Nodes[i].Position), Nodes[i].Position)
                < ObstacleConsiderationThresholdDistance)
                .Select(o => o.Boundary).ToList();

            Nodes[i].ConnectedNodes = Nodes
                .Where(n => n.Id != Nodes[i].Id)
                .Where(n => !GenerationHelper.LinePassesThroughColliders(closeObstacles, Nodes[i].Position, n.Position))
                .Select(n => new NodeConnection(n, Vector3.Distance(Nodes[i].Position, n.Position)))
                .OrderBy(c => c.EdgeWeight)
                .Take(ConnectionsPerNode)
                .ToList();
        }
    }

    void DrawNodeConnectionsDebug()
    {
        Nodes.ForEach(n1 =>
            n1.ConnectedNodes.ForEach(n2 => Debug.DrawLine(n1.Position, n2.Node.Position))
        );
    }
}
