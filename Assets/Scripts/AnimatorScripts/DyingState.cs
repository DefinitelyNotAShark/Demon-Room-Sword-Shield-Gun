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

        //cycle through all the renderers and turn them all off so the enemy is invisible
        TurnOffRenderers(animator);
        //cycle through all the colliders and turn them all off so the enemy can't be bumped into
        TurnOffColliders(animator);

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

    private void TurnOffColliders(Animator animator)
    {
        BoxCollider[] colliders = animator.GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider c in colliders)
        {
            c.enabled = false;
        }
    }


private ParticleSystem FindDeathParticles(Animator animator)
    {
        ParticleSystem[] particles = animator.gameObject.GetComponentsInChildren<ParticleSystem>();

        foreach(ParticleSystem p in particles)
        {
            if (p.name.Contains("DeathParticles"))
                return p;
        }
        return null;
    }
}
