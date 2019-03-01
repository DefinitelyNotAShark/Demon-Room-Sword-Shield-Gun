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

    [SerializeField]
    private ParticleSystem hitParticles, deathParticles;

    private Animator anim;


    private bool coroutineStarted;

    private void Start()
    {
        //Vars
        EnemyLives = enemyLives;//set our base lives to our customized lives

        //passs the particle systems into the base class to be used as general particle systems for any enemy
        HitParticles = hitParticles;
        DeathParticles = deathParticles;

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (EnemyIsDead())
        {
            //the death state behavior will take care of death effects
            anim.SetBool("IsDead", true);
        }
    }
}
