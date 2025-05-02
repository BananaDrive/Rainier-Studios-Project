using System.Collections;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public AudioSource health, death, hurt;
    public float damageReduc;
    public float maxHealth;
    public float currentHealth;
    public float invincibltyTime;
    public bool invincible;

    public abstract void HandleDeath();
    public abstract void OtherDamageLogic(float damage);
    public abstract void OtherHealthLogic();

    public void Start()
    {
        damageReduc = 1;
    }


    public void TakeDamage(float damage)
    {
        if (invincible)
            return;

        float finalDamage = damage / damageReduc;
        currentHealth -= finalDamage;

        OtherDamageLogic(finalDamage);

        if (currentHealth <= 0)
            HandleDeath();
    }

    public void AddHealth(float addHealth)
    {
        currentHealth = Mathf.Clamp(currentHealth += addHealth, 0f, maxHealth);

        OtherHealthLogic();
    }

    public IEnumerator Invincible()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibltyTime);
        invincible = false;
    }
}
