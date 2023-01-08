using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class TurretController : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject Bullet;
    public float TrackingSpeed;
    public float Range;

    public float ShootTimer;
    public float ShootTimeThreshold;
    public float ShootAngleThreshold;

    public bool CanShoot
    { 
        get
        {
            return Vector3.Distance(transform.position, gameManager.Enemy.transform.position) < Range;
        } 
    }    

    void Start()
    {
        ShootTimer = ShootTimeThreshold;
    }

    void Update()
    {
        UpdateTurret();
        ShootLoop();
    }

    void ShootLoop()
    {
        if (CanShoot && 
            Quaternion.Angle(
                Quaternion.LookRotation(
                    Vector3.Normalize(transform.position - gameManager.Enemy.transform.position)), transform.rotation) < ShootAngleThreshold)
        {
            if (ShootTimer >= ShootTimeThreshold)
            {
                ShootTimer = 0;
                Shoot();
            }
            ShootTimer += Time.deltaTime;
        }
        else
            ShootTimer = ShootTimeThreshold;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(Bullet);
        bullet.transform.rotation = transform.rotation;
        bullet.transform.position = transform.position;
    }

    void UpdateTurret()
    {
        if (CanShoot)
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.LookRotation(Vector3.Normalize(transform.position - gameManager.Enemy.transform.position)),
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
}