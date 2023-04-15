using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : Controller
{
    public override void DoDeath()
    {
        Debug.Log("Game over");
        GlobalReferences.gm.GameOver();
    }

    public override void ResetState()
    {
        
    }
}
