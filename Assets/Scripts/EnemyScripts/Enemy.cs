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

    public void SwordHit()
    {
        EnemyLives -= 2;
        HitParticles.Play();
    }

    public void GunHit()
    {
        EnemyLives--;
        HitParticles.Play();
    }
}

