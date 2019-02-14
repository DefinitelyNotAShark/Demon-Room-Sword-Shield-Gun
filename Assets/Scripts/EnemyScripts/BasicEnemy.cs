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

    private ParticleSystem particles;
    private MeshRenderer mesh;
    private BoxCollider collider;


    private bool coroutineStarted;

    private void Start()
    {
        //Vars
        EnemyLives = enemyLives;//set our base lives to our customized lives

        //Components
        particles = GetComponentInChildren<ParticleSystem>();
        mesh = GetComponent<MeshRenderer>();
        collider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (EnemyIsDead() && !coroutineStarted)//detects if enemy has been killed every frame
            StartCoroutine(DestroyEnemyCooldown());
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
