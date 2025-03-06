using System.Collections;
using UnityEngine;

public class EnemyMelee : EnemyBehavior
{
    public GameObject attackHitBox;
    public EnemyAttackHitbox enemyAttackHitbox;
    
    public void FixedUpdate()
    {
        if (enemyMovement.player != null && Vector2.Distance(enemyMovement.player.position, transform.position) < rangeToAttack && !hasAttacked)
        {
            hasAttacked = true;
            attackHitBox.transform.position = new(transform.position.x + (enemyMovement.sprite.flipX ? -0.2f : 0.2f), transform.position.y);
            StartCoroutine(MeleeAttack());
        }

        EnableClip<EnemyMelee>();

    }

    public IEnumerator MeleeAttack()
    {
        enemyMovement.rb.linearVelocityX = 0;
        enemyMovement.canMove = false;
        yield return new WaitForSeconds(0.2f);
        attackHitBox.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        attackHitBox.SetActive(false);
        enemyAttackHitbox.hasHit = false;
        yield return new WaitForSeconds(5 / attackRate);
        enemyMovement.canMove = true;
        hasAttacked = false;
    }
}
