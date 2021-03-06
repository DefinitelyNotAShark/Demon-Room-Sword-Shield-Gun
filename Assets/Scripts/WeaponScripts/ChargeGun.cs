﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeGun : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The time it takes for the gun to recharge.")]
    private float maxChargeTime;

    [HideInInspector]
    public bool gunIsCharged = false;

    //[SerializeField]
    //private AudioClip gunIsChargedAlert;

    //[SerializeField]
    //private float gunIsChargedAlertVolume;

    private AudioSource audioSource;

    private Slider slider;
    private float currentChargeTime;
    private bool alertHasBeenPlayed;

	void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        slider = GetComponentInChildren<Slider>();
        currentChargeTime = maxChargeTime;
	}
	
	void Update ()
    {
        //CHANGE THE CHARGE
        if (currentChargeTime < maxChargeTime)//if it can be charged more, keep charging it
        {
            gunIsCharged = false;
            currentChargeTime += Time.deltaTime;
        }

        else gunIsCharged = true;//otherwise, it is charged

        //DISPLAY THE CHARGE
        slider.value = currentChargeTime / maxChargeTime;
        UpdateSliderColor();//change the color based on if charged
	}

    /// <summary>
    /// Call this every time a charge on the gun is used
    /// </summary>
    public void ResetGunCharge()
    {
        currentChargeTime = 0;
    }

    private void UpdateSliderColor()
    {
        if (gunIsCharged)
            slider.GetComponentInChildren<Image>().color = Color.white;
        else
            slider.GetComponentInChildren<Image>().color = Color.gray;
    }
}
