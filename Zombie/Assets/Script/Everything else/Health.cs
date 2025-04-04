using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject overlay;
    public AudioSource health;
    public AudioSource death;
    public AudioSource hurt;

    public enum HealthType
    {
        player,
        enemy,
        breakable
    }
    public HealthType healthType;
    public float maxHealth;
    public float currentHealth;

    bool overlayCooldown;
    

    public void TakeDamage(float damage)
    {
        if (overlay != null && !overlayCooldown)
        {
            overlayCooldown = true;
            StartCoroutine(OverlayDisplay());
           hurt.Play();
        }  
            
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

    public void HandleDeath()
    {
        if (healthType == HealthType.player)
        {
            death.Play();

            GameManager.Instance.GameOver();
        }
        else if (healthType == HealthType.enemy)
        {
            gameObject.SetActive(false);
            currentHealth = maxHealth;
        }
        else
            gameObject.SetActive(false);








    }

    public IEnumerator OverlayDisplay()
    {
        overlay.SetActive(true);
        yield return new WaitForSeconds(1f);
        overlayCooldown = false;
        overlay.SetActive(false);
    }
}
