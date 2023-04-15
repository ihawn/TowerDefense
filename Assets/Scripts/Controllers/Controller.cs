using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public float Health;
    public float MaxHealth;

    public float Damage;
    public Side Side;

    public abstract void DoDeath();
    public abstract void ResetState();

    private void OnEnable()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        Health = MaxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Controller obj = collision.gameObject.GetComponent<Controller>();
        DoDamage(obj);
    }

    private void OnTriggerEnter(Collider other)
    {
        Controller obj = other.gameObject.GetComponent<Controller>();
        DoDamage(obj);
    }

    void DoDamage(Controller obj)
    {
        if (obj != null && Side != obj.Side)
        {
            Health -= obj.Damage;
            if (Health <= 0)
                DoDeath();
        }
    }

}

public enum Side
{
    Offense = 0,
    Defense = 1
}