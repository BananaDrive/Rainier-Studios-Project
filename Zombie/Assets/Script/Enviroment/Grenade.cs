using UnityEngine;

public class Grenade : MonoBehaviour
{
    public Rigidbody2D rb;
    public LayerMask ground, layerToHit;
    public float damage;
    public float blastRadius;
    public float explodeTimer;

    bool hasExploded;

    public void Intitialize(float _damage, LayerMask _layerToHit)
    {
        damage = _damage;
        layerToHit = _layerToHit;

        rb.constraints = RigidbodyConstraints2D.None;
        hasExploded = false;

        Invoke(nameof(Explode), explodeTimer);
    }

    public void Explode()
    {
        if (!gameObject.activeSelf)
            return;

        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        foreach (var collider in Physics2D.OverlapCircleAll(transform.position, blastRadius, layerToHit))
        {
            if (collider.TryGetComponent<Health>(out var health))
            {
                health.TakeDamage(damage);
            }
        }

        gameObject.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if ((ground & (1 << other.gameObject.layer)) != 0 && !hasExploded)
        {
            hasExploded = true;
            Explode();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }
}
