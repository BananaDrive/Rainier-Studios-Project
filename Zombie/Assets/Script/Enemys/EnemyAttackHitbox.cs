using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackHitbox : MonoBehaviour
{
    public EnemyBehavior enemyBehavior;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Health>().TakeDamage(enemyBehavior.damage);
        }
    }

    
}
