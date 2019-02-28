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
            
            if (fightableObject != null)
            {
                switch (other.gameObject.name)//do damage based on what object the collider is attatched to. Head shots do double damage.
                {
                    case "LegsDetectObject": fightableObject.GunLegsHit(); break;//if the collider we hit is connected to a game object with that name, we do the body damage w/sword
                    case "BodyDetectObject": fightableObject.GunBodyHit(); break;
                    case "HeadDetectObject": fightableObject.GunHeadHit(); break;
                }

                Debug.Log("The bullet hit " + other.gameObject.name.ToString());
                Destroy(this.gameObject);
            }
        }
        catch (NullReferenceException)//if there's no fightable object
        {
            Debug.Log("The bullet hit " + other.gameObject.name.ToString());
            //here we could play some particles for the bullet hitting a solid object like some sparks or something
        }
    }
}
