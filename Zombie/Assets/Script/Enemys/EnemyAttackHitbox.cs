using UnityEngine;

public class EnemyAttackHitbox : MonoBehaviour
{
    public EnemyBehavior enemyBehavior;
    public float damage;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (enemyBehavior != null)
                other.GetComponent<Health>().TakeDamage(enemyBehavior.damage);
            else
                other.GetComponent<Health>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
