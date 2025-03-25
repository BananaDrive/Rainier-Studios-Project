using System.Collections;
using UnityEngine;

public class EnemyMelee : EnemyBehavior
{
    public GameObject attackHitBox;
    public EnemyAttackHitbox enemyAttackHitbox;

    bool isAttacking;
    
    public void FixedUpdate()
    {
        if (enemyMovement.player != null && Vector2.Distance(enemyMovement.player.position, transform.position) < rangeToAttack && !hasAttacked)
        {
            hasAttacked = true;
            attackHitBox.transform.position = new(transform.position.x + (enemyMovement.sprite.flipX ? -0.2f : 0.2f), transform.position.y);
            CoroutineHandler.Instance.StartCoroutine(MeleeAttack());
        }
        if (!isAttacking)
            EnableClip<EnemyMelee>();
    }

    public IEnumerator MeleeAttack()
    {
        isAttacking = true;
        enemyMovement.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(0.2f);
        attackHitBox.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        attackHitBox.SetActive(false);
        enemyAttackHitbox.hasHit = false;
        yield return new WaitForSeconds(5 / attackRate);
        enemyMovement.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        isAttacking = false;
        hasAttacked = false;
    }
}
