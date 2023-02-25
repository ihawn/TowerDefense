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
        List<AgentController> candidates = GlobalReferences.gm.AgentMasterController.Agents.Where(a => InRange(a) && (!LineOfSightOnly || TargetIsLOS(a))).ToList();
        if (candidates.Count == 0) return null;

        Vector3 goalPosition = GlobalReferences.gm.Goal.transform.position;
        switch (EnemyTargetingPriority)
        {
            case EnemyTargetingPriority.Random:
                return candidates.ElementAt(Random.Range(0, candidates.Count));

            case EnemyTargetingPriority.ClosestToGoal:
                return candidates.Aggregate((a, b) => Vector3.Distance(a.transform.position, goalPosition) < Vector3.Distance(b.transform.position, goalPosition) ? a : b);

            case EnemyTargetingPriority.HighestHealth:
                return candidates.Aggregate((a, b) => a.Health > b.Health ? a : b);
        }    
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
        return Vector3.Distance(a.transform.position, transform.position) <= ShootDistanceThreshold;
    }
}
