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

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        enemyAgent = animator.gameObject.GetComponent<NavMeshAgent>();
        enemyScript = animator.gameObject.GetComponent<BasicEnemy>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyTransform = animator.gameObject.transform;

        enemyAgent.isStopped = false;//when we enter the move state, we can move again

        enemyAgent.SetDestination(playerTransform.position);
    }

    public override void OnStateMove(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyAgent.SetDestination(playerTransform.position);

        float distance = Vector3.Distance(playerTransform.position, enemyTransform.position);

        if (distance <= enemyScript.minRange)
        {
            enemyAgent.isStopped = true;//stop it, you
            animator.SetBool("IsCloseToPlayer", true);//this is how the animator knows to go back to idle
        }
    }
}
