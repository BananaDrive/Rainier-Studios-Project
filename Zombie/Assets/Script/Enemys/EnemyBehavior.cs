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
}
