using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IFightable
{
    [HideInInspector]
    public int EnemyLives;

    [HideInInspector]
    public ParticleSystem HitParticles, DeathParticles;

    private GameObject particleInstance;
    public bool isHurt;//detects if the enemy was hurt so the animator can be set in the enemy class

    public bool EnemyIsDead()
    {
        if (EnemyLives <= 0)
            return true;

        else return false;
    }

    public void SwordHit(Vector3 collisionPoint, int swordDamage)
    {
        if (!EnemyIsDead())
        {
            EnemyLives -= swordDamage;
            particleInstance = Instantiate(HitParticles, collisionPoint, HitParticles.transform.rotation).gameObject;//put the hit particles at the place of where they were hit
            DestroyParticlesAfterPlaying(particleInstance);
            isHurt = true;
        }
    }

    public void GunHit(Vector3 collisionPoint, int bulletDamage)
    {
        if (!EnemyIsDead())
        {
            EnemyLives-= bulletDamage;
            particleInstance = Instantiate(HitParticles, collisionPoint, HitParticles.transform.rotation).gameObject;//put the hit particles at the place of contact
            DestroyParticlesAfterPlaying(particleInstance);
            isHurt = true;
        }
    }

    void DestroyParticlesAfterPlaying(GameObject particles)
    {
        Destroy(particles, 2);
    }
}

