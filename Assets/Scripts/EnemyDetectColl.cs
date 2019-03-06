using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDetectColl : MonoBehaviour
{
    private GameObject panel;
    private TintScreen tint;
    [SerializeField]
    private AudioClip audioClip;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        panel = GameObject.FindGameObjectWithTag("Panel");
        tint = panel.GetComponent<TintScreen>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(audioSource != null && audioClip != null)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
            }
            Debug.Log("Enemy hit the player!");
            StartCoroutine(tint.FadeImage());
        }
    }
}
