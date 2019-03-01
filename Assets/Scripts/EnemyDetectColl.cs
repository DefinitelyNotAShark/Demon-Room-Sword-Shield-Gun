using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDetectColl : MonoBehaviour
{
    private GameObject panel;
    private TintScreen tint;

    [SerializeField]
    private AudioClip enemyMeleeSound1;

    [SerializeField]
    private float enemyMeleeVolume1;

    private AudioSource audio;

    private void Start()
    {
        panel = GameObject.FindGameObjectWithTag("Panel");
        tint = panel.GetComponent<TintScreen>();
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy hit the player!");
            StartCoroutine(tint.FadeImage());
            audio.PlayOneShot(enemyMeleeSound1);

        }
    }
}
