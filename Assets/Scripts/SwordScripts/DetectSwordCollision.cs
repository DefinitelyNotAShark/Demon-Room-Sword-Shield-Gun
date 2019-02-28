using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this goes on the sword and handles any collision with it

public class DetectSwordCollision : MonoBehaviour
{
    private IFightable fightableObject;

    private void OnTriggerEnter(Collider other)
    {
        try
        {
            fightableObject = other.GetComponentInParent<IFightable>();//try to see if the object is fightable

            if (fightableObject != null)
            {
                switch (other.gameObject.name)//do damage based on what object the collider is attatched to. Head shots do double damage.
                {
                    case "LegsDetectObject": fightableObject.SwordLegsHit(); break;//if the collider we hit is connected to a game object with that name, we do the body damage w/sword
                    case "BodyDetectObject": fightableObject.SwordBodyHit(); break;
                    case "HeadDetectObject": fightableObject.SwordHeadHit(); break;
                }
                Debug.Log("The sword hit an enemy");
            }
        }
        catch(NullReferenceException)//if there's no fightable object
        {
            Debug.Log("The sword hit something other than an enemy");
        }
    }
}
