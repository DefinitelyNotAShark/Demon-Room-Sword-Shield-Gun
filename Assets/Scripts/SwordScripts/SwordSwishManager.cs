using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordSwishManager : MonoBehaviour
{
    [SerializeField]
    private float swooshVelocity = 5;

    [SerializeField]
    private Text debugText;//DEBUG DELETE LATER

    private TrailRenderer trailRenderer;

    private Vector3 lastPosition;

	// Use this for initialization
	void Start ()
    {
        trailRenderer = GetComponentInChildren<TrailRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //debugText.text = "Swoosh Speed: " + rigidbody.velocity.magnitude.ToString() + "\nThe rigidbody is on " + rigidbody.gameObject.name.ToString();

        //if (rigidbody.velocity.magnitude >= swooshVelocity)
        //{
        //    // Swoosh audio here
        //    trailRenderer.emitting = true;
        //}
        //else
        //    trailRenderer.emitting = false;

        if (Speed() >= swooshVelocity)
            trailRenderer.emitting = true;

        else trailRenderer.emitting = false;

    }

        private float Speed()
        {
            float speed = (transform.position - lastPosition).magnitude / Time.deltaTime;
            lastPosition = transform.position;

            debugText.text = "speed = " + speed;
            return speed;
        }
    }
