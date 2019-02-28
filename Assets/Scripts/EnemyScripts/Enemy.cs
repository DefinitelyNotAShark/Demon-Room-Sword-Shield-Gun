using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IFightable
{
    [HideInInspector]
    public int EnemyLives;

    [HideInInspector]
    public ParticleSystem HitParticles;

    public bool EnemyIsDead()
    {
        if (EnemyLives <= 0)
            return true;

        else return false;
    }

    public void SwordBodyHit()
    {
        Debug.Log("You hit the body with the sword");
        EnemyLives -= 2;
        HitParticles.Play();
    }

    public void SwordHeadHit()
    {
        Debug.Log("You hit the head with the sword");
        EnemyLives -= 4;//does double damage
        HitParticles.Play();
    }

    public void SwordLegsHit()
    {
        Debug.Log("You hit the legs with the sword");
        EnemyLives = -2;
        HitParticles.Play();
    }

    public void GunLegsHit()
    {
        Debug.Log("You hit the legs with the gun");
        EnemyLives--;
        HitParticles.Play();
    }

    public void GunHeadHit()//does double damage
    {
        Debug.Log("You hit the head with the gun");
        EnemyLives -= 2;
        HitParticles.Play();
    }

    public void GunBodyHit()
    {
        Debug.Log("You hit the body with the gun");
        EnemyLives--;
        HitParticles.Play();
    }
}

