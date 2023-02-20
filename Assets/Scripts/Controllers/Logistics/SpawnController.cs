using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameManager gameManager;
    public BoxCollider SpawnBounds;

    void Start()
    {
        SpawnBounds = GetComponent<BoxCollider>();
    }

    void Update()
    {
        
    }

    public void SpawnAgent(string agent)
    {
        AgentController newAgent = gameManager.ObjectPoolers[agent].GetPooledObject().GetComponent<AgentController>();
        newAgent.gameObject.SetActive(true);

        Vector3 bounds = SpawnBounds.bounds.size;
        newAgent.gameObject.transform.position =
            SpawnBounds.transform.TransformPoint(Random.Range(-bounds.x, bounds.x), 0, Random.Range(-bounds.z, bounds.z)) + SpawnBounds.center;

        newAgent.GameManager = gameManager;
        newAgent.ConnectToPathGraph();
    }
}
