using System.Collections;
using UnityEngine;

public class PlayerHealth : Health
{
    public GameObject overlay;

    public float regenRate;
    public float regenAmount;
    public bool regenTick;

    bool overlayCooldown;

    void FixedUpdate()
    {
        if (regenTick && regenAmount > 0)
        {
            regenTick = false;
            Invoke(nameof(Regenerate), regenRate);
        }
    }

    public void Regenerate()
    {
        currentHealth += regenAmount * regenRate;
        regenTick = true;
    }

    public override void OtherDamageLogic()
    {
        if (overlay != null && !overlayCooldown)
        {
            overlayCooldown = true;
            StartCoroutine(OverlayDisplay());
            hurt.Play();
        }  
    }

    public override void HandleDeath()
    {
        death.Play();
        GameManager.Instance.GameOver();
    }

    public IEnumerator OverlayDisplay()
    {
        overlay.SetActive(true);
        yield return new WaitForSeconds(1f);
        overlayCooldown = false;
        overlay.SetActive(false);
    }
}
