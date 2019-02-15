using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : Enemy
{
    public float minRange;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float enemyCoolDownLength;

    [SerializeField]
    private int enemyLives;


    private bool coroutineStarted;

    private void Start()
    {
        //Vars
        EnemyLives = enemyLives;//set our base lives to our customized lives
    }

    private void Update()
    {
        if (EnemyIsDead() && !coroutineStarted)//detects if enemy has been killed every frame
            Destroy(this.gameObject);
    }
}
