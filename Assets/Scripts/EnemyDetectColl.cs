using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDetectColl : MonoBehaviour
{
    private AudioSource audioSource;//this is the audio source that displays the melee enemy punch sound
    [SerializeField]
    private float damage = 1;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(audioSource != null)
            {
                audioSource.Play();
            }
            if (other.gameObject.GetComponent<PlayerTakeDamage>() != null)
                other.gameObject.GetComponent<PlayerTakeDamage>().TakeDamage(damage);
            //TINT
        }
    }
}
