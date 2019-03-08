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

    private bool lastFrameTrailEnabled;

    void Start()
    {
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        audio = GetComponent<AudioSource>();

        trailRenderer.emitting = true;
        timeElapsed = 0;
        timeRequired = .4f;
    }

    void Update()
    {
        float tempSpeed = Speed();//set the variable to the speed. Only get the speed once so that the previous position isn't reset multiple times per frame.

        if (tempSpeed >= swooshVelocity)//if the sword is swinging fast enough to warrant a trail being made
        {
            trailRenderer.emitting = true;//make the trail

        }
        else trailRenderer.emitting = false;

        if (TrailHasStarted())//if the countdown has reset then we can play the sound. This is to prevent the sound being started every frame
            audio.Play();//play the sword swish sound.

        timeElapsed += Time.deltaTime;
        trailRenderer.startColor = speedGradient.Evaluate(1 / (maximumSwoosh / tempSpeed));
    }

    private bool TrailHasStarted()
    {
        if (lastFrameTrailEnabled == false && trailRenderer.emitting == true)//if it wasn't emitting before and now it is, return true
            return true;

        lastFrameTrailEnabled = trailRenderer.emitting;//reset the state of the trailRenderer
        return false;//otherwise it hasn't started yet.
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
