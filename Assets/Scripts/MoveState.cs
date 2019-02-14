using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveState : StateMachineBehaviour
{
    //Components
    private GameObject playerObject;
    private GameObject enemyObject;

    private Transform playerTransform;
    private Transform enemyTransform;

    private NavMeshAgent agent;//this is the component that tells the enemy to walk on the NavMesh
    private Animator enemyAnim;

    private float timeElapsed;
    private float timeBetweenAttacks;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {       
        //set components OBJECTS
        playerObject = GameObject.FindGameObjectWithTag("Player");//get the player transform
        enemyObject = animator.gameObject;//get the transform of the animated enemy

        //set components ENEMY COMPONENTS
        enemyTransform = enemyObject.GetComponent<Transform>();
        enemyAnim = enemyObject.GetComponent<Animator>();
        agent = enemyObject.GetComponent<NavMeshAgent>();
        agent.stoppingDistance = enemyObject.GetComponent<BasicEnemy>().minRange;//set the range for the enemy to stop

        timeBetweenAttacks = 4.0f;

        //set components PLAYER COMPONENTS
        playerTransform = playerObject.GetComponent<Transform>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        float distance = Vector3.Distance(enemyTransform.position, playerTransform.position);//the distance between us and the enemy;

        if (distance <= agent.stoppingDistance)//if the enemy is stopped because it's close
        {
            enemyAnim.SetBool("IsCloseToPlayer", true);//tell the animator that we close

            if (timeElapsed >= timeBetweenAttacks)//if our timer goes off
            {
                enemyAnim.SetTrigger("Attack");//the enemy attacks
                timeElapsed = 0;//reset timer
            }

            timeElapsed += Time.deltaTime;//add time to the timer
        }        
    }
}
