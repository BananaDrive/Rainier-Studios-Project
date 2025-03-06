using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public enum EnemyType
    {
        civilian,
        crawly,
        policeMan,
        soldierA,
        soliderB
    }
    public EnemyType enemyType;
    public EnemyMovement enemyMovement;
    public LayerMask enemyLayer;

    [Header("Stats")]
    public float damage;
    public float attackRate;
    public float speed;
    public float attackRange;
    public float rangeToAttack;

    [Header("Traits")]
    public bool canJump;
    public bool canSprint, explodeOnDeath;

    public bool hasAttacked;

    public void InitializeStats(float speed, float attackRate, float attackRange, float damage)
    {
        this.speed = speed;
        enemyMovement.moveSpeed = speed;
        this.attackRate = attackRate;
        this.attackRange = attackRange;
        this.damage = damage;
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

                collider.attachedRigidbody.AddForceX(distance / 350f, ForceMode2D.Impulse);
            }
        }
    }
}
