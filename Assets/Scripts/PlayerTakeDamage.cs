using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTakeDamage : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 5;

    [SerializeField]
    private Slider healthSlider;

    private float currentHealth;

    public event System.Action PlayerIsDead;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth / maxHealth;
        if (currentHealth < 0 && PlayerIsDead != null)
            PlayerIsDead.Invoke();
    }
}
