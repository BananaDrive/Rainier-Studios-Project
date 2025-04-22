using System.Collections;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public AudioSource health;
    public AudioSource death;
    public AudioSource hurt;
    public float damageReduc = 1;
    public float maxHealth;
    public float currentHealth;

    public abstract void HandleDeath();
    public abstract void OtherDamageLogic();

    public void Start()
    {
        damageReduc = 1;
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage / damageReduc;

        OtherDamageLogic();

        if (currentHealth <= 0)
            HandleDeath();
    }

    public void AddHealth(float addHealth)
    {
        currentHealth = Mathf.Clamp(currentHealth += addHealth, 0f, maxHealth);
    }
}
