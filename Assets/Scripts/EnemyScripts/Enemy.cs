using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IFightable
{
    [HideInInspector]
    public int EnemyLives;

    public bool EnemyIsDead()
    {
        if (EnemyLives <= 0)
            return true;

        else return false;
    }

    public virtual void SwordHit()
    {
        EnemyLives -= 2;
    }

    public virtual void GunHit()
    {
        EnemyLives--;
    }
}

