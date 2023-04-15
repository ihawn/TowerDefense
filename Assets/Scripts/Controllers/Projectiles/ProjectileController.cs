using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileController : MonoBehaviour
{
    public float Lifetime = 1;
    public float MoveSpeed;
    public float Damage;
    public bool Collided;

    void OnEnable()
    {
        Collided = false;
        StartCoroutine(ProjectileDeath());
    }

    void Update()
    {
        ProjectileMovement();
    }

    public abstract void ProjectileCollision(Collision collision);

    public abstract void ProjectileMovement();

    IEnumerator ProjectileDeath()
    {
        yield return new WaitForSeconds(Lifetime);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "DefenseObject")
        {
            AgentController agent = collision.gameObject.GetComponent<AgentController>();
            if (agent != null && !Collided)
            {
                agent.Health -= Damage;
                if (agent.Health <= 0)
                    agent.DoDeath();
            }

            ProjectileCollision(collision);
        }
    }
}
