using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : Enemy
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float enemyCoolDownLength;

    [SerializeField]
    [Tooltip("The minimum distance an enemy can be to the player as a float.")]
    private float minRange;

    [SerializeField]
    private int enemyLives;

    [SerializeField]
    private ParticleSystem deathParticles;

    [SerializeField]
    private ParticleSystem hitParticles;

    private Transform playerTransform;
    private MeshRenderer mesh;
    private BoxCollider collider;

    //Nav Stuff
    private NavMeshAgent agent;//this is the component that tells the enemy to walk on the NavMesh
    private Vector3 targetVector;//this is the vector of the destination


    private bool coroutineStarted;

    private void Start()
    {
        //Vars
        EnemyLives = enemyLives;//set our base lives to our customized lives

        //Components
        mesh = GetComponent<MeshRenderer>();
        collider = GetComponent<BoxCollider>();

        //AI
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();//get the player transform
        agent = GetComponent<NavMeshAgent>();
        targetVector = playerTransform.position;
        agent.SetDestination(targetVector);
    }

    private void Update()
    {
        StopEnemyWhenTooClose();

        if (EnemyIsDead() && !coroutineStarted)//detects if enemy has been killed every frame
            StartCoroutine(DestroyEnemyCooldown());
    }

    /// <summary>
    /// checks the distance between the enemy and the player, and stops the enemy from moving into the player's space
    /// </summary>
    private void StopEnemyWhenTooClose()//if we've arrived at where we're going, we don't need to keep moving.
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);//the distance between us and the enemy

        if (distance < minRange && !agent.isStopped)//if we're too close and we're still moving
            agent.isStopped = true;//we stop

        else  if(distance > minRange && agent.isStopped)//if the player or the enemy moves out of the space
        {
            agent.isStopped = false;//resume enemy chase
        }
    }

    IEnumerator DestroyEnemyCooldown()
    {
        //AUDIO play enemy hit sound
        coroutineStarted = true;
        deathParticles.Play();//give us some death particles!
        mesh.enabled = false;//invisible
        collider.enabled = false;//can't hit again
        yield return new WaitForSeconds(enemyCoolDownLength);
        Destroy(this.gameObject);//DIE
    }

    public override void SwordHit()
    {
        hitParticles.Play();
        base.SwordHit();
    }
}
