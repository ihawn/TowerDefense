using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Goal;
    public GameObject Enemy;
    public GameObject EnemySpawner;
    public GameObject EnemyRemains;

    public bool GameOver;

    void Start()
    {
        
    }

    void Update()
    {
        if(GameOver)
        {
            GameOver = false;
            Enemy.GetComponent<EnemyConroller>().Respawn();
        }
    }
}
