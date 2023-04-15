using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class StationaryDefense : Controller
{
    public float ShootAngleThreshold;
    public float ShootTimeThreshold;
    public float ShootDistanceThreshold;
    public float TrackingSpeed;
    public EnemyTargetingPriority EnemyTargetingPriority;
    public bool LineOfSightOnly;

    public AgentController Target;
    public ObjectPooler ProjectilePool;

    private void OnEnable()
    {
        StartCoroutine(ShootLoop());
    }

    private void Update()
    {
        if (GlobalReferences.gm.GameActive)
        {
            if (Target == null || (Target != null && !Target.gameObject.activeInHierarchy) || (LineOfSightOnly && !TargetIsLOS()))
                Target = GetTarget();
            UpdateWeaponTransform();
        }
        else
            Target = null;
    }

    public abstract void Shoot();
    public abstract bool CanShoot();
    public abstract void UpdateWeaponTransform();
    public abstract bool InRange(AgentController a);

    AgentController GetTarget()
    {
        List<AgentController> candidates = GlobalReferences.gm.AgentMasterController.Agents
            .Where(a => a.gameObject.activeInHierarchy && a.IsPossibleTarget && InRange(a) && (!LineOfSightOnly || TargetIsLOS(a))).ToList();
        if (candidates.Count == 0) return null;

        Vector3 goalPosition = GlobalReferences.gm.Goal.transform.position;
        switch (EnemyTargetingPriority)
        {
            case EnemyTargetingPriority.Random:
                return candidates.ElementAt(Random.Range(0, candidates.Count));

            case EnemyTargetingPriority.ClosestToGoal:
                return candidates.Aggregate((a, b) => Vector3.Distance(a.transform.position, goalPosition) < Vector3.Distance(b.transform.position, goalPosition) ? a : b);

            case EnemyTargetingPriority.ClosestToSelf:
                return candidates.Aggregate((a, b) => Vector3.Distance(a.transform.position, transform.position) < Vector3.Distance(b.transform.position, transform.position) ? a : b);

            case EnemyTargetingPriority.HighestHealth:
                return candidates.Aggregate((a, b) => a.Health > b.Health ? a : b);
        }
        return GlobalReferences.gm.AgentMasterController.Agents.FirstOrDefault(a => a.IsPossibleTarget && InRange(a));
    }

    public bool TargetIsLOS()
    {
        if(Target == null)
            return false;

        return !Physics.Linecast(transform.position, Target.transform.position, GlobalReferences.gm.LineOfSightMask);
    }

    public bool TargetIsLOS(AgentController targetCandidate)
    {
        return !Physics.Linecast(transform.position, targetCandidate.transform.position, GlobalReferences.gm.LineOfSightMask);
    }

    public IEnumerator ShootLoop()
    {
        while (gameObject.activeInHierarchy)
        {
            if (Target != null && CanShoot())
            {
                Shoot();
                yield return new WaitForSeconds(ShootTimeThreshold);
            }

            yield return null;
        }
    }
}

public enum EnemyTargetingPriority
{
    Random = 0,
    ClosestToGoal = 1,
    ClosestToSelf = 2,
    HighestHealth = 3
}
