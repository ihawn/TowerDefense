using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dictionary<string, ObjectPooler> ObjectPoolers { get; set; }
    public GameObject MasterPooler;

    public List<SpawnController> SpawnControllers { get; set; }
    public GameObject MasterSpawner;

    public PathfindingMasterController PathfindingMasterController;
    public AgentMasterController AgentMasterController;

    public GameObject Goal;

    public LayerMask LineOfSightMask;

    public bool RunPathfinding;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        GlobalReferences.gm = this;
        InitPoolers();
        InitSpawners();
    }

    void Update()
    {

    }

    void InitPoolers()
    {
        ObjectPoolers = new Dictionary<string, ObjectPooler>();
        int childCount = MasterPooler.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = MasterPooler.transform.GetChild(i);
            ObjectPooler pooler = child.GetComponent<ObjectPooler>();
            if (pooler != null)
            {
                ObjectPoolers[pooler.Name] = pooler;
            }
        }
    }

    void InitSpawners()
    {
        SpawnControllers = new List<SpawnController>();
        int childCount = MasterSpawner.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = MasterSpawner.transform.GetChild(i);
            SpawnController spawner = child.GetComponent<SpawnController>();
            if (spawner != null)
            {
                SpawnControllers.Add(spawner);
            }
        }
    }
}
