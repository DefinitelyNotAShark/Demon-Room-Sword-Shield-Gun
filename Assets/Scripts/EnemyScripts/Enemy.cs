using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IFightable
{
    [HideInInspector]
    public int EnemyLives;

    [HideInInspector]
    public ParticleSystem HitParticles, DeathParticles;

    public bool isHurt;//detects if the enemy was hurt so the animator can be set in the enemy class

    public bool EnemyIsDead()
    {
        if (EnemyLives <= 0)
            return true;

        else return false;
    }

    public void SwordHit()
    {
        if (!EnemyIsDead())
        {
            EnemyLives -= 2;
            HitParticles.Play();
            isHurt = true;
        }
    }

    public void GunHit()
    {
        if (!EnemyIsDead())
        {
            EnemyLives--;
            HitParticles.Play();
            isHurt = true;
        }
    }
}

