using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this goes on the sword and handles any collision with it

public class DetectSwordCollision : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClip;

    private IFightable fightableObject;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)//called when the sword hits something
    {
        if(audioSource != null && audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        try
        {
            fightableObject = other.GetComponent<IFightable>();//try to see if the object is fightable
            fightableObject.SwordHit(GetContactPoint(other));//do whatever it's supposed to if it's hit

        }
        catch(NullReferenceException)//if there's no fightable object
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
