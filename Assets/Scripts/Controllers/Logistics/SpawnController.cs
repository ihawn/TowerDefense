using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public BoxCollider SpawnBounds;
    public float SpawnIntervalMin;
    public float SpawnIntervalMax;

    void Start()
    {
        SpawnBounds = GetComponent<BoxCollider>();
        StartCoroutine(SpawnAtIntervals());
    }

    void Update()
    {
        
    }

    public void SpawnAgent(string agent)
    {
        AgentController newAgent = GlobalReferences.gm.ObjectPoolers[agent].GetPooledObject().GetComponent<AgentController>();

        Vector3 bounds = SpawnBounds.bounds.size;
        newAgent.gameObject.transform.position =
            SpawnBounds.transform.TransformPoint(Random.Range(-bounds.x, bounds.x), 0, Random.Range(-bounds.z, bounds.z)) + SpawnBounds.center;

        newAgent.ConnectToPathGraph();
    }

    IEnumerator SpawnAtIntervals()
    {
        while (true)
        {
            if(GlobalReferences.gm.PathfindingMasterController.GraphBakeComplete)
                SpawnAgent("TestAgent");
            yield return new WaitForSeconds(Random.Range(SpawnIntervalMin, SpawnIntervalMax));
        }
    }
}
