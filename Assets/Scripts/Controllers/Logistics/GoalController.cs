using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    public float Health;
    public float MaxHealth;

    private void OnEnable()
    {
        Health = MaxHealth;
    }
}
