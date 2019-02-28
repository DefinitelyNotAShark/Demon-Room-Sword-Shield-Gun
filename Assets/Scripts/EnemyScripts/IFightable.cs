using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFightable
{
    void SwordBodyHit();//so that different things happen to the enemy depending on where it gets hit by the sword
    void SwordHeadHit();
    void SwordLegsHit();

    void GunBodyHit();//so that different things happen to the enemy depending on where it gets shot
    void GunHeadHit();
    void GunLegsHit();

}
