using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float MoveSpeed;

    void OnEnable()
    {
        StartCoroutine(BulletDeath());
    }

    void Update()
    {
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;
    } 

    IEnumerator BulletDeath()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
