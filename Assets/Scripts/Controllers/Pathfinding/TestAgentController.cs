using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//controller for pathfinding test agent
public class TestAgentController : MonoBehaviour
{
    public float Speed;
    public float Distance;

    public Path Path { get; set; }
    public Dictionary<string, Node> AllNodes { get; set; }

    string StartKey = null;
    string EndKey = null;

    void Start()
    {
    }

    void Update()
    {
        if(Path == null || Distance > Path.PathLength || !Path.Distances.Any())
        {
            Distance = 0;
            EndKey = StartKey ?? AllNodes.Keys.OrderBy(x => Random.Range(0f, 1f)).First();
            StartKey = AllNodes.Keys.Where(x => x != EndKey).OrderBy(x => Random.Range(0f, 1f)).First();
            Node start = AllNodes[StartKey];
            Node end = AllNodes[EndKey];
            Path = AStar.GetShortestPath(start, end, AllNodes);
        }

        Distance += Speed * Time.deltaTime;

        if(Path.Distances.Any())
            transform.position = Path.GetPositionAlongPath(Distance);
    }
}
