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

    public void SwordHit(Vector3 collisionPoint)
    {
        if (!EnemyIsDead())
        {
            EnemyLives -= 2;
            Instantiate(HitParticles, collisionPoint, HitParticles.transform.rotation);//put the hit particles at the place of where they were hit
            isHurt = true;
        }
    }

    public void GunHit(Vector3 collisionPoint)
    {
        if (!EnemyIsDead())
        {
            EnemyLives--;
            Instantiate(HitParticles, collisionPoint, HitParticles.transform.rotation);//put the hit particles at the place of contact
            isHurt = true;
        }
    }
}

