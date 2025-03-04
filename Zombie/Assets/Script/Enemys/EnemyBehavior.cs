using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject attackHitBox;
    public EnemyMovement enemyMovement;

    [Header("Stats")]
    public float damage;
    public float attackRate;
    public float speed;

    [Header("Traits")]
    public bool canJump, canSprint, explodeOnDeath;

    private bool hasAttacked;

    public void FixedUpdate()
    {
        if (enemyMovement.player != null && Vector2.Distance(enemyMovement.player.position, transform.position) < 1f && !hasAttacked)
        {
            hasAttacked = true;
            StartCoroutine(Attack());
        }    
    }

    public IEnumerator Attack()
    {
        enemyMovement.rb.linearVelocityX = 0;
        enemyMovement.canMove = false;
        yield return new WaitForSeconds(1f);
        attackHitBox.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        attackHitBox.SetActive(false);
        yield return new WaitForSeconds(1f);
        enemyMovement.canMove = true;
        hasAttacked = false;
    }
}
