using UnityEngine;

public class Damage : MonoBehaviour
{
    public LayerMask layerToHit;
    public float damage;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if ((layerToHit & (1 << other.gameObject.layer)) == 0)
            return;
        if (other.TryGetComponent<Health>(out var health))
        {
            health.TakeDamage(damage);
        }
    }
}
