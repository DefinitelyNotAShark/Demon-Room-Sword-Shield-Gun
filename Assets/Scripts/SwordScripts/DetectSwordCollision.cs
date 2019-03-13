using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this goes on the sword and handles any collision with it

public class DetectSwordCollision : MonoBehaviour
{
    private IFightable fightableObject;
    private AudioSource audioSource;
    private SwordSwishManager swordSwishScript;

    private void Start()
    {
        swordSwishScript = GetComponentInParent<SwordSwishManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)//called when the sword hits something
    {
        try
        {
            fightableObject = other.GetComponent<IFightable>();//try to see if the object is fightable

            if (swordSwishScript.SwordIsFastEnough)//check to see if the sword is going fast enough to do damage
            {
                fightableObject.SwordHit(GetContactPoint(other));//do whatever it's supposed to if it's hit

                if (audioSource != null)//if we have the enemy hit audio
                    audioSource.Play();//play the sound of the sword stabbing an enemy
            }
        }
        catch (NullReferenceException)//if there's no fightable object
        {
            //AUDIO play a sword hitting stone sound
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
