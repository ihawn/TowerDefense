using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConroller : MonoBehaviour
{
    public GameManager gameManager;
    public float MoveSpeed = 5;
    private Rigidbody rb;
    public Vector3 initialPosision;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosision = transform.position;
    }


    void Update()
    {
        MoveTowardsGoal();
    }

    public void Respawn()
    {
        BoxCollider spawnArea = gameManager.EnemySpawner.GetComponent<BoxCollider>();
        Vector3 spawnOrigin = spawnArea.gameObject.transform.position;
        Vector3 boundSize = spawnArea.bounds.size;
        Vector3 spawnPosition = new Vector3(Random.Range(spawnOrigin.x - boundSize.x / 2, spawnOrigin.x + boundSize.x / 2), spawnOrigin.y, spawnOrigin.z);

        transform.position = spawnPosition;
    }

    void MoveTowardsGoal()
    {
        transform.position += Vector3.Normalize(gameManager.Goal.transform.position - transform.position) * MoveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            GameObject remains = Instantiate(gameManager.EnemyRemains);
            remains.transform.position = transform.position;
            Respawn();
        }
    }
}
