using System;
using System.Collections;
using UnityEngine;

public class Traps : Placeable
{
    public float damage;
    public bool stopMovement;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (((layerToAvoid & (1 << other.gameObject.layer)) != 0) || !isActiveAndEnabled)
            return;
        if (other.TryGetComponent<Health>(out var health) && other.TryGetComponent<Movement>(out var movement))
        {
            health.TakeDamage(damage);
            if (stopMovement)
                StartCoroutine(StopMovement(movement));
        }
    }

    public IEnumerator StopMovement(Movement movement)
    {
        yield return new WaitForSeconds(0.1f);
        movement.canMove = false;
        movement.rb.linearVelocityX = 0;
        yield return new WaitForSeconds(2f);
        movement.canMove = true;
    }
}
