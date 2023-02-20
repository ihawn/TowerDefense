using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TurretController : StationaryDefense
{
    private void Start()
    {
        ProjectilePool = GlobalReferences.gm.ObjectPoolers["TurretBullet"];
    }

    public override bool CanShoot()
    {
        return Target != null && InRange(Target);
    }

    public override AgentController GetTarget()
    {
        var s = GlobalReferences.gm.AgentMasterController.Agents.FirstOrDefault(a => a.IsPossibleTarget && InRange(a));
        return GlobalReferences.gm.AgentMasterController.Agents.FirstOrDefault(a => a.IsPossibleTarget && InRange(a));
    }

    public override void Shoot()
    {
        GameObject bullet = ProjectilePool.GetPooledObject();
        bullet.transform.rotation = transform.rotation;
        bullet.transform.position = transform.position;
    }

    public override void UpdateWeaponTransform()
    {
        if (Target != null && !Target.gameObject.activeInHierarchy)
            Target = null;

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

    bool InRange(AgentController a)
    {
        float f = Vector3.Distance(a.transform.position, transform.position);
        return Vector3.Distance(a.transform.position, transform.position) <= ShootDistanceThreshold;
    }
}
