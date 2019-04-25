using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this goes on the bullet and handles any collision with it

public class DetectBulletCollision : MonoBehaviour
{
    private IFightable fightableObject;
    private AudioSource audio;

    public int bulletDamage;//set by the spawner depending on what kind of shot was done

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        try
        {
            //PRE COLLISION CHECKS
            fightableObject = other.GetComponent<IFightable>();//try to see if the object is fightable  

            if (isDying(other))//ignore weapon collision if the enemy is dying
                return;

            //HANDLE COLLISION
            fightableObject.GunHit(GetContactPoint(other), bulletDamage);//do whatever it's supposed to if it's hit
            
            if (audio != null)//play the bullet hitting enemy sound
                audio.Play();

            StartCoroutine(DestroyBullet());
        }
        catch (NullReferenceException)//if there's no fightable object
        {
            //AUDIO play a bullet hittin stone sound
            //here we could play some particles for the bullet hitting a solid object like some sparks or something
        }
    }

    //this is so we can play the bullet hit enemy sound effect
    private IEnumerator DestroyBullet()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

    //checks the enemy script to see whether it is dying
    private bool isDying(Collider other)
    {
        if (other.GetComponent<BasicEnemy>().EnemyIsDead())//if the enemy script says it is dying return true
            return true;

        else return false;
    }

    /// <summary>
    /// Returns the Vector3 of where the sword hit on the enemy
    /// </summary>
    private Vector3 GetContactPoint(Collider collision)
    {
        return collision.ClosestPointOnBounds(transform.position);
    }
}

