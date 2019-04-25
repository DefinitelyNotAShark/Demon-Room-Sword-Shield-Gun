using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeGun : MonoBehaviour
{
    [Header("Gun Charging Values")]
    [SerializeField]
    private Slider gunChargingUI;

    [SerializeField]
    [Tooltip("The time it takes for the gun to recharge.")]
    private float maxChargeTime;

    [HideInInspector]
    public bool gunIsCharged = false;

    private float currentChargeTime;

	void Start ()
    {
        currentChargeTime = maxChargeTime;
	}
	
	void Update ()
    {
        //CHANGE THE CHARGE
        if (currentChargeTime < maxChargeTime)//if it can be charged more, keep charging it
            currentChargeTime += Time.deltaTime;

        else gunIsCharged = true;//otherwise, it is charged

        //DISPLAY THE CHARGE
        gunChargingUI.value = currentChargeTime / maxChargeTime;
	}

    /// <summary>
    /// Call this every time a charge on the gun is used
    /// </summary>
    public void ResetGunCharge()
    {
        currentChargeTime = 0;
    }
}
