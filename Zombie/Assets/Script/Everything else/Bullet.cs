using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public TrailRenderer trailRenderer;
    public Rigidbody2D rb;
    public bool isPiercing;
    public float damage;
    public float despawnTimer;

    public LayerMask layerToHit, layerToDespawn;

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((layerToHit & (1 << other.gameObject.layer)) != 0)
        {
            if (other.TryGetComponent<Health>(out var health)) //checks if the collided object has a health script
            {
                health.TakeDamage(damage);
            }

            if (!isPiercing)
                TurnOffObj();
            damage *= 0.85f;
        }

        if ((layerToDespawn & (1 << other.gameObject.layer)) != 0)
            TurnOffObj();
    }

    void TurnOffObj()
    {
        layerToHit = 0;
        damage = 0f;
        if (trailRenderer != null)
            trailRenderer.Clear();
        gameObject.SetActive(false);
    }

    public IEnumerator Despawn()
    {
        float timer = 0;

        while (timer < despawnTimer) //makes a timer thatll cancel the script if the object is inactive
        {
            if (!gameObject.activeInHierarchy)
                yield break;

            timer += Time.deltaTime;
            yield return null;
        }
        TurnOffObj();
    }
}
