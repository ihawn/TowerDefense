using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPooler : MonoBehaviour
{
    public string Name;

    public GameObject objectToPool;
    public int poolSize;

    private List<GameObject> pooledObjects;

    private void Start()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        GameObject obj = pooledObjects.FirstOrDefault(x => !x.activeInHierarchy);

        if (obj == null)
        {
            obj = Instantiate(objectToPool);
            pooledObjects.Add(obj);
        }

        obj.SetActive(true);
        
        if(obj.GetComponent<AgentController>() != null)
            GlobalReferences.gm.AgentMasterController.Agents.Add(obj.GetComponent<AgentController>());

        return obj;
    }
}