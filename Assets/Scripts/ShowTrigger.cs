using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTrigger : MonoBehaviour
{
    public void HideTriggerHint()
    {
        this.gameObject.SetActive(false);
    }
}
