using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    public float currenthealth;

    public sliderScript healthBar;

    private void Start()
    {
        currenthealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float amount)
    {
        currenthealth -= amount;
        healthBar.SetHealth(currenthealth);
        if (currenthealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
