using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    GameManager gameManager;
    public float MoveSpeed;
    Vector3 shootDirection;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        shootDirection = Vector3.Normalize(gameManager.Enemy.transform.position - transform.position);

        StartCoroutine(BulletDeath());
    }

    void Update()
    {
        transform.position += shootDirection * MoveSpeed * Time.deltaTime;
    }

    IEnumerator BulletDeath()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
