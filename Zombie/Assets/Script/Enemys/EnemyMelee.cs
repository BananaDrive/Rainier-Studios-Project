using System.Collections;
using UnityEngine;

public class EnemyMelee : EnemyBehavior
{
    public GameObject attackHitBox;
    private EnemyAttackHitbox enemyAttackHitbox;
    
    public void Start()
    {
        enemyAttackHitbox = attackHitBox.GetComponent<EnemyAttackHitbox>();
    }
    public void FixedUpdate()
    {
        if (enemyMovement.player != null && Vector2.Distance(enemyMovement.player.position, transform.position) < rangeToAttack && !hasAttacked)
        {
            hasAttacked = true;
            StartCoroutine(MeleeAttack());
        }    
    }

    public IEnumerator MeleeAttack()
    {
        enemyMovement.rb.linearVelocityX = 0;
        enemyMovement.canMove = false;
        yield return new WaitForSeconds(5 / attackRate);
        attackHitBox.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        attackHitBox.SetActive(false);
        enemyAttackHitbox.hasHit = false;
        yield return new WaitForSeconds(5 / attackRate);
        enemyMovement.canMove = true;
        hasAttacked = false;
    }
}
