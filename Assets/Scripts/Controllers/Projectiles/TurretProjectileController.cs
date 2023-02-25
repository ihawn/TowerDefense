using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TurretProjectileController : ProjectileController
{
    public override void ProjectileCollision(Collision collision)
    {
        Collided = true;
        GameObject particles = GlobalReferences.gm.ObjectPoolers["TurretBulletImpact"].GetPooledObject();
        particles.transform.position = collision.contacts[0].point;
        particles.transform.up = collision.contacts[0].normal;
        particles.transform.parent = null;
        gameObject.SetActive(false);
    }

    public override void ProjectileMovement()
    {
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;
    }
}
