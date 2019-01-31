using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IFightable
{
    public bool EnemyIsDead;
    public int EnemyLives;

    public void SwordHit()
    {
        EnemyLives -= 2;
    }

    public void GunHit()
    {
        EnemyLives--;
    }
}

