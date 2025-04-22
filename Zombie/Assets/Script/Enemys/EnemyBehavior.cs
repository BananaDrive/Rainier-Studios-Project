using System.Collections;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public EnemyMovement enemyMovement;
    public LayerMask enemyLayer;

    [Header("Stats")]
    public float damage;
    public float attackRate;
    public float rangeToAttack;

    [Header("Traits")]
    public bool canJump;
    public bool canSprint, explodeOnDeath;

    public bool hasAttacked;

    public void InitializeStats(float attackRate, float damage)
    {
        this.attackRate = attackRate;
        this.damage = damage;
    }

    public IEnumerator AttackCD()
    {
        yield return new WaitForSeconds(attackRate);
        hasAttacked = false;
    }

    public void EnableClip<T>() where T : EnemyBehavior
    {
        foreach (var collider in Physics2D.OverlapCircleAll(transform.position, enemyMovement._collider.bounds.extents.x, enemyLayer))
        {
            if (collider.GetComponent<T>() != null && collider != enemyMovement._collider)
            {
                float distance = collider.transform.position.x - transform.position.x;
                if (distance <= 0)
                    distance = Mathf.Clamp(distance, -1f, -0.1f);
                else
                    distance = Mathf.Clamp(distance, 0.1f, 1f);
                distance = 0.02f / distance;

                collider.attachedRigidbody.AddForceX(distance / 2, ForceMode2D.Impulse);
            }
        }
    }
}
