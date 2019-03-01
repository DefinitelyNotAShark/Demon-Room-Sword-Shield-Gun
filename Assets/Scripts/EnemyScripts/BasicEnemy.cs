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

    [SerializeField]
    private AudioClip snarlSound, attackSound;

    [SerializeField]
    private float snarlVolume, attackVolume;

    private Animator anim;
    private AudioSource audio;


    private bool coroutineStarted;
    private float timeElapsed;
    float enemySnarlTime;//the time nwhere we snarlllll

    private void Start()
    {
        //Vars
        EnemyLives = enemyLives;//set our base lives to our customized lives
        HitParticles = hitParticles;
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        timeElapsed = 0;
    }

    private void Update()
    {
        if (EnemyIsDead())
        {
            anim.SetBool("IsDead", true);
        }

        timeElapsed += Time.deltaTime;

        if(timeElapsed >= enemySnarlTime)
        {
            audio.PlayOneShot(snarlSound, snarlVolume);
            Debug.Log("Time to snarl!");
            timeElapsed = 0;
            enemySnarlTime = RandomSnarlTime();
        }
    }

    private float RandomSnarlTime()
    {
        return Random.Range(4, 10);
    }
}
