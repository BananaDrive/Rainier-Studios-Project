using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackHitbox : MonoBehaviour
{
    public EnemyBehavior enemyBehavior;
    public bool hasHit;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !hasHit)
        {
            hasHit = true;
            other.GetComponent<Health>().TakeDamage(enemyBehavior.damage);
        }
    }
}
