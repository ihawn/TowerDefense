using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StationaryDefense : MonoBehaviour
{
    public float ShootAngleThreshold;
    public float ShootTimeThreshold;
    public float ShootDistanceThreshold;
    public float TrackingSpeed;

    public AgentController Target;
    public ObjectPooler ProjectilePool;

    private void OnEnable()
    {
        StartCoroutine(ShootLoop());
    }

    private void Update()
    {
        if (Target == null)
            Target = GetTarget();
        UpdateWeaponTransform();
    }

    public abstract void Shoot();
    public abstract AgentController GetTarget();
    public abstract bool CanShoot();
    public abstract void UpdateWeaponTransform();

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
