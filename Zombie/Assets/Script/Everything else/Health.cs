using System.Collections;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public AudioSource health;
    public AudioSource death;
    public AudioSource hurt;
    public HealthBar healthbar;
    public float damageReduc;
    public float maxHealth;
    public float currentHealth;
    public float invincibltyTime;
    public bool invincible;

    public abstract void HandleDeath();
    public abstract void OtherDamageLogic();
    public abstract void OtherHealthLogic();

    public void Start()
    {
        damageReduc = 1;
    }


    public void TakeDamage(float damage)
    {
        if (invincible)
            return;
        currentHealth -= damage / damageReduc;

        if (healthbar != null)
            healthbar.UpdateHealth(currentHealth / maxHealth);

        OtherDamageLogic();

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
