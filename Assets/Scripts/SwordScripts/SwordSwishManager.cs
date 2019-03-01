using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordSwishManager : MonoBehaviour
{
    [SerializeField]
    private float swooshVelocity = 5;

    private TrailRenderer trailRenderer;

    private Vector3 lastPosition;

    private AudioSource audio;

    [SerializeField]
    private AudioClip slashSound;

    [SerializeField]
    private float slashVolume;

    void Start ()
    {
        audio = GetComponent<AudioSource>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
	}
	
	void Update ()
    {
        if (Speed() >= swooshVelocity)
            
        {
            trailRenderer.emitting = true;
            audio.PlayOneShot(slashSound, slashVolume); // adding slash sound
            Debug.Log("Time to slash!");
        }

        else trailRenderer.emitting = false;
        
        

    }

        private float Speed()
        {
            float speed = (transform.position - lastPosition).magnitude / Time.deltaTime;
            lastPosition = transform.position;

            return speed;
        }
    }
