using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RectangularPathfindingSurfaceController : PathfindingSurfaceController
{
    public int NodeDensityX = 10;
    public int NodeDensityZ = 10;
    public int ConnectionsPerNode = 8;
    public bool DrawDebugPaths;

    Vector3 BoxBounds { get { return ((BoxCollider)Collider).size / 2f; } }

    void Awake()
    {
        Collider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (DrawDebugPaths && Nodes != null)
            DrawNodeConnectionsDebug();
    }

    public override void GenerateNodes()
    {
        if(Nodes == null)
            Nodes = new List<Node>();

        float xStep = BoxBounds.x / NodeDensityX;
        float zStep = BoxBounds.z / NodeDensityZ;

        //Create nodes
        for (float x = -BoxBounds.x; x < BoxBounds.x; x += xStep)
            for (float z = -BoxBounds.z; z < BoxBounds.z; z += zStep)
            {
                Vector3 position = Collider.transform.TransformPoint(new Vector3(x, 0, z) + ((BoxCollider)Collider).center);

                Collider colliderContainingPoint = Obstacles
                    .Select(x => x.Boundary)
                    .FirstOrDefault(x => Vector3.Distance(x.ClosestPoint(position), position) < 0.001f);

                if (colliderContainingPoint != null)
                {
                    if (colliderContainingPoint is BoxCollider)
                        Nodes.AddRange(
                            GenerationHelper.GenerateWeightedPointsArountRect(
                                (BoxCollider)colliderContainingPoint, position, 0.5f * (NodeDensityX + NodeDensityZ) / 100f
                            )
                            .Where(x => Vector3.Distance(colliderContainingPoint.ClosestPoint(x), x) < 0.001f)
                            .Select(pos => new Node(pos, isBorderNode: true))
                        );
                }
                else
                    Nodes.Add(new Node(position));
            }

        //Determine which nodes are shared between surfaces
        for(int i = 0; i < ConnectedSurfaces.Count; i++)
        {
            BoxCollider bounds = ConnectedSurfaces[i].GetComponent<BoxCollider>();
            List<Node> sharedNodes = Nodes
                .Where(n => Vector3.Distance(bounds.ClosestPoint(n.Position), n.Position) < 0.001f)
                .ToList();

            if (ConnectedSurfaces[i].Nodes == null)
                ConnectedSurfaces[i].Nodes = new List<Node>();
            ConnectedSurfaces[i].Nodes.AddRange(sharedNodes);
        }
    }

    public override void ConnectNodes()
    {
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
                .Where(c => c.EdgeWeight <= ObstacleConsiderationThresholdDistance)
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
