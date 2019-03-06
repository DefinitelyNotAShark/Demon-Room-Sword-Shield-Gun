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

    private bool hasStartedDeath;//this is so that the enemy doesn't keep entering the death state once it's dead. 

    private void Start()
    {
        //Vars
        EnemyLives = enemyLives;//set our base lives to our customized lives

        //passs the particle systems into the base class to be used as general particle systems for any enemy
        HitParticles = hitParticles;
        DeathParticles = deathParticles;

        anim = GetComponent<Animator>();
        hasStartedDeath = false;
    }

    private void Update()
    {
        if (EnemyIsDead() && !hasStartedDeath)
        {
            //the death state behavior will take care of death effects
            anim.SetTrigger("Death");
            hasStartedDeath = true;
        }

        //check if the base detected a hurt to trigger the animation. Don't do hurt animation AND death animation, so only if the enemy is still alive
        if (isHurt && !EnemyIsDead())
            Hurt();
    }

    private void Hurt()
    {
        anim.SetTrigger("Hurt");
        isHurt = false;//reset the variable

    }
}
