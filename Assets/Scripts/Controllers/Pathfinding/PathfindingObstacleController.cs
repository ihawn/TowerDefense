using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingObstacleController : MonoBehaviour
{
    public Collider Boundary { get; set; }

    void Awake()
    {
        Boundary = GetComponent<Collider>();
    }
}
