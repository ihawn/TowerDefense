using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRemainsController : MonoBehaviour
{
    float force = 5;
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(
            new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * force
        );

        StartCoroutine(RemainsDeath());
    }

    IEnumerator RemainsDeath()
    {
        yield return new WaitForSeconds(Random.Range(10f, 15f));
        Destroy(gameObject);
    }
}
