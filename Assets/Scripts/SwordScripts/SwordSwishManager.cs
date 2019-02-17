using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwishManager : MonoBehaviour
{
    [SerializeField]
    private float swooshVelocity = 5;

    private TrailRenderer trailRenderer;
    private Rigidbody rigidbody;

	// Use this for initialization
	void Start ()
    {
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (rigidbody.velocity.magnitude >= swooshVelocity)
        {
            // Swoosh audio here
            trailRenderer.emitting = true;
        }
        else
            trailRenderer.emitting = false;
	}
}
