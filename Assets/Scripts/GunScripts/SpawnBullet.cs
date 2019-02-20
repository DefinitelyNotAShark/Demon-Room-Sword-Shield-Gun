using System.Collections;
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

    [SerializeField]
    private float bulletLifeTime;

    private float shootButtonValue;
    private bool coroutineStarted;
    private ParticleSystem muzzleFlash;

    private Vector3 direction;

    private void Start()
    {
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
    }


    void Update ()
    {
        shootButtonValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
	}

    private void FixedUpdate()
    {
        if(shootButtonValue > .5f && !coroutineStarted)
        {
            StartCoroutine(Shoot());
            coroutineStarted = true;
        }
    }

    private IEnumerator Shoot()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, transform.rotation);//instantiates a bullet at the spawn position and at it's set prefab rotation
        bulletInstance.AddComponent<MoveBullet>().bulletSpeed = bulletSpeed;//give us a move script and set the speed to our bulletSpeed
        bulletInstance.GetComponent<MoveBullet>().bulletLifeTime = bulletLifeTime;
        //AUDIO play a shooting sound here
        muzzleFlash.Play();
        yield return new WaitForSeconds(shootCoolDownTime);
        coroutineStarted = false;
    }
}
