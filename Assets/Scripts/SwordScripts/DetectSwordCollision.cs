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

    private void OnTriggerEnter(Collider other)
    {
        if(audioSource != null && audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        try
        {
            fightableObject = other.GetComponent<IFightable>();//try to see if the object is fightable
            fightableObject.SwordHit();//do whatever it's supposed to if it's hit

            Debug.Log("The sword hit an enemy");
        }
        catch(NullReferenceException)//if there's no fightable object
        {
            Debug.Log("The sword hit something other than an enemy");
        }
    }
}
