using UnityEngine;

public class EnemyAttackHitbox : MonoBehaviour
{
    public EnemyBehavior enemyBehavior;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            other.GetComponent<Health>().TakeDamage(enemyBehavior.damage);
        }
    }
}
