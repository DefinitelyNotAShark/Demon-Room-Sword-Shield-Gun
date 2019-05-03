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

    [SerializeField]
    private Light muzzleFlashLight;

    [SerializeField]
    private float lightThreshold = .5f;

    [SerializeField]
    private AudioClip shootSound, weakShootSound;

    [SerializeField]
    private float shootSoundVolume, weakShootSoundVolume;

    [SerializeField]
    [Tooltip("The damage as an int that the bullet does based on whether it's charged or not")]
    private int chargedBulletDamage, nonChargedBulletDamage;

    [SerializeField]
    private ShowTrigger triggerUI;

    private float lastShoot;

    private ParticleSystem muzzleFlash;
    private Vector3 direction;
    private AudioSource audioSource;
    private bool gunHasBeenShotOnce = false;//this tells us whether you've used your gun and if you haven't we'll show you how

    private ChargeGun gunCharge;//ref to script that handles the charging of the gun

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        gunCharge = GetComponentInParent<ChargeGun>();

        muzzleFlashLight.enabled = false;
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))//if you press the shoot button
        {
            gunHasBeenShotOnce = true;

            if (gunCharge.gunIsCharged)//strong shoot
            {
                audioSource.PlayOneShot(shootSound, shootSoundVolume);
                OVRHaptics.RightChannel.Preempt(new OVRHapticsClip(shootSound));

                lastShoot = Time.time;
                muzzleFlashLight.enabled = true;
                muzzleFlash.Play();

                ShootBullet(chargedBulletDamage);        
            }
            else if(!gunCharge.gunIsCharged)//weak shoot
            {
                //AUDIO play weak shot sound
                audioSource.PlayOneShot(weakShootSound, weakShootSoundVolume);

                ShootBullet(nonChargedBulletDamage);
                OVRHaptics.RightChannel.Preempt(new OVRHapticsClip(weakShootSound));
            }

            gunCharge.ResetGunCharge();//Either way, reset the charge
        }

        if (Time.time - lastShoot > lightThreshold)
            muzzleFlashLight.enabled = false;

        if (gunHasBeenShotOnce)
            triggerUI.HideTriggerHint();
    }

    //spawn the bullet and set its params to the ones set in the serialize fields of the class
    void ShootBullet(int damage)
    {
        //INSTANTIATE BULLET
        GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, transform.rotation);//instantiates a bullet at the spawn position and at it's set prefab rotation
        bulletInstance.AddComponent<MoveBullet>().bulletSpeed = bulletSpeed;//give us a move script and set the speed to our bulletSpeed
        bulletInstance.GetComponent<MoveBullet>().bulletLifeTime = bulletLifeTime;
        bulletInstance.GetComponent<DetectBulletCollision>().bulletDamage = damage;
    }
}
