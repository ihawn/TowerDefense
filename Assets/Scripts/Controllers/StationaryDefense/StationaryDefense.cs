using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StationaryDefense : MonoBehaviour
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
        if (Target == null || (Target != null && LineOfSightOnly && !TargetIsLOS()))
            Target = GetTarget();
        UpdateWeaponTransform();
    }

    public abstract void Shoot();
    public abstract AgentController GetTarget();
    public abstract bool CanShoot();
    public abstract void UpdateWeaponTransform();

    public bool TargetIsLOS()
    {
        if(Target == null)
            return false;

        return !Physics.Linecast(transform.position, Target.transform.position);
    }

    public bool TargetIsLOS(AgentController targetCandidate)
    {
        return !Physics.Linecast(transform.position, targetCandidate.transform.position);
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
    HighestHealth = 2
}
