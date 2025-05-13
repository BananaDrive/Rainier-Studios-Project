using System.Collections;
using UnityEngine;

public class Traps : Placeable
{
    public ItemStats itemStats;

    public float damage, stopTime;
    public bool stopMovement, persists;

    void OnEnable()
    {
        if (!persists)
            Invoke(nameof(Despawn), despawnTime);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (((layerToAvoid & (1 << other.gameObject.layer)) != 0) || !isActiveAndEnabled)
            return;
        if (other.TryGetComponent<Health>(out var health) && other.TryGetComponent<Movement>(out var movement))
        {
            health.TakeDamage(damage);

            if (stopMovement)
            {
                if (other.TryGetComponent<BuffsHandler>(out var buffs))
                {
                    buffs.AddBuff(itemStats);
                    return;
                }
                CoroutineHandler.Instance.StartCoroutine(StopMovement(movement));
            }
        }
    }

    public IEnumerator StopMovement(Movement movement)
    {
        yield return new WaitForSeconds(0.1f);
        movement.canMove = false;
        movement.rb.linearVelocityX = 0;
        yield return new WaitForSeconds(stopTime);
        movement.canMove = true;
    }
}
