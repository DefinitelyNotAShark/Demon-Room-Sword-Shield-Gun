using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class DyingState : StateMachineBehaviour
{
    private ParticleSystem deathParticles;
    private bool coroutineStarted;

    private float timeElapsed;
    private float deathTime = 3;

    private NavMeshAgent enemyAgent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        enemyAgent = animator.GetComponent<NavMeshAgent>();
        enemyAgent.isStopped = true;//don't let a dead man walk...

        //AUDIO play enemy death sound
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (timeElapsed >= deathTime)//if everything has played and object is ok to be destroyed now
        {
            Destroy(animator.gameObject);//DESTROOYYYYY
            //GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnEnemy>().EnemiesOnScreen--;//Tell the spawner that there's one more demon in hell
            GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemyWaveManager>().TotalEnemies--;
        }

        timeElapsed += Time.deltaTime;//add time to the timer
    }

    private ParticleSystem FindDeathParticles(Animator animator)
    {
        return animator.GetComponent<BasicEnemy>().DeathParticles;
    }
}
