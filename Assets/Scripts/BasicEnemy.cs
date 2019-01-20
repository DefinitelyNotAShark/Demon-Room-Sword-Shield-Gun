using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour, IFightable
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float enemyCoolDownLength;

    private GameObject player;
    private ParticleSystem particles;
    private bool enemyIsDead;
    private MeshRenderer mesh;
    private BoxCollider collider;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");//get the player
        particles = GetComponentInChildren<ParticleSystem>();
        mesh = GetComponent<MeshRenderer>();
        collider = GetComponent<BoxCollider>();
    }

    public void SwordHit()
    {
        if(!enemyIsDead)
        StartCoroutine(DestroyEnemyCooldown());//death cooldown
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed *Time.deltaTime);//move in the direction of the player at specified speed
    }

    IEnumerator DestroyEnemyCooldown()
    {   
        //AUDIO play enemy hit sound
        enemyIsDead = true;//so that we don't start the coroutine more than once
        particles.Play();//give us some death particles!
        mesh.enabled = false;//invisible
        collider.enabled = false;//can't hit again
        yield return new WaitForSeconds(enemyCoolDownLength);
        Destroy(this.gameObject);//DIE
    }
}
