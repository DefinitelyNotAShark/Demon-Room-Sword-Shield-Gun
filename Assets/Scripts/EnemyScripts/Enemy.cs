using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IFightable
{
    public int EnemyLives;

    public bool EnemyIsDead()
    {
        if (EnemyLives <= 0)
            return true;

        else return false;
    }

    public void SwordHit()
    {
        EnemyLives -= 2;
    }

    public void GunHit()
    {
        EnemyLives--;
    }
}

