using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : StateMachineBehaviour
{     
    //Components
    private GameObject enemyObject;
    private BasicEnemy enemyScript;

    private Transform playerTransform;
    private Transform enemyTransform;

    private Animator enemyAnim;

    private float timeElapsed;
    private float timeBetweenAttacks;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        enemyObject = animator.gameObject;//get the transform of the animated enemy

        enemyTransform = enemyObject.GetComponent<Transform>();
        enemyScript = enemyObject.GetComponent<BasicEnemy>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //set components ENEMY COMPONENTS
        enemyAnim = enemyObject.GetComponent<Animator>();

        timeBetweenAttacks = 1.0f;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (timeElapsed >= timeBetweenAttacks)//if our timer goes off
        {
            enemyTransform.LookAt(playerTransform);//look at the player before attack
            
            enemyAnim.SetTrigger("Attack");//the enemy attacks
            timeElapsed = 0;//reset timer
        }

        timeElapsed += Time.deltaTime;//add time to the timer

        if (ShouldMoveAgain())
            animator.SetBool("IsCloseToPlayer", false);//this makes us move again
    }

    /// <summary>
    /// Checks to see if the distance is bigger than the enemys attack range. Returns true if so.
    /// </summary>
    private bool ShouldMoveAgain()//checks to see if the enemy gets too far away from the player to attack
    {
        float distance = Vector3.Distance(enemyTransform.position, playerTransform.position);

        if (distance > enemyScript.minRange)
        {
            return true;
        }
        else return false;
    }
}
