using System.Collections;
using UnityEngine;

public class PlayerHealth : Health
{
    public GameObject overlay;

    public float regenRate;
    public float regenAmount;
    public bool regenTick;

    bool overlayCooldown;

    public override void OtherDamageLogic(float damage)
    {
        GameManager.Instance.UIManager.playerHealthBar.UpdateSlider(currentHealth);
        
        if (overlay != null && !overlayCooldown)
        {
            overlayCooldown = true;
            StartCoroutine(OverlayDisplay());
            if (hurt != null)
                hurt.Play();
        }

        CoroutineHandler.Instance.StartCoroutine(Invincible());
    }

    public override void OtherHealthLogic()
    {
        GameManager.Instance.UIManager.playerHealthBar.UpdateSlider(currentHealth);
    }

    new void Start()
    {
        GameManager.Instance.UIManager.playerHealthBar.InitializeSlider(maxHealth);
    }

    void FixedUpdate()
    {
        if (!regenTick && regenAmount > 0)
        {
            regenTick = true;
            Invoke(nameof(Regenerate), regenRate);
        }
    }

    public void Regenerate()
    {
        AddHealth(regenAmount * regenRate);
        regenTick = false;
    }

    public override void HandleDeath()
    {
        if (death != null)
            death.Play();
        GameManager.Instance.GameOver();
    }

    public IEnumerator OverlayDisplay()
    {
        GameManager.Instance.UIManager.hurtOverlay.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        overlayCooldown = false;
        GameManager.Instance.UIManager.hurtOverlay.gameObject.SetActive(false);
    }
}
