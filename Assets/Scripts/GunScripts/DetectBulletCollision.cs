using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this goes on the bullet and handles any collision with it

public class DetectBulletCollision : MonoBehaviour
{
    private IFightable fightableObject;

    private void OnTriggerEnter(Collider other)
    {
        try
        {
            fightableObject = other.GetComponent<IFightable>();//try to see if the object is fightable            
            fightableObject.GunHit(GetContactPoint(other));//do whatever it's supposed to if it's hit

            Destroy(this.gameObject);
        }
        catch (NullReferenceException)//if there's no fightable object
        {
            //AUDIO play a bullet hittin stone sound
            //here we could play some particles for the bullet hitting a solid object like some sparks or something
        }
    }
    /// <summary>
    /// Returns the Vector3 of where the sword hit on the enemy
    /// </summary>
    private Vector3 GetContactPoint(Collider collision)
    {
        return collision.ClosestPointOnBounds(transform.position);
    }
}

