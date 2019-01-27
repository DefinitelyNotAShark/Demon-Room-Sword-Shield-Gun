﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullet : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    [Tooltip("The time until another shot is allowed after the previous shot.")]
    private float shootCoolDownTime;

    [SerializeField]
    private float bulletSpeed;

    private float shootButtonValue;
    private bool coroutineStarted;


	void Update ()
    {
        shootButtonValue = Input.GetAxis("Shoot");
	}

    private void FixedUpdate()
    {
        if(shootButtonValue > 0 && !coroutineStarted)
        {
            StartCoroutine(Shoot());
            coroutineStarted = true;
        }
    }

    private IEnumerator Shoot()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, this.gameObject.transform.position, Quaternion.identity);//instantiates a bullet at the spawn position and at it's set prefab rotation
        bulletInstance.AddComponent<MoveBullet>().bulletSpeed = bulletSpeed;//give us a move script and set the speed to our bulletSpeed
        //AUDIO play a shooting sound here
        //PARTICLES play a muzzle flash particle here
        yield return new WaitForSeconds(shootCoolDownTime);
        coroutineStarted = false;
    }
}
