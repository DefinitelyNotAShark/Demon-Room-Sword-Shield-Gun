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

	void Start ()
    {
        trailRenderer = GetComponentInChildren<TrailRenderer>();
	}
	
	void Update ()
    {
        if (Speed() >= swooshVelocity)
            trailRenderer.emitting = true;

        else trailRenderer.emitting = false;

    }

        private float Speed()
        {
            float speed = (transform.position - lastPosition).magnitude / Time.deltaTime;
            lastPosition = transform.position;

            return speed;
        }
    }
