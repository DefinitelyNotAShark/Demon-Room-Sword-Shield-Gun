using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : Enemy
{
    public float minRange { get; private set; }

    [SerializeField]
    private float speed;

    [SerializeField]
    private float enemyCoolDownLength;

    [SerializeField]
    private int enemyLives;

    private Transform playerTransform;
    private ParticleSystem particles;
    private MeshRenderer mesh;
    private BoxCollider collider;
    private Animator enemyAnim;

    //Nav Stuff
    private NavMeshAgent agent;//this is the component that tells the enemy to walk on the NavMesh
    private Vector3 targetVector;//this is the vector of the destination


    private bool coroutineStarted;
    public bool attackCoroutineStarted;

    private void Start()
    {
        //Vars
        EnemyLives = enemyLives;//set our base lives to our customized lives

        //Components
        particles = GetComponentInChildren<ParticleSystem>();
        mesh = GetComponent<MeshRenderer>();
        collider = GetComponent<BoxCollider>();
        enemyAnim = GetComponent<Animator>();

        //AI
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();//get the player transform
        agent = GetComponent<NavMeshAgent>();
        targetVector = playerTransform.position;
        agent.SetDestination(targetVector);

    }

    private void Update()
    {
        //StopEnemyWhenTooClose();

        if (EnemyIsDead() && !coroutineStarted)//detects if enemy has been killed every frame
            StartCoroutine(DestroyEnemyCooldown());
    }

    /// <summary>
    /// checks the distance between the enemy and the player, and acts according to distance
    /// </summary>
    //private void StopEnemyWhenTooClose()//if we've arrived at where we're going, we don't need to keep moving.
    //{
    ////    float distance = Vector3.Distance(this.transform.position, playerTransform.position);//the distance between us and the enemy

    ////    if (distance < minRange && !agent.isStopped)//if we're too close and we're still moving
    ////    {
    ////        //Stop
    ////        agent.isStopped = true;//we stop
    ////        enemyAnim.SetBool("IsCloseToPlayer", true);//tell the animator that we close

    ////    }
    //    else if (distance < minRange && agent.isStopped)//if we're close and we've stopped
    //    {
    //        //Attack
    //        if (!attackCoroutineStarted)//GET EM!
    //            StartCoroutine(EnemyAttackCooldown());
    //    }

    //    else if (distance > minRange && agent.isStopped)//if the player or the enemy moves out of the space
    //    {
    //        //Move Again
    //        agent.isStopped = false;
    //        targetVector = playerTransform.position;//reset the target
    //        enemyAnim.SetBool("IsCloseToPlayer", false);//tell the animator that we're not close enough for clobberin' anymore
    //    }
    //}

    public IEnumerator EnemyAttackCooldown()
    {
        //AUDIO play attack sound
        attackCoroutineStarted = true;
        enemyAnim.SetTrigger("Attack");
        yield return new WaitForSeconds(2);
        attackCoroutineStarted = false;
    }

    IEnumerator DestroyEnemyCooldown()
    {
        //AUDIO play enemy hit sound
        coroutineStarted = true;
        particles.Play();//give us some death particles!
        mesh.enabled = false;//invisible
        collider.enabled = false;//can't hit again
        yield return new WaitForSeconds(enemyCoolDownLength);
        Destroy(this.gameObject);//DIE
    }
}
