using UnityEngine;

public class PlayerHealth : Health
{
    public float regenRate;
    public float regenAmount;
    public bool regenTick;
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
}
