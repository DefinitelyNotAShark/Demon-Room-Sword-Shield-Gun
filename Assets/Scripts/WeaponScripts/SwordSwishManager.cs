using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordSwishManager : MonoBehaviour
{
    [HideInInspector]
    public bool SwordIsFastEnough { get; private set; }//If this evaluates true, then the sword can do damage. Otherwise, it shouldn't do much
 
    [SerializeField]
    private float swooshVelocity = 5;
    [SerializeField]
    private float maximumSwoosh = 5;
    [SerializeField]
    private Gradient speedGradient;

    [SerializeField]
    private AudioClip swordSwish;

    [SerializeField]
    private float swordSwishVolume;

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
        timeRequired = .4f;
    }

    void Update()
    {
        float tempSpeed = Speed();//set the variable to the speed. Only get the speed once so that the previous position isn't reset multiple times per frame.

        if (tempSpeed >= swooshVelocity)//if the sword is swinging fast enough to warrant a trail being made
        {
            SwordIsFastEnough = true;//tell the sword it's going fast enough to do damage
            trailRenderer.emitting = true;//make the trail

            if (tempSpeed > (swooshVelocity + 2) && CanSwish())//If the sword is swinging fast enough to make a swish sound
                audio.PlayOneShot(swordSwish, swordSwishVolume);
        }
        else
        {
            trailRenderer.emitting = false;//turns the trail off
            SwordIsFastEnough = false;//tells the sword it's not going fast enough to do damage
        }

        timeElapsed += Time.deltaTime;
        trailRenderer.startColor = speedGradient.Evaluate(1 / (maximumSwoosh / tempSpeed));//change color to match how fast it's swinging
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
