using System.Collections;
using System.IO.Compression;
using Unity.VisualScripting;
using UnityEngine;

public class Molotov : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D _collider;
    public LayerMask layerToHit, ground;

    public float damage;
    public float hitRate;
    public float length;
    public float despawnTimer;

    public bool hasHit;

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((ground & (1 << other.gameObject.layer)) != 0)
        {
            Explode();
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if ((layerToHit & (1 << other.gameObject.layer)) != 0)
        {
            if (other.TryGetComponent<Health>(out var health) && !hasHit)
            {
                hasHit = true;
                health.TakeDamage(damage);
                Invoke(nameof(Delay), hitRate);
            }
        }
    }

    public void Explode()
    {
        hasHit = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        _collider.size = new(length * (1 / transform.localScale.x), 0.1f);
    }

    public IEnumerator Despawn()
    {
        float timer = 0;

        while (timer < despawnTimer) //makes a timer thatll cancel the script if the object is inactive
        {
            if (!gameObject.activeInHierarchy)
                yield break;

            timer += Time.deltaTime;
            yield return Time.deltaTime;
        }
        _collider.size = new (transform.localScale.x, transform.localScale.y);
        rb.bodyType = RigidbodyType2D.Dynamic;
        hasHit = true;
        gameObject.SetActive(false);
    }


    public void Delay()
    {
        hasHit = false;
    }
}
