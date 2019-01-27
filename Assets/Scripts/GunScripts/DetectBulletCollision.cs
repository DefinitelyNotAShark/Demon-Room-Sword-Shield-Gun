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
            fightableObject.GunHit();//do whatever it's supposed to if it's hit

            Debug.Log("The bullet hit an enemy");
        }
        catch (NullReferenceException)//if there's no fightable object
        {
            Debug.Log("The bullet hit something other than an enemy");
            //here we could play some particles for the bullet hitting a solid object like some sparks or something
        }
    }
}
