using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDetectColl : MonoBehaviour
{
    private GameObject panel;
    private TintScreen tint;

    private void Start()
    {
        panel = GameObject.FindGameObjectWithTag("Panel");
        tint = panel.GetComponent<TintScreen>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy hit the player!");
            StartCoroutine(tint.FadeImage());
        }
    }
}
