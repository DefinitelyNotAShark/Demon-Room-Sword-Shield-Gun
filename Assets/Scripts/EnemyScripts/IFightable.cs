﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFightable
{
    void SwordHit(Vector3 collisionTransform, int swordDamage);//this is for when the game's supposed to be aware of where the sword hit
    void GunHit(Vector3 collisionTransform, int bulletDamage);//does the thing it's supposed to do when hit by bullet
}
