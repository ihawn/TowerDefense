using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TurretController : StationaryDefense
{
    public float ShotSpreadMaxAngle = 5;

    private void Start()
    {
        ProjectilePool = GlobalReferences.gm.ObjectPoolers["TurretBullet"];
    }

    public override bool CanShoot()
    {
        return Target != null 
            && InRange(Target)
            && Quaternion.Angle(
                Quaternion.LookRotation(
                    Vector3.Normalize(Target.transform.position - transform.position)), transform.rotation) < ShootAngleThreshold;
    }

    public override void Shoot()
    {
        float randomAngle = Random.Range(0f, ShotSpreadMaxAngle);
        Vector3 randomDirection = Random.insideUnitSphere;
        Quaternion randomRotation = Quaternion.AngleAxis(randomAngle, randomDirection);

        GameObject bullet = ProjectilePool.GetPooledObject();
        bullet.transform.rotation = transform.rotation * randomRotation;
        bullet.transform.position = transform.position;
    }

    public override void UpdateWeaponTransform()
    {
        if (Target != null && InRange(Target))
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.LookRotation(Vector3.Normalize(Target.transform.position - transform.position)),
                TrackingSpeed * Time.deltaTime
            );
        }
        else
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.identity,
                TrackingSpeed * Time.deltaTime
            );
        }
    }

    public override void ResetState()
    {
        transform.rotation = Quaternion.identity;
        Target = null;
    }

    public override bool InRange(AgentController a)
    {
        return Vector3.Distance(a.transform.position, transform.position) <= ShootDistanceThreshold;
    }

    public override void DoDeath()
    {
        throw new System.NotImplementedException();
    }
}
