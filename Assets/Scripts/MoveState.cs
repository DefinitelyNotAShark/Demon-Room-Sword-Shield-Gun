using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveState : StateMachineBehaviour
{
    private BasicEnemy enemyScript;
    private NavMeshAgent enemyAgent;

    private Transform playerTransform;
    private Transform enemyTransform;

    private Vector3 targetVector;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        enemyAgent = animator.gameObject.GetComponent<NavMeshAgent>();
        enemyScript = animator.gameObject.GetComponent<BasicEnemy>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyTransform = animator.gameObject.transform;


    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        //reset destination every frame
        targetVector = playerTransform.position;//set the target to the player
        enemyAgent.SetDestination(targetVector);//we go to the player

        float distance = Vector3.Distance(playerTransform.position, enemyTransform.position);

        if (distance <= enemyScript.minRange)
        {
            enemyAgent.isStopped = true;//stop it, you
            animator.SetBool("IsCloseToPlayer", true);//this is how the animator knows to go back to idle
        }
    }
}
