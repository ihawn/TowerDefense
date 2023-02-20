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

    void Start()
    {
        InitPoolers();
        InitSpawners();
        InitPathfinder();
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
                pooler.gameManager = this;
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
                spawner.gameManager = this;
                SpawnControllers.Add(spawner);
            }
        }
    }

    void InitPathfinder()
    {
        PathfindingMasterController.GameManager = this;
    }
}
