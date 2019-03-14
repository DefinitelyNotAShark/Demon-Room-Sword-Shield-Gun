using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DyingState : StateMachineBehaviour
{
    private ParticleSystem deathParticles;
    private bool coroutineStarted;

    private float timeElapsed;
    private float deathTime = 3;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        Debug.Log("ENTERED DYING STATE FOR " + animator.gameObject.name.ToString());

        deathParticles = FindDeathParticles(animator);//find our death particle system
        deathParticles.Play();//play our particles

        TurnOffRenderers(animator);//make invisible
        animator.GetComponent<BoxCollider>().enabled = false;//make uncollideable

        //AUDIO play enemy death sound
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (timeElapsed >= deathTime)//if everything has played and object is ok to be destroyed now
        {
            Destroy(animator.gameObject);
        }

        timeElapsed += Time.deltaTime;//add time to the timer
    }

    private void TurnOffRenderers(Animator animator)
    {
        SkinnedMeshRenderer[] renderers = animator.GetComponentsInChildren<SkinnedMeshRenderer>();//turn off graphic
        foreach (SkinnedMeshRenderer s in renderers)
        {
            s.enabled = false;
        }
    }

    private ParticleSystem FindDeathParticles(Animator animator)
    {
        //ParticleSystem[] particles = animator.gameObject.GetComponentsInChildren<ParticleSystem>();

        //foreach(ParticleSystem p in particles)
        //{
        //    if (p.name == "DeathParticles")
        //        return p;
        //}
        //return null;

        return animator.GetComponent<BasicEnemy>().DeathParticles;
    }
}
