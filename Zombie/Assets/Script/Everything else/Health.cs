using System;
using System.Collections;
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

    public void AddHealth(float addHealth)
    {
        currentHealth += addHealth;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public IEnumerator RegenerateHealth(float potency, float duration)
    {
        float timer = 0;

        while (timer < duration)
        {
            AddHealth(potency);
            yield return new WaitForSeconds(1f);
            timer++;
        }

    }

    public void HandleDeath()
    {
        if (healthType == HealthType.player)
            GameManager.Instance.GameOver();
        else if (healthType == HealthType.enemy)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(false);
    }
}
