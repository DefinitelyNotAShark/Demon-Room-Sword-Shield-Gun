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
    private AudioSource audio;

    private float timeElapsed, timeRequired;

    void Start()
    {
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        audio = GetComponent<AudioSource>();

        trailRenderer.emitting = true;
        timeElapsed = 0;
        timeRequired = .5f;
    }

    void Update()
    {
        float tempSpeed = Speed();

        if (tempSpeed >= swooshVelocity)
        {
            trailRenderer.emitting = true;

            if (CanSwish())
                audio.Play();
        }

        else trailRenderer.emitting = false;
        timeElapsed += Time.deltaTime;

        trailRenderer.startColor = speedGradient.Evaluate(1 / (maximumSwoosh / tempSpeed));
    }

    private float Speed()
    {
        float speed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        lastPosition = transform.position;

        return speed;
    }

    /// <summary>
    /// Only returns true if the countdown is done for the audio's sake
    /// </summary>
    /// <returns></returns>
    private bool CanSwish()
    {
        if (timeElapsed > timeRequired)
        {
            timeElapsed = 0;
            return true;
        }
        else return false;
    }
}
