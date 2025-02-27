using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public enum HealthType
    {
        player,
        enemy,
        breakable
    }

    public HealthType healthType;
    public float maxHealth;
    public float currentHealth;


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            HandleDeath();
    }

    public void HandleDeath()
    {
        if (healthType == HealthType.player)
            return;
        else if (healthType == HealthType.enemy)
            return;
        else
            gameObject.SetActive(false);
    }
}
