using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordSwishManager : MonoBehaviour
{
    [SerializeField]
    private float swooshVelocity = 5;
    [SerializeField]
    private float maximumSwoosh = 5;
    [SerializeField]
    private Gradient speedGradient;

    private TrailRenderer trailRenderer;

    private Vector3 lastPosition;

	void Start ()
    {
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        trailRenderer.emitting = true;
	}
	
	void Update ()
    {
        float tempSpeed = Speed();

        if (tempSpeed >= swooshVelocity)
        {
            trailRenderer.emitting = true;
        }

        else trailRenderer.emitting = false;

        trailRenderer.startColor = speedGradient.Evaluate(1/(maximumSwoosh / tempSpeed));
    }

        private float Speed()
        {
            float speed = (transform.position - lastPosition).magnitude / Time.deltaTime;
            lastPosition = transform.position;

            return speed;
        }
    }
